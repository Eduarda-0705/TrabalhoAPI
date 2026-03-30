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
        return Ok(await _context.Produtos.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}", Name = "GetProduto")] // Adicionamos o Name aqui
public async Task<ActionResult<Produto?>> GetByIdAsync(int id)
{
    return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
}

[HttpPost]
public async Task<ActionResult<Produto>> CreateAsync(Produto produto)
{
    produto.Id = 0;
    _context.Produtos.Add(produto);
    await _context.SaveChangesAsync();

    return CreatedAtRoute("GetProduto", new { id = produto.Id }, produto);
}

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();

        return NoContent(); // Retorno 204 [cite: 21]
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _context.Produtos.Where(p => p.Id == id).ExecuteDeleteAsync();
        return NoContent();
    }
}