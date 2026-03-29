using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoApi;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAllAsync()
    {
        var clientes = await _context.Clientes.AsNoTracking().ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cliente?>> GetByIdAsync(int id)
    {
        return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> CreateAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByIdAsync), new { id = cliente.Id }, cliente);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _context.Clientes.Where(c => c.Id == id).ExecuteDeleteAsync();
        return NoContent();
    }
}