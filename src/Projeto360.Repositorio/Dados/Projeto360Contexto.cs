using Microsoft.EntityFrameworkCore;
using Projeto360.Dominio.Entidades;

namespace Projeto360.Repositorio.Dados;

public class Projeto360Contexto : DbContext
{
    public Projeto360Contexto(DbContextOptions<Projeto360Contexto> options)
        : base(options) { }

    public DbSet<Tarefa> Tarefas => Set<Tarefa>();

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public DbSet<Projeto> Projetos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tarefa>(entidade =>
        {
            entidade.ToTable("Tarefas");
            entidade.HasKey(t => t.Id);

            entidade.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            entidade.Property(t => t.Concluida)
                .IsRequired();
        });

        modelBuilder.Entity<Usuario>(entidade =>
        {
            entidade.ToTable("Usuarios");

            entidade.HasKey(u => u.Id);

            entidade.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(150);

            entidade.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            entidade.Property(u => u.SenhaHash)
                .IsRequired();

            entidade.Property(u => u.TipoUsuario)
                .IsRequired();
        });

        modelBuilder.Entity<Projeto>(entidade =>
        {
            entidade.ToTable("Projetos");

            entidade.HasKey(p => p.Id);

            entidade.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entidade.Property(p => p.Descricao)
                .IsRequired(false)
                .HasMaxLength(500);
        });
    }
}