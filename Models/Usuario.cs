using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models
{
    public class Usuario : IdentityUser
    {
        public UserRole Role { get; set; } = UserRole.CLIENTE;
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        // construtor que vai invocar o identity user através do base()
        public Usuario() : base() 
        {

        }
    }
}
