using ecoshare_backend.Data.DTOs;

namespace ecoshare_backend.Data.DTOs;

public class LoginRequestResponse : AuthResult
{
    public bool Result { get; internal set; }
}