using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Categorias(NomeCategoria, ImagemCategoria) Values('Bebidas','bebidas.jpg')");
            migrationBuilder.Sql("Insert into Categorias(NomeCategoria, ImagemCategoria) Values('Lanches','lanches.jpg')");
            migrationBuilder.Sql("Insert into Categorias(NomeCategoria, ImagemCategoria) Values('Sobremesas','sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
