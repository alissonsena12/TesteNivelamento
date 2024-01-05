using Models.CreditoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Service
{
    public interface ICreditoService
    {
        Task<AprovacaoCreditoResult> AprovacaoCredito(AprovacaoCreditoModel aprovacaoCreditoModel);
    }
}
