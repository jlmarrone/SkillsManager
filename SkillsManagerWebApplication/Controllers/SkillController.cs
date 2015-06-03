using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkillsManagerWebApplication.Models;
using SkillsManagerWebApplication.DAL;

namespace SkillsManagerWebApplication.Controllers
{
    public class SkillController : Controller
    {
        private SkillDBContext db = new SkillDBContext();

        //
        // GET: /Skill/

        public ActionResult Index(int employee_id = 0, string username = "")
        {
            var skills = db.Skills.Include(s => s.Technology);
            if (employee_id != 0)
            {
                skills = from s in db.Skills.Include(s => s.Technology)
                             where s.EmployeeID == employee_id
                             select s;
            }
            else if (username != "")
            {
                var employees = from e in db.UserProfiles
                           where e.UserName == username
                           select e;
                if (employees.Count() == 0)
                {
                    return RedirectToAction("Index", "Home");
                }else
                {
                    var employee = employees.Single();
                    skills = from s in db.Skills.Include(s => s.Technology).Include(s => s.UserProfile)
                             where s.EmployeeID == employee.UserId
                             select s;
                }
            }

            return View(skills.ToList());
        }

        //  
        // GET: /Skill/Details/5

        public ActionResult Details(int id = 0)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        //
        // GET: /Skill/Create

        public ActionResult Create()
        {
            var employees = from e in db.UserProfiles
                            where e.UserName == User.Identity.Name
                            select e;

            var employeeTechnologies = (from s in db.Skills.Include(s => s.TechnologyID)
                         where s.EmployeeID == employees.FirstOrDefault().UserId
                         select s.TechnologyID).ToArray();

            var technologies = db.Technologies.Where(t => !employeeTechnologies.Contains(t.TechnologyID));

            ViewBag.TechnologyID = new SelectList(technologies, "TechnologyID", "Name");
            List<string> levelslist = new List<string>();
            string[] members = { "Ninguno", "Basico", "Intermedio", "Avanzado" };
            foreach (var level in members)
            {
                levelslist.Add(level);
            }
            ViewBag.levelslist = levelslist;
            return View();
        }

        //
        // POST: /Skill/Create

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            var employees = from u in db.UserProfiles where u.UserName == User.Identity.Name select u;
            UserProfile employee = employees.First();
            skill.EmployeeID = employee.UserId;
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TechnologyID = new SelectList(db.Technologies, "TechnologyID", "Name", skill.TechnologyID);
            List<string> levelslist = new List<string>();
            string[] members = { "Ninguno", "Basico", "Intermedio", "Avanzado" };
            foreach (var level in members)
            {
                levelslist.Add(level);
            }
            ViewBag.levelslist = levelslist;
            return View(skill);
        }

        //
        // GET: /Skill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewBag.TechnologyID = new SelectList(db.Technologies, "TechnologyID", "Name", skill.TechnologyID);
            List<string> levelslist = new List<string>();
            string[] members = { "Ninguna", "Basico", "Intermedio", "Avanzado", "Experto" };
            foreach (var level in members)
            {
                levelslist.Add(level);
            }
            ViewBag.levelslist = levelslist;
            return View(skill);
        }

        //
        // POST: /Skill/Edit/5

        [HttpPost]
        public ActionResult Edit(Skill skill)
        {
            var employees = from u in db.UserProfiles where u.UserName == User.Identity.Name select u;
            UserProfile employee = employees.First();
            skill.EmployeeID = employee.UserId;
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TechnologyID = new SelectList(db.Technologies, "TechnologyID", "Name", skill.TechnologyID);
            List<string> levelslist = new List<string>();
            string[] members = { "Ninguna", "Basico", "Intermedio", "Avanzado", "Experto" };
            foreach (var level in members)
            {
                levelslist.Add(level);
            }
            ViewBag.levelslist = levelslist;
            return View(skill);
        }

        //
        // GET: /Skill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        //
        // POST: /Skill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skills.Find(id);
            db.Skills.Remove(skill);
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