
using ecoshare_backend.Data;
using ecoshare_backend.Data.DTOs;
using ecoshare_backend.Models;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        try
        {
            return await _context.Produtos
            .Include(p => p.Fornecedor) 
            .ToListAsync();
        } catch (Exception e)
        {
            throw new Exception(e.Message.ToString());
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto == null) return NotFound();

        var produtoDto = new ProdutoDto
        {
            ProdutoId = produto.ProdutoId,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Descricao = produto.Descricao,
            FornecedorId = produto.FornecedorId,
            Imagem = produto.Imagem,
            FornecedorNome = produto.Fornecedor?.NomeFantasia,
        };

        return produtoDto;
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

    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduto), new { id = produto.ProdutoId }, produto);
    }

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

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? nome, [FromQuery] string? fornecedor, [FromQuery] string? descricao)
    {
        var query = _context.Produtos.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(p => p.Nome.Contains(nome));

        if (!string.IsNullOrEmpty(fornecedor))
            query = query.Where(p => p.Fornecedor.NomeFantasia.Contains(fornecedor));

        if (!string.IsNullOrEmpty(descricao))
            query = query.Where(p => p.Descricao.Contains(descricao));

        var result = await query.ToListAsync();

        return Ok(result);
    }
}