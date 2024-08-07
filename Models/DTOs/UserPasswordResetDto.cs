using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models.DTOs;

public class UserPasswordResetDto
{
    [Required]

    public string Email { get; set; }
}

