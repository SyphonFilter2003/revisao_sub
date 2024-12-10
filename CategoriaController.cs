using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/categoria/cadastrar
    [HttpPost("cadastrar")]
    public async Task<ActionResult<Categoria>> CadastrarCategoria(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ListarCategorias), new { id = categoria.CategoriaId }, categoria);
    }

    // GET: api/categoria/listar
    [HttpGet("listar")]
    public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategorias()
    {
        return await _context.Categorias.Include(c => c.Tarefas).ToListAsync();
    }
}
