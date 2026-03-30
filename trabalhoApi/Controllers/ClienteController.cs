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
        return Ok(await _context.Clientes.AsNoTracking().ToListAsync());
    }

    [HttpGet("{id:int}", Name = "GetCliente")] // Adicionamos o Name aqui
public async Task<ActionResult<Cliente?>> GetByIdAsync(int id)
{
    return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
}

[HttpPost]
public async Task<ActionResult<Cliente>> CreateAsync(Cliente cliente)
{
    cliente.Id = 0;
    _context.Clientes.Add(cliente);
    await _context.SaveChangesAsync();

    return CreatedAtRoute("GetCliente", new { id = cliente.Id }, cliente);
}
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();

        return NoContent(); // Retorno 204 [cite: 31]
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _context.Clientes.Where(c => c.Id == id).ExecuteDeleteAsync();
        return NoContent();
    }
}