using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Models.ConfigurationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeira.Configuration.BuilderExtensions
{
    public static class InjecaoConfiguracoesBuilderExtensions
    {
        public static IServiceCollection AddConfiguracoesBuilderExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ParametrosAprovacaoCredito>(configuration.GetSection("ParametrosAprovacaoCredito"));
            return services;
        }
    }
}
