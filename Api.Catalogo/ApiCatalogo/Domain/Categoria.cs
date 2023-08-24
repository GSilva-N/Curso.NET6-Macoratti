using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.Domain;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? NomeCategoria { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemCategoria { get; set; }

    public ICollection<Produto> Produtos { get; set; }
    
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
}

