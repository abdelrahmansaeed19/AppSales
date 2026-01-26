using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices.Auth
{

    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string email, string code);
        Task SendResetPasswordEmailAsync(string email, string code);
    }
}
