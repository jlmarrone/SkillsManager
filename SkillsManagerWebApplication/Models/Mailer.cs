using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace SkillsManagerWebApplication.Models
{
    public class Mailer
    {
        string ADMIN_EMAIL      = "skillseveris@gmail.com";
        string ADMIN_EMAIL_PASS = "a1D3g5z0";

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        
 
	    public Mailer()
	    {
            smtpServer.EnableSsl = true;
            smtpServer.Credentials = new System.Net.NetworkCredential(ADMIN_EMAIL, ADMIN_EMAIL_PASS);
            smtpServer.Port = 587;
            smtpServer.UseDefaultCredentials = true;
	    }
        public void SendMessage(MailMessage email_message)
        {
            smtpServer.Send(email_message);
        }
    }
}