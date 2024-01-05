using Interfaces.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.CreditoModels;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Financeira.Controllers
{
    [ApiController]
    public class CreditoController : Controller
    {
        private readonly ICreditoApplication _creditoApplication;

        public CreditoController(ICreditoApplication creditoApplication)
        {
            _creditoApplication = creditoApplication;
        }

        [HttpPost("aprovacao-credito")]
        public async Task<IActionResult> AprovacaoCredito([FromBody] AprovacaoCreditoModel aprovacaoCreditoModel)
        {          
            return Ok(await _creditoApplication.AprovacaoCredito(aprovacaoCreditoModel));
        }
    }
}
