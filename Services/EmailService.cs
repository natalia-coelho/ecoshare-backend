using FluentEmail.Core;

namespace ecoshare_backend.Services;

public class EmailService
{
    private readonly IFluentEmail _fluentEmail;
    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async void SendEmail()
    {
        var email = await _fluentEmail
            .To("bemesko@gmail.com")
            .Subject("Test Email")
            .Body("This is a test email")
            .SendAsync();
    }
}