
using ecoshare_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecoshare_backend.Controllers;

[Route("ecoshare/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: ecoshare/Produtos
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        //var roles = await _userManager.GetRolesAsync(user);
        //if (roles.Contains(RoleManager.GetRoleName(UserRole.Supplier)))
        //{
        //    // Logic for supplier
        //}
        return await _context.Produtos.ToListAsync();
    }

    // GET: ecoshare/Produtos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
            return NotFound();

        return produto;
    }

    // GET: ecoshare/Produtos/Fornecedor/5
    [HttpGet("Fornecedor/{fornecedorId}")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProductsBySupplier(int fornecedorId)
    {
        var productsBySupplier = await _context.Produtos.
                            Where(p => p.FornecedorId == fornecedorId)
                            .ToListAsync();

        if (productsBySupplier == null || !productsBySupplier.Any())
            return NotFound();

        return productsBySupplier;
    }

    // POST: ecoshare/Produtos
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduto), new { id = produto.ProdutoId }, produto);
    }

    // PATCH: ecoshare/Produtos/5
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProduto(int id, [FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Produto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
            return NotFound();

        patchDocument.ApplyTo(produto, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: ecoshare/Produtos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
            return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.ProdutoId == id);
    }

    [HttpPost("{id}/upload")]
    public async Task<IActionResult> UploadImage(int id, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Nenhum arquivo enviado.");

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
            return NotFound();

        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            produto.Imagem = ms.ToArray();
        }

        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();

        return Ok();
    }
}