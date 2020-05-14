﻿using System.Threading.Tasks;

namespace CinemaConstructor.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
