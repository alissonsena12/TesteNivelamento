using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.BuilderExtensions
{
    public static class InjecaoConfiguracoesBuilderExtensions
    {
        public static IServiceCollection AddInjecaoConfiguracoesBuilderExtensions(this IServiceCollection services)
        {            
            return services;
        }
    }
}
