using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Data.DTOs;

public class ForgotPasswordDto
{
    [Required]
    // [EmailAddress]
    public string? Email { get; set; }

}