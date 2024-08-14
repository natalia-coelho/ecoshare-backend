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
        private readonly EmailService _emailService;

        public UsuariosController(AppDbContext context, UsuarioService userService, EmailService emailService)
        {
            _context = context;
            _userService = userService;
            _emailService = emailService;
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
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto userDto)
        {
            var user = await _userService.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var token = await _userService.GeneratePasswordResetTokenAsync(user);

            // TODO: Change this into a URL for the frontend.
            // The call to the reset password endpoint should happen there.
            var passwordResetUrl = Url.Action("ResetPassword", "Usuarios",
            new { token, email = user.Email }, Request.Scheme);

            // Change this to an email
            _emailService.SendEmail();
            Console.WriteLine(passwordResetUrl);

            return Ok();
        }

        // https://chatgpt.com/share/25a14338-ddf6-4202-aa4f-89c2b78f1fc5
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userService.FindByEmailAsync(resetPasswordDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.ResetPasswordAsync(
                user,
                resetPasswordDto.Token,
                resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Password has been reset successfully." });
        }
    }
}
