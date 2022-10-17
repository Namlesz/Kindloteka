using System.Net.Mail;
using Kindloteka.Models;

namespace Kindloteka.Services;

public class EmailService
{
    private readonly MailMessage _mailMessage;
    private readonly SmtpClient _smtpClient;

    public EmailService()
    {
        var password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD") ?? throw new InvalidOperationException();
        var email = Environment.GetEnvironmentVariable("EMAIL_ACCOUNT") ?? throw new InvalidOperationException();

        _mailMessage = new MailMessage();
        _mailMessage.From = new MailAddress(email);
        _mailMessage.IsBodyHtml = true;

        _smtpClient = new SmtpClient("smtp.mailgun.org", 587);
        _smtpClient.Credentials = new System.Net.NetworkCredential(email, password);
        _smtpClient.EnableSsl = true;
    }

    public Task SendConfirmEmail(Book book, User user)
    {
        //CHANGE TO MOM EMAIL
        var email = Environment.GetEnvironmentVariable("EMAIL_RECIEVIER") ?? throw new InvalidOperationException();

        _mailMessage.To.Add(email);
        _mailMessage.Subject = "Rezerwacja książki ZSK";
        _mailMessage.IsBodyHtml = true;

        var msg =
            "<head><meta charset='UTF-8'></head>" +
            "<div>" +
            "<h3>Rezerwacja książki</h3>" +
            $"<table><tbody><tr><td>Imię i nazwisko: </td>{user.Name} {user.Surname}<td></td></tr><tr><td>Klasa: </td>{user.SchoolClass}<td></td></tr><tr><td>Tytuł: </td><td>{book.Title}</td></tr><tr><td>Autor: </td><td>{book.Author}</td></tr></tbody></table>" +
            "</div>";
        _mailMessage.Body = msg;

        try
        {
            return _smtpClient.SendMailAsync(_mailMessage);
        }
        catch (Exception ex)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            if (isDevelopment)
                Console.WriteLine(ex);
        }
        return Task.CompletedTask;
        
    }
}