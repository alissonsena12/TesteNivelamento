using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Configuration.BuilderExtensions
{
    public static class InjecaoDependenciaApplicationBuilderExtensions
    {
        public static IServiceCollection AddInjecaoDependenciaApplicationBuilderExtensions(this IServiceCollection services)
        {
            services.AddScoped<ICreditoApplication, CreditoApplication>();
            return services;
        }
    }
}
