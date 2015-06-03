using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkillsManagerWebApplication.Models;
using SkillsManagerWebApplication.DAL;
using System.Net.Mail;

namespace SkillsManagerWebApplication.Controllers
{
    [Authorize(Users = "administrator")]
    public class UserProfileController : Controller
    {
        private SkillDBContext db = new SkillDBContext();

        //
        // GET: /UserProfile/

        public ActionResult Index()
        {
            var employees = from u in db.UserProfiles
                            where u.UserName != "administrator"
                         select u;

            return View(employees);
        }

        //
        // GET: /UserProfile/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile != null)
            {
                var skills = from s in db.Skills
                             where s.EmployeeID == userprofile.UserId
                             select s;

                ViewBag.SkillsList = skills;
            }

            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /UserProfile/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserProfile/Create

        [HttpPost]
        public ActionResult Create(UserProfile userprofile)
        {
            string new_user_name        = userprofile.Email.Substring(0, userprofile.Email.IndexOf('@'));
            string enc_password         = Helpers.SHA1.Encode(userprofile.Password);
            userprofile.UserName        = new_user_name;
            userprofile.Password        = enc_password;
            userprofile.ConfirmPassword = enc_password;
            userprofile.RememberMe = false;
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                /* try
                {
                    string ADMIN_EMAIL = "skillseveris@gmail.com";
                    string ADMIN_NAME  = "administrator";

                    Mailer Cr = new Mailer();
                    MailMessage msg = new MailMessage();

                    msg.Subject = "Everis Skills Web Application - Account Created";
                    msg.To.Add(new MailAddress(userprofile.Email));
                    msg.From = new MailAddress(ADMIN_EMAIL, ADMIN_NAME);
                    msg.Priority = MailPriority.High;

                    msg.Body = "An account has been created for you on Skills Manager App. \n\n Your credentials are the following: UserName: " + userprofile.UserName + " Password: " + userprofile.Password + " Send from: Everis Skills Portal ";

                    Cr.SendMessage (msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }*/

                return RedirectToAction("Index");
            }

            return View(userprofile);
        }

        //
        // GET: /UserProfile/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /UserProfile/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // GET: /UserProfile/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /UserProfile/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}