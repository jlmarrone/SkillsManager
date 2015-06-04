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
using SkillsManagerWebApplication.Mailers;

namespace SkillsManagerWebApplication.Controllers
{
    [Authorize(Users = "administrator")]
    public class UserProfileController : Controller
    {
        private SkillDBContext db = new SkillDBContext();
        
        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }
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
            string unenc_password       = userprofile.Password;
            string enc_password         = Helpers.SHA1.Encode(userprofile.Password);
            userprofile.UserName        = new_user_name;
            userprofile.Password        = enc_password;
            userprofile.ConfirmPassword = enc_password;

            userprofile.RememberMe = false;
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();

                ViewBag.Name     = userprofile.UserName;
                ViewBag.Password = userprofile.Password;
                UserMailer.Welcome(userprofile.Email, new_user_name, unenc_password).Send();
               
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