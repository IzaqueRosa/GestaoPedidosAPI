using GestaoPedidos.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Data.DataBase
{
    namespace GestaoPedidos.Data.DataBase
    {
        public class GestaoPedidosContext : DbContext
        {
            public GestaoPedidosContext(DbContextOptions<GestaoPedidosContext> options)
                : base(options)
            {
            }

            public DbSet<Cliente> Cliente { get; set; }
            public DbSet<Pedido> Pedido { get; set; }
            public DbSet<PedidoHistorico> PedidoHistorico { get; set; }
            public DbSet<PedidoItem> PedidoItem { get; set; }
            public DbSet<Produto> Produto { get; set; }
        }
    }
}
