using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ConfigurationModels
{
    public class ParametrosAprovacaoCredito
    {
        /// <summary>
        /// Percentual em decimal da taxa de Crédito Direto
        /// </summary>
        public decimal CreditoDiretoTaxa { get; set; }
        /// <summary>
        /// Percentual em decimal da taxa de Crédito Consignado
        /// </summary>
        public decimal CreditoConsignadoTaxa { get; set; }
        /// <summary>
        /// Percentual em decimal da taxa de Crédito de Pessoa Jurídica
        /// </summary>
        public decimal CreditoPessoaJuridicaTaxa { get; set; }
        /// <summary>
        /// Percentual em decimal da taxa de Crédito de Pessoa Física
        /// </summary>
        public decimal CreditoPessoaFisica { get; set; }
        /// <summary>
        /// Percentual em decimal da taxa de Crédito Imobiliário
        /// </summary>
        public decimal CreditoImobiliario { get; set; }
        /// <summary>
        /// Valor que representa o empréstimo minimo para o tipo de empréstipo de Pessoa Jurídica
        /// </summary>
        public decimal VarlorMinimoPessoaJuridica { get; set; }
        /// <summary>
        /// Valor máximo que representa o empréstimo para qualquer tipo de empréstimo
        /// </summary>
        public decimal ValorMaximoLiberado { get; set; }
        /// <summary>
        /// Valor que representa a quantidade mínima de parcelas para o crédito a ser contratado
        /// </summary>
        public int QuantidadeMinimaParcelas { get; set; }
        /// <summary>
        /// Valor que representa a quantidade máxima de parcelas para o crédito a ser contratado
        /// </summary>
        public int QuantidadeMaximaParcelas { get; set; }
        /// <summary>
        /// Valor que representa o mínimo de dias para o vencimento da primeira parcela após o crédito ser contratado
        /// </summary>
        public int DiasPrimeiroVencimentoMinimo { get; set; }
        /// <summary>
        /// Valor que representa o máximo de dias para o vencimento da primeira parcela após o crédito ser contratado
        /// </summary>
        public int DiasPrimeiroVencimentoMaximo { get; set; }
    }
}
