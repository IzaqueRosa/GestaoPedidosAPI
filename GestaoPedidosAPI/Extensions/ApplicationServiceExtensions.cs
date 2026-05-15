using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Interfaces.Services;
using GestaoPedidos.Domain.Repositories;
using GestaoPedidos.Domain.Services;

namespace GestaoPedidosAPI.Extensions
{
    /// <summary>
    /// API configuration class
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Method responsible for injecting service dependencies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceInjection(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
