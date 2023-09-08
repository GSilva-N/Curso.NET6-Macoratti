using ApiCatalogo.Data;
using ApiCatalogo.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly Context _context;

    public CategoriasController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetAll()
    {
        try
        {
            var categorias = _context.Categorias?.ToList();

            if (categorias is null)
                return NotFound();

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um Erro...");
        }
        
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
    {
        var categorias = _context.Categorias?.Include(p=>p.Produtos).ToList();

        if (categorias is null)
            return NotFound();

        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id) 
    {
        var categoria = _context.Categorias?.FirstOrDefault(c => c.CategoriaId.Equals(id));

        if (categoria is null)
            return BadRequest();
        
        return Ok(categoria);
    }

    [HttpGet("{name}")]
    public ActionResult<IEnumerable<Categoria>> GetByName(string name)
    {
        var categorias = _context.Categorias?.Where(c=>c.NomeCategoria.Contains(name)).ToList();    

        if (categorias is null)
            return NotFound();

        return Ok(categorias);
    }



    [HttpPost]
    public ActionResult Post(Categoria categoria) 
    {
        if (categoria is null)
            return BadRequest();
        
        _context.Categorias?.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int idCategoria, Categoria categoria) 
    {
        if (idCategoria != categoria.CategoriaId)
            return BadRequest();
        
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int idCategoria) 
    {
        var catgoria = _context.Categorias?.FirstOrDefault(c => c.CategoriaId.Equals(idCategoria));

        if(catgoria is null)
            return NotFound();

        _context.Remove(catgoria); 
        _context.SaveChanges();

        return Ok(catgoria);
    }


}

