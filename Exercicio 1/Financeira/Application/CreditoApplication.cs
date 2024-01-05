using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Application;
using Interfaces.Service;
using Models.CreditoModels;

namespace Application
{
    public class CreditoApplication : ICreditoApplication
    {
        private readonly ICreditoService _creditoService;

        public CreditoApplication(ICreditoService creditoService)
        {
            this._creditoService = creditoService;           
        }

        public async Task<AprovacaoCreditoResult> AprovacaoCredito(AprovacaoCreditoModel aprovacaoCreditoModel)
        {
            return await _creditoService.AprovacaoCredito(aprovacaoCreditoModel);
        }
    }
}
