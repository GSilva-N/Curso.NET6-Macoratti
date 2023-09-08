using ApiCatalogo.Data;
using ApiCatalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly Context _context;

    public ProdutosController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetAll()
    {
        try
        {
            var produtos = _context.Produtos?.ToList();

            if (produtos == null)
                return NotFound();

            return Ok(produtos);
        }
        catch (Exception ex)
        {

            throw ex;
        }
        
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _context.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    [HttpGet("{name}")]
    public ActionResult<IEnumerable<Produto>> GetByName(string name) 
    { 
        var produtos = _context.Produtos?.Where(p => p.NomeProduto.Contains(name)).ToList();
        
        if (produtos is null || produtos.Count() == 0)
            return NotFound();

        return Ok(produtos);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {

        if (produto is null)
            return BadRequest();
                              
        _context.Produtos?.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id}:int")]
    public ActionResult Put(int idProduto,Produto produto) 
    {
        if(idProduto != produto.ProdutoId)
            return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) 
    {
        var produto = _context.Produtos?.FirstOrDefault(p => p.ProdutoId.Equals(id));

        if (produto is null) 
            return NotFound();

        _context.Remove(produto);
        _context.SaveChanges();
        
        return Ok();
    }

}

