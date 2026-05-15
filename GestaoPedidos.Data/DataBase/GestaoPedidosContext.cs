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

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                builder.Entity<Produto>()
                    .Property(x => x.Preco)
                    .HasColumnType("decimal(18,2)");

                builder.Entity<Pedido>()
                    .Property(x => x.Status)
                    .HasConversion<int>();

                builder.Entity<Pedido>()
                    .Property(x => x.ValorTotal)
                    .HasColumnType("decimal(18,2)");

                builder.Entity<PedidoItem>()
                    .Property(x => x.PrecoUnitario)
                    .HasColumnType("decimal(18,2)");

                builder.Entity<PedidoItem>()
                    .Property(x => x.ValorTotalItem)
                    .HasColumnType("decimal(18,2)");

                builder.Entity<PedidoHistorico>()
                    .Property(x => x.StatusAnterior)
                    .HasConversion<int>();

                builder.Entity<PedidoHistorico>()
                    .Property(x => x.NovoStatus)
                    .HasConversion<int>();

                builder.Entity<PedidoHistorico>()
                    .Property(x => x.Motivo)
                    .HasMaxLength(2000);
            }
        }
    }
}
