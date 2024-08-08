namespace ecoshare_backend.Data.DTOs
{
    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;
        public string StatusCode { get; set; }
        public List<string> Error { get; set; }
    }
}
