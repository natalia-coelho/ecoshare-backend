using ecoshare_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecoshare_backend.Controllers
{
    [Route("ecoshare/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnderecosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ecoshare/Enderecos
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            return await _context.Enderecos.ToListAsync();
        }

        // GET: ecoshare/Enderecos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);

            if (endereco == null)
                return NotFound();

            return endereco;
        }

        // POST: ecoshare/Enderecos
        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEndereco), new { id = endereco.EnderecoId }, endereco);
        }

        // PATCH: ecoshare/Enderecos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEndereco(int id, [FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Endereco> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
                return NotFound();

            patchDocument.ApplyTo(endereco, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, Endereco endereco)
        {
            if (id != endereco.EnderecoId)
                return BadRequest();

            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: ecoshare/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
                return NotFound();

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoExists(int id)
        {
            return _context.Enderecos.Any(e => e.EnderecoId == id);
        }
    }
}
