using Microsoft.Extensions.Configuration;
using Models.ConfigurationModels;
using Models.CreditoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidacaoAprovacaoCreditoAttribute : ValidationAttribute
    {
        private readonly IConfigurationRoot _configuration;
        private readonly string _nomePropriedade;
        public ValidacaoAprovacaoCreditoAttribute(string nomePropriedade)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            this._configuration = configuration;

            this._nomePropriedade = nomePropriedade;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ParametrosAprovacaoCredito? _parametrosAprovacaoCredito = null;

            //lógica para pegar da instancia do objeto
            var instance = validationContext.ObjectInstance as AprovacaoCreditoModel;
            if (instance.ValidarNaModel)
            {
                if (instance != null && instance.ParametrosAprovacaoCredito != null)
                {
                    _parametrosAprovacaoCredito = instance.ParametrosAprovacaoCredito;
                }

                //lógica para pegar do default
                if (_parametrosAprovacaoCredito == null)
                {
                    _parametrosAprovacaoCredito = _configuration.GetSection("ParametrosAprovacaoCredito").Get<ParametrosAprovacaoCredito>();
                }

                switch (_nomePropriedade)
                {
                    case nameof(AprovacaoCreditoModel.QuantidadeParcelas):
                        int QuantidadeParcelas = (int)value;
                        if (QuantidadeParcelas < _parametrosAprovacaoCredito.QuantidadeMinimaParcelas
                            || QuantidadeParcelas > _parametrosAprovacaoCredito.QuantidadeMaximaParcelas)
                        {
                            return new ValidationResult($"O valor deve estar entre {_parametrosAprovacaoCredito.QuantidadeMinimaParcelas} e {_parametrosAprovacaoCredito.QuantidadeMaximaParcelas}.");
                        }
                        break;
                    case nameof(AprovacaoCreditoModel.DataPrimeiroVencimento):
                        DateTime DataPrimeiroVencimento = (DateTime)value;
                        DateTime Hoje = DateTime.Now.Date;
                        DateTime DataMinima = Hoje.AddDays(_parametrosAprovacaoCredito.DiasPrimeiroVencimentoMinimo);
                        DateTime DataMaxima = Hoje.AddDays(_parametrosAprovacaoCredito.DiasPrimeiroVencimentoMaximo);
                        if (DataPrimeiroVencimento < DataMinima
                            || DataPrimeiroVencimento > DataMaxima)
                        {
                            return new ValidationResult($"O valor deve estar entre {DataMinima.ToString("dd/MM/yyyy")} e {DataMaxima.ToString("dd/MM/yyyy")}.");
                        }
                        break;
                    case nameof(AprovacaoCreditoModel.ValorCredito):
                        decimal ValorCredito = (decimal)value;
                        decimal valorMinimo = 0;
                        decimal valorMaximo = _parametrosAprovacaoCredito.ValorMaximoLiberado;
                        TipoCredito tipoCredito = instance.TipoCredito;
                        if (tipoCredito == TipoCredito.CreditoPessoaJuridica)
                        {
                            valorMinimo = _parametrosAprovacaoCredito.VarlorMinimoPessoaJuridica;
                        }
                        if (ValorCredito < valorMinimo
                            || ValorCredito > valorMaximo)
                        {
                            return new ValidationResult($"O valor deve estar entre {valorMinimo} e {valorMaximo}.");
                        }
                        break;
                    default:
                        break;
                }
            }

            return ValidationResult.Success;
        }
    }
}

