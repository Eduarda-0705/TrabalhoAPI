using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoApi;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAllAsync()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto?>> GetByIdAsync(int id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> CreateAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByIdAsync), new { id = produto.Id }, produto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _context.Produtos.Where(p => p.Id == id).ExecuteDeleteAsync();
        return NoContent();
    }
}