using ApiCatalogo.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Data;

public class Context : DbContext
{
    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet<Produto>? Produtos { get; set; }

    public Context(DbContextOptions<Context> options): base(options)
    {       
    }
}

