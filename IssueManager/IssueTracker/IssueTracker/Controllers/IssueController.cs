using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IssueTracker.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        private MyDbContext db { get; set; }
        private UserManager<ApplicationUser> manager { get; set; }
        public IssueController()
        {
            db = new MyDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: Issue
        public ActionResult Index()
        {
             var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(db.Issues.ToList().Where(issue => issue.Submitter.Id == currentUser.Id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult All()
        {
            return View(db.Issues.ToList());
        }

        [Authorize(Roles = "Admin, Agent")]
        public ActionResult AllOpen()
        {
            return View(db.Issues.ToList().Where(issue => issue.Status == IssueStatus.open));
        }

        // GET: Issue/Details/5
        public ActionResult Details(int? id)
        {
            var currentUserID = User.Identity.GetUserId();
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);

            if (issue == null)
            {
                return HttpNotFound();
            }
            if(issue.Submitter.Id != currentUserID && userManager.IsInRole(currentUserID,"User"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(issue);
        }

        // GET: Issue/Create
        public ActionResult Create()
        {
            var issueModel = new Issue();
            return View(issueModel);
        }

        // POST: Issue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Priority, Subject, Content")] Issue issue)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                issue.SubmitDate = DateTime.Now;
                issue.Status = IssueStatus.open;
                issue.Submitter = currentUser;
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        // GET: Issue/Edit/5
        public ActionResult Edit(int? id)
        {
            var currentUserID = User.Identity.GetUserId();
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            if (issue.Submitter.Id != currentUserID && userManager.IsInRole(currentUserID, "User"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Subject,Content,Priority,Status")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issue/Delete/5
        public ActionResult Delete(int? id)
        {
            var currentUserID = User.Identity.GetUserId();
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            if (issue.Submitter.Id != currentUserID && userManager.IsInRole(currentUserID, "User"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(issue);
        }

        // POST: Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
