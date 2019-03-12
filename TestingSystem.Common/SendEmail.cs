namespace TestingSystem.Common
{
    using System;
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// Defines the <see cref="SendEmail" />
    /// </summary>
    public class SendEmail
    {
        /// <summary>
        /// Gets or sets the EmailAccount
        /// </summary>
        private string EmailAccount { get; set; }

        /// <summary>
        /// Gets or sets the Pass
        /// </summary>
        private string Pass { get; set; }

        /// <summary>
        /// Gets or sets the SendAs
        /// </summary>
        private string SendAs { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// Gets or sets the ServerSmtp
        /// </summary>
        private string ServerSmtp { get; set; }

        /// <summary>
        /// Gets or sets the Port
        /// </summary>
        private int Port { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmail"/> class.
        /// </summary>
        public SendEmail()
        {
            EmailAccount = "";
            Pass = "";
            SendAs = "";
            Name = "Default Name";
            ServerSmtp = "smtp.gmail.com";
            Port = 587;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmail"/> class.
        /// </summary>
        /// <param name="email">The email<see cref="string"/></param>
        /// <param name="pass">The pass<see cref="string"/></param>
        public SendEmail(string email, string pass)
        {
            EmailAccount = email.Trim();
            Pass = pass.Trim();
            SendAs = EmailAccount;
            Name = EmailAccount.Split('@')[0];
            ServerSmtp = "smtp.gmail.com";
            Port = 587;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmail"/> class.
        /// </summary>
        /// <param name="email">The email<see cref="string"/></param>
        /// <param name="pass">The pass<see cref="string"/></param>
        /// <param name="sendAs">The sendAs<see cref="string"/></param>
        /// <param name="Name">The Name<see cref="string"/></param>
        /// <param name="smtp">The smtp<see cref="string"/></param>
        /// <param name="port">The port<see cref="string"/></param>
        public SendEmail(string email, string pass, string sendAs, string Name, string smtp, string port)
        {
            EmailAccount = email.Trim();
            Pass = pass.Trim();
            SendAs = sendAs;
            this.Name = Name;
            ServerSmtp = smtp;
            Port = int.Parse(port);
        }

        /// <summary>
        /// The Send
        /// </summary>
        /// <param name="EmailTo">The EmailTo<see cref="String"/></param>
        /// <param name="Subject">The Subject<see cref="String"/></param>
        /// <param name="Body">The Body<see cref="String"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string Send(String EmailTo, String Subject, String Body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ServerSmtp, Port);
                mail.From = new MailAddress(SendAs, Name);
                mail.To.Add(EmailTo.Trim());
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = Body;
                SmtpServer.Port = Port;
                SmtpServer.Credentials = new NetworkCredential(EmailAccount, Pass);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
