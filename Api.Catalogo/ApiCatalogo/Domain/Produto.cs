using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.Domain;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    [Required]
    [StringLength(80)]
    public string? NomeProduto { get; set; }

    [Required]
    [StringLength(300)]
    public string? DescricaoProduto { get; set; }

    [Required]
    [Column(TypeName ="decimal(10,2")]
    public decimal PrecoProduto { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemProduto { get; set; }

    public int EstoqueProduto { get; set;}

    public DateTime DataCadastroProduto { get; set; }

    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}

