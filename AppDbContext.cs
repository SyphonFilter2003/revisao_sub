using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurando o relacionamento entre Tarefa e Categoria
        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Categoria)  // Tarefa tem uma categoria
            .WithMany(c => c.Tarefas)  // Categoria pode ter muitas tarefas
            .HasForeignKey(t => t.CategoriaId)  // Relacionamento pela chave estrangeira CategoriaId
            .OnDelete(DeleteBehavior.Cascade);  // Definindo comportamento de deleção (opcional)

        base.OnModelCreating(modelBuilder);  // Chama a implementação base para configurações adicionais
    }
}
