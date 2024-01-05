using Application;
using Interfaces.Application;
using Interfaces.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Configuration.BuilderExtensions
{
    public static class InjecaoDependenciaServiceBuilderExtensions
    {
        public static IServiceCollection AddInjecaoDependenciaServiceBuilderExtensions(this IServiceCollection services)
        {
            services.AddScoped<ICreditoService, CreditoService>();
            return services;
        }
    }
}
