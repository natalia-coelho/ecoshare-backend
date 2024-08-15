using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace ecoshare_backend.Services;

public class EmailService
{
    private readonly IFluentEmail _fluentEmail;
    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task<SendResponse> SendForgotPasswordEmail(string subject, string resetPasswordLink)
    {
        return await _fluentEmail
            .To(subject)
            .Subject("Ecoshare - Esqueci minha senha")
            .Body($"Olá! Este email está sendo enviado pois recebemos uma solicitação de alteração de senha. Você pode fazer isso pelo link {resetPasswordLink}")
            .SendAsync();
    }
}