using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CreditoModels
{
    /// <summary>
    /// Classe que representa o retorno do endpoint da controller: Credito Action: AprovacaoCredito
    /// </summary>
    public class AprovacaoCreditoResult
    {
        /// <summary>
        /// Status do crédito deve conter um dos valores "Aprovado" ou "Reprovado"
        /// </summary>
        public string StatusCredito { get; set; } = string.Empty;
        /// <summary>
        /// Valor que representa o calculo do valor total do crédito com juros aplicado
        /// </summary>
        public decimal ValorTotalComJuros { get; set; }
        /// <summary>
        /// Valor que representa somente o juros após o calculo de Valor do Crédito - Valor Total com Juros
        /// </summary>
        public decimal ValorJuros { get; set; }
        /// <summary>
        /// Lista de mensagens para retornar erros ou o motivo do pq não foi aprovado
        /// </summary>
        public List<string> Mensagens { get; set; } = new List<string>();


    }
}
