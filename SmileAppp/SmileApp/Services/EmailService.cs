using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace SmileApp.Services
{


    public class EmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _correoEmisor = "sacroger7@gmail.com";
        private readonly string _contraseña = "wxbv qxrd ujmt tedf"; 

        public async Task EnviarConfirmacionCitaAsync(string correoDestino, string nombrePaciente, DateTime fechaHora)
        {
            var mensaje = new MailMessage();
            mensaje.From = new MailAddress(_correoEmisor);
            mensaje.To.Add(correoDestino);
            mensaje.Subject = "Confirmación de Cita";
            mensaje.Body = $"Hola {nombrePaciente},\n\nTu cita ha sido registrada exitosamente para el día {fechaHora:dd/MM/yyyy} a las {fechaHora:hh:mm tt}.\n\nGracias por confiar en nosotros.";

            using (var smtp = new SmtpClient(_smtpHost, _smtpPort))
            {
                smtp.Credentials = new NetworkCredential(_correoEmisor, _contraseña);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mensaje);
            }
        }
    }
}
