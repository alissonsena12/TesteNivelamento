using Models.ConfigurationModels;
using Models.CreditoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidacaoAprovacaoCreditoAttribute : ValidationAttribute
    {
        private ParametrosArovacaoCredito _parametrosValidacoes;
        private readonly object[] _valores;
        public ValidacaoAprovacaoCreditoAttribute(ParametrosArovacaoCredito parametrosValidacoes,params object[] valores)
        {
            this._parametrosValidacoes = parametrosValidacoes;
            this._valores = valores;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            //lógica para pegar do default
            if (_parametrosValidacoes == null)
            {
                this._parametrosValidacoes = (ParametrosArovacaoCredito)AppDomain.CurrentDomain.GetData("ParametrosValidacoes");                
            }

            switch (value.GetType().Name)
            {
                case nameof(AprovacaoCreditoModel.QuantidadeParcelas):
                    int QuantidadeParcelas = (int)value;
                    if(QuantidadeParcelas < _parametrosValidacoes.QuantidadeMinimaParcelas 
                        || QuantidadeParcelas > _parametrosValidacoes.QuantidadeMaximaParcelas)
                    {
                        return new ValidationResult($"O valor deve estar entre {_parametrosValidacoes.QuantidadeMinimaParcelas} e {_parametrosValidacoes.QuantidadeMaximaParcelas}.");
                    }
                    break;
                case nameof(AprovacaoCreditoModel.DataPrimeiroVencimento):
                    DateTime DataPrimeiroVencimento = (DateTime)value;
                    DateTime Hoje = DateTime.Now.Date;
                    DateTime DataMinima = Hoje.AddDays(_parametrosValidacoes.DiasPrimeiroVencimentoMinimo);
                    DateTime DataMaxima = Hoje.AddDays(_parametrosValidacoes.DiasPrimeiroVencimentoMaximo);
                    if(DataPrimeiroVencimento < DataMinima
                        || DataPrimeiroVencimento > DataMaxima)
                    {
                        return new ValidationResult($"O valor deve estar entre {DataMinima.ToString("dd/MM/aaaa")} e {DataMaxima.ToString("dd/MM/aaaa")}.");
                    }
                    break;
                case nameof(AprovacaoCreditoModel.ValorCredito):
                    decimal ValorCredito = (decimal)value;
                    decimal valorMinimo = 0;
                    decimal valorMaximo = _parametrosValidacoes.ValorMaximoLiberado;
                    TipoCredito tipoCredito = (TipoCredito)_valores[0];
                    if(tipoCredito == TipoCredito.CreditoPessoaJuridica)
                    {
                        valorMinimo = _parametrosValidacoes.VarlorMinimoPessoaJuridica;
                    }
                    if(ValorCredito < valorMinimo
                        || ValorCredito > valorMaximo)
                    {
                        return new ValidationResult($"O valor deve estar entre {valorMinimo} e {valorMaximo}.");
                    }
                    break;
                default:
                    break;
            }

            return ValidationResult.Success;
        }
    }
}
