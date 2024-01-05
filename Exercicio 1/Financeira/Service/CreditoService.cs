using Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Models.ConfigurationModels;
using Models.CreditoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CreditoService : ICreditoService
    {
        private readonly ParametrosAprovacaoCredito _parametrosAprovacaoCredito;
        public CreditoService(IConfiguration configuration)
        {
            this._parametrosAprovacaoCredito = configuration.GetSection("ParametrosAprovacaoCredito").Get<ParametrosAprovacaoCredito>();
        }

        public Task<AprovacaoCreditoResult> AprovacaoCredito(AprovacaoCreditoModel aprovacaoCreditoModel)
        {
            var result = new AprovacaoCreditoResult();
            ValidaParametros(aprovacaoCreditoModel, ref result);
            if (result.StatusCredito == "Aprovado")
            {
                decimal TaxaJuros = DefinicaoTaxaJuros(aprovacaoCreditoModel);
                result.ValorJuros = (TaxaJuros * aprovacaoCreditoModel.ValorCredito) * aprovacaoCreditoModel.QuantidadeParcelas;
                result.ValorTotalComJuros = result.ValorJuros + aprovacaoCreditoModel.ValorCredito;
            };
            return Task.FromResult<AprovacaoCreditoResult>(result);
        }
        #region Metódos Internos
        /// <summary>
        /// Define a taxa de juros de acordo com o enum TipoCredito
        /// </summary>
        /// <param name="aprovacaoCreditoModel"></param>
        /// <returns></returns>
        internal decimal DefinicaoTaxaJuros(AprovacaoCreditoModel aprovacaoCreditoModel)
        {
            decimal TaxaJuros = 0;
            switch (aprovacaoCreditoModel.TipoCredito)
            {
                case TipoCredito.CreditoDireto:
                    TaxaJuros = _parametrosAprovacaoCredito.CreditoDiretoTaxa;
                    break;
                case TipoCredito.CreditoConsignado:
                    TaxaJuros = _parametrosAprovacaoCredito.CreditoConsignadoTaxa;
                    break;
                case TipoCredito.CreditoPessoaJuridica:
                    TaxaJuros = _parametrosAprovacaoCredito.CreditoPessoaJuridicaTaxa;
                    break;
                case TipoCredito.CreditoPessoaFisica:
                    TaxaJuros = _parametrosAprovacaoCredito.CreditoPessoaFisica;
                    break;
                case TipoCredito.CreditoImobiliario:
                    TaxaJuros = _parametrosAprovacaoCredito.CreditoImobiliario;
                    break;
                default:
                    break;
            }

            return TaxaJuros;
        }

        /// <summary>
        /// Mesma validação que é feita em ValidacaoAprovacaoCreditoAttribute, mas se um dia a service for desacoplada em um microserviço vai manter a validação
        /// </summary>
        /// <param name="aprovacaoCreditoModel"></param>
        /// <param name="result"></param>
        internal void ValidaParametros(AprovacaoCreditoModel aprovacaoCreditoModel, ref AprovacaoCreditoResult result)
        {
            result.StatusCredito = "Aprovado";
            if (aprovacaoCreditoModel.QuantidadeParcelas < _parametrosAprovacaoCredito.QuantidadeMinimaParcelas
                        || aprovacaoCreditoModel.QuantidadeParcelas > _parametrosAprovacaoCredito.QuantidadeMaximaParcelas)
            {
                result.StatusCredito = "Reprovado";
                result.Mensagens.Add($"Quantidade de Parcelas: O valor deve estar entre {_parametrosAprovacaoCredito.QuantidadeMinimaParcelas} e {_parametrosAprovacaoCredito.QuantidadeMaximaParcelas}.");
            }

            DateTime DataPrimeiroVencimento = aprovacaoCreditoModel.DataPrimeiroVencimento;
            DateTime Hoje = DateTime.Now.Date;
            DateTime DataMinima = Hoje.AddDays(_parametrosAprovacaoCredito.DiasPrimeiroVencimentoMinimo);
            DateTime DataMaxima = Hoje.AddDays(_parametrosAprovacaoCredito.DiasPrimeiroVencimentoMaximo);
            if (DataPrimeiroVencimento < DataMinima
                || DataPrimeiroVencimento > DataMaxima)
            {
                result.StatusCredito = "Reprovado";
                result.Mensagens.Add($"Data primeiro vencimento: O valor deve estar entre {DataMinima.ToString("dd/MM/yyyy")} e {DataMaxima.ToString("dd/MM/yyyy")}.");
            }

            decimal ValorCredito = aprovacaoCreditoModel.ValorCredito;
            decimal valorMinimo = 0;
            decimal valorMaximo = _parametrosAprovacaoCredito.ValorMaximoLiberado;
            if (aprovacaoCreditoModel.TipoCredito == TipoCredito.CreditoPessoaJuridica)
            {
                valorMinimo = _parametrosAprovacaoCredito.VarlorMinimoPessoaJuridica;
            }
            if (ValorCredito < valorMinimo
                || ValorCredito > valorMaximo)
            {
                result.StatusCredito = "Reprovado";
                result.Mensagens.Add($"Valor crédito: O valor deve estar entre {valorMinimo} e {valorMaximo}.");
            }          
        }
        #endregion
    }
}
