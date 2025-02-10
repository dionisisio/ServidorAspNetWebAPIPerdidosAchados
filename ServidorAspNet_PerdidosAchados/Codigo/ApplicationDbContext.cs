using Microsoft.EntityFrameworkCore;

namespace ServidorAsp.Codigo
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UsuarioDTO> Users { get; set; } // Aqui é onde a tabela 'User' será mapeada
        public DbSet<ItemPerdidoAchadoDTO> ItensPerdidosAchados { get; set; } // Aqui é onde a tabela 'User' será mapeada

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Configuração adicional, se necessário
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Adicione quaisquer configurações personalizadas de mapeamento aqui
        }
    }
}
