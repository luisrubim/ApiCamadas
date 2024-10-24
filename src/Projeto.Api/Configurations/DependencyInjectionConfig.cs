using Projeto.Business.Interfaces;
using Projeto.Business.Notificacoes;
using Projeto.Business.Services;
using Projeto.Data.Context;
using Projeto.Data.Repository;

namespace Projeto.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Data
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            //Business
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<INotificador, Notificador>();

            return services;
        }

    }
}
