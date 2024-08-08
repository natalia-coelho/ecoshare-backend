using ecoshare_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace ecoshare_backend;

public static class RoleManager
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Enum.GetNames(typeof(UserRole)))
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    public static string GetRoleName(UserRole role) => Enum.GetName(typeof(UserRole), role);
}

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleManager.SeedRolesAsync(roleManager);
    }
}