namespace AspNetCoreTemplate.Web.ViewModels.Users
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class Example
    {
        public async Task Execute(string from, string to, string subject, string plainTextContent, string htmlContent)
        {
            var apiKey = GlobalConstants.EmailApiKey;
            var client = new SendGridClient(apiKey);
            var fromEmail = new EmailAddress(from);
            var toEmail = new EmailAddress(to);
            htmlContent = "<strong>" + htmlContent + "<strong>";
            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
