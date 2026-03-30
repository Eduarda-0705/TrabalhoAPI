using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoApi;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAllAsync()
    {
        return Ok(await _context.Categorias.AsNoTracking().ToListAsync());
    }

   [HttpGet("{id:int}", Name = "GetCategoria")] // Adicionamos o Name aqui
public async Task<ActionResult<Categoria?>> GetByIdAsync(int id)
{
    return await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
}

[HttpPost]
public async Task<ActionResult<Categoria>> CreateAsync(Categoria categoria)
{
    categoria.Id = 0;
    _context.Categorias.Add(categoria);
    await _context.SaveChangesAsync();

    // Mudamos para CreatedAtRoute usando o nome que demos acima
    return CreatedAtRoute("GetCategoria", new { id = categoria.Id }, categoria);
}

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();

        return NoContent(); // Retorno 204 [cite: 21, 26]
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _context.Categorias.Where(c => c.Id == id).ExecuteDeleteAsync();
        return NoContent();
    }
}