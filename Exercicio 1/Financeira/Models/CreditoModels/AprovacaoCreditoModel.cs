using Models.ConfigurationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Models.Attributes;
using System.ComponentModel;
using System.Runtime.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Models.CreditoModels
{
    /// <summary>
    /// Os valores do objeto ParametrosValidacoes se não preenchidos pegaram o valor padrão definido arquivo de configuração do projeto
    /// </summary>
    public class AprovacaoCreditoModel
    {
        /// <summary>
        /// Valor do crédito a ser contratado
        /// </summary>
        [Required]
        [ValidacaoAprovacaoCredito(nameof(ValorCredito))]
        public decimal ValorCredito { get; set; }
        [Required]
        /// <summary>
        /// Tipo do crédito a ser contratado
        /// </summary>
        public TipoCredito TipoCredito { get; set; }
        /// <summary>
        /// Quantidade de parcelas do crédito a ser contratado
        /// </summary>
        [Required]
        [ValidacaoAprovacaoCredito(nameof(QuantidadeParcelas))]
        public int QuantidadeParcelas { get; set; }
        /// <summary>
        /// Data do vencimento da primeira parcela do crédito a ser contratado
        /// </summary>
        [Required]
        [ValidacaoAprovacaoCredito(nameof(DataPrimeiroVencimento))]
        public DateTime DataPrimeiroVencimento { get; set; }
        /// <summary>
        /// Indica se a validação será realizada diretamente pelo atributo ValidacaoAprovacaoCreditoAttribute.
        /// Se for true, o atributo realiza a validação na propriedade AprovacaoCreditoModel. Se for false, a validação será feita somente na camada de serviço correspondente.
        /// </summary>      
        public bool ValidarNaModel { get; set; } = false;
        /// <summary>
        /// Configurações padrão para parâmetros de aprovação de crédito.
        /// Caso não fornecidos na requisição, são obtidos do appsettings.json.
        /// </summary>
        public ParametrosAprovacaoCredito? ParametrosAprovacaoCredito { get; set; } = null;
    }
    /// <summary>
    /// Tipo do crédito a ser contratado
    /// </summary>
    public enum TipoCredito
    {
        /// <summary>
        /// Crédito Direto
        /// </summary>
        CreditoDireto,
        /// <summary>
        /// Crédito Consignado
        /// </summary>
        CreditoConsignado,
        /// <summary>
        /// Crédito Pessoa Jurídica
        /// </summary>
        CreditoPessoaJuridica,
        /// <summary>
        /// Crédito Pessoa Física
        /// </summary>
        CreditoPessoaFisica,
        /// <summary>
        /// Crédito Imobiliário
        /// </summary>
        CreditoImobiliario
    }

}
