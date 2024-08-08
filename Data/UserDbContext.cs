using ecoshare_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecoshare_backend.Data
{
    public class UserDbContext : IdentityDbContext<Usuario>
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
    }
}
