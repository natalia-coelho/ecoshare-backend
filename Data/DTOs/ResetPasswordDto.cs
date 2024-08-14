using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Data.DTOs;

public class ResetPasswordDto
{
    public string Token { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string NewPassword { get; set; }
}