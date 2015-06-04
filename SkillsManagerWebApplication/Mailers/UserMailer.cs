using Mvc.Mailer;

namespace SkillsManagerWebApplication.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Welcome(string receiver_email, string receiver_name, string receiver_password)
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
                ViewBag.Name = receiver_name;
                ViewBag.Password = receiver_password;
				x.Subject = "Welcome to Skills Manager";
				x.ViewName = "Welcome";
				x.To.Add(receiver_email);
			});
		}
 	}
}