using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ecoshare_backend.Models;
using ecoshare_backend.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ecoshare_backend.Controllers
{
    
    [ApiController]
    [Route("ecoshare/[controller]")]
    [Authorize]
    public class PerfilController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly AppDbContext _context;

        public PerfilController(UserManager<Usuario> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Pessoa>> GetPerfil()
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized("Usuário não está autenticado.");
            
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            var pessoa = _context.Pessoas.FirstOrDefault(p => p.UsuarioId == usuario.Id);
            if (pessoa == null) return NotFound("Perfil não encontrado. Necessário cadastrar em configurações.");

            return Ok(pessoa);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerfil(Pessoa pessoaAtualizada)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            var pessoa = _context.Pessoas.FirstOrDefault(p => p.UsuarioId == usuario.Id);
            if (pessoa == null) return NotFound("Perfil não encontrado.");

            pessoa.UsuarioId = usuario.Id;
            pessoa.Nome = pessoaAtualizada.Nome;
            pessoa.Sobrenome = pessoaAtualizada.Sobrenome;
            pessoa.EmailContato = pessoaAtualizada.EmailContato;
            pessoa.Bio = pessoaAtualizada.Bio;
            pessoa.TituloPerfil = pessoaAtualizada.TituloPerfil;
            pessoa.Descricao = pessoaAtualizada.Descricao;
            pessoa.FotoPerfil = pessoaAtualizada.FotoPerfil;

            if (pessoaAtualizada.EnderecoId != 0)
            {
                var novoEndereco = await _context.Enderecos.FindAsync(pessoaAtualizada.EnderecoId);
                if (novoEndereco == null) return BadRequest("Novo endereço não encontrado.");

                pessoa.EnderecoId = novoEndereco.EnderecoId;
                pessoa.Endereco = novoEndereco;
            }
            else if (pessoaAtualizada.EnderecoId == 0 && pessoa.Endereco != null)
            {
                pessoa.Endereco = null;
                pessoa.EnderecoId = 0;
            }

            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> CreatePerfil(Pessoa novaPessoa)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized("Usuário não está autenticado.");

            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            var perfilExistente = _context.Pessoas.FirstOrDefault(p => p.UsuarioId == usuario.Id);
            if (perfilExistente != null) return BadRequest("Perfil já existe para este usuário.");

            novaPessoa.UsuarioId = usuario.Id;

            if (novaPessoa.EnderecoId != 0)
            {
                var enderecoExistente = await _context.Enderecos.FindAsync(novaPessoa.EnderecoId);

                if (enderecoExistente == null) return BadRequest("Endereço não encontrado.");
                
                novaPessoa.Endereco = enderecoExistente;
            }

            _context.Pessoas.Add(novaPessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerfil), new { id = novaPessoa.PessoaId }, novaPessoa);
        }
    }
}
