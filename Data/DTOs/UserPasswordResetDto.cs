using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Data.DTOs;

public class UserPasswordResetDto
{
    [Required]

    public string Email { get; set; }
}

