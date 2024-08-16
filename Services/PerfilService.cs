using ecoshare_backend.Data;
using ecoshare_backend.Models;

namespace ecoshare_backend.Services
{
    public class PerfilService
    {
        private AppDbContext _context;

        public PerfilService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CriarPerfil(Usuario user)
        {
            var perfil = new Pessoa
            {
                Usuario = user,
                UsuarioId = user.Id,
                EmailContato = user.Email,
                TelefoneContato = user.PhoneNumber
            };

            _context.Pessoas.Add(perfil);
            await _context.SaveChangesAsync();
        }
    }
}
