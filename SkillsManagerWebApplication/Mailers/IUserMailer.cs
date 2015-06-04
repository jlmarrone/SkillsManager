using Mvc.Mailer;

namespace SkillsManagerWebApplication.Mailers
{ 
    public interface IUserMailer
    {
			MvcMailMessage Welcome(string receiver_email, string receiver_name, string receiver_password);
	}
}