using ecoshare_backend.Data;
using ecoshare_backend.Data.DTOs;
using ecoshare_backend.Models;
using ecoshare_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecoshare_backend.Controllers
{
    [Route("ecoshare/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UsuarioService _userService;

        public UsuariosController(AppDbContext context, UsuarioService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: ecoshare/Usuarios
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: ecoshare/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return usuario;
        }

        // POST: ecoshare/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PATCH: ecoshare/Usuarios/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUsuario(int id, [FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Usuario> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            patchDocument.ApplyTo(usuario, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(string id, Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: ecoshare/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDTO userDto)
        {
            var token = await _userService.LoginAsync(userDto);
            return Ok(new LoginRequestResponse()
            {
                Token = token,
                Result = true
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUser(UserRegistrationDTO userDTO)
        {
            await _userService.RegisterUser(userDTO);
            return Ok(userDTO);
        }


        [HttpPost]
        [Route("ForgotPassword")] //Will be Forgot password
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto requestDto)
        {
            //Verificar se e-mail existe
            Console.WriteLine(requestDto.Email);
            return Ok();

            // TODO: Rename this endpoint to forgot password and create a new one to actually reset it
            // 1. Check if email is in database
            // 2. Generate a password reset token
            // 3. Generate a password reset link like resetpassword?token=xxx,email=yyy
            // 4. (do last) send the link via email
            // 5. Return ok
        }

        // https://chatgpt.com/share/25a14338-ddf6-4202-aa4f-89c2b78f1fc5
        // Reset Password
        // If implemented separately can be reused in a context other than forgetting the password
        // 1. check if user exists
        // 2. forward email and token to a function in the user manager that actually resets the password if the token is valid

    }
}
