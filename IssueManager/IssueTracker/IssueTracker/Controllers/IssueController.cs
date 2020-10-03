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
using PagedList;

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

       
        // GET: My Issues
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var issues = from issue in db.Issues
                         where issue.Submitter.Id == currentUser.Id
                         select issue;
            if (!String.IsNullOrEmpty(searchString))
            {
                issues = issues.Where(s => s.Subject.Contains(searchString)
                                       || s.Content.Contains(searchString)
                                       || s.Submitter.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    issues = issues.OrderByDescending(i => i.SubmitDate);
                    break;
                case "Status":
                    issues = issues.OrderBy(i => i.Status);
                    break;
                case "status_desc":
                    issues = issues.OrderByDescending(i => i.Status);
                    break;
                default:
                    issues = issues.OrderBy(i => i.SubmitDate);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(issues.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin, Agent")]
        public ActionResult All(string sortOrder, string currentFilter, string searchString, int? selectedItem, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            ViewBag.CurrentFilter = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            

            var issues = from issue in db.Issues
                         select issue;
            if (!String.IsNullOrEmpty(searchString))
            {
                issues = issues.Where(s => s.Subject.Contains(searchString)
                                       || s.Content.Contains(searchString)
                                       || s.Submitter.UserName.Contains(searchString));
            }
            
            switch (sortOrder)
   {
      case "date_desc":
         issues = issues.OrderByDescending(i => i.SubmitDate);
         break;
      case "Status":
         issues = issues.OrderBy(i => i.Status);
         break;
      case "status_desc":
         issues = issues.OrderByDescending(i => i.Status);
         break;
      default:
         issues = issues.OrderBy(i => i.SubmitDate);
         break;
   }
            if (selectedItem == null)
            {
                selectedItem = issues.ToList()[0].ID;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            if (selectedItem == null)
            {
                selectedItem = issues.First().ID;
            }
            ViewBag.SelectedItem = selectedItem;

            return View(issues.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin, Agent")]
        public ActionResult AllOpen(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.PrioritySortParm = sortOrder == "Priority" ? "priority_desc" : "Priority";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var issues = from issue in db.Issues
                         where issue.Status == IssueStatus.open
                         select issue;

            if (!String.IsNullOrEmpty(searchString))
            {
                issues = issues.Where(s => s.Subject.Contains(searchString)
                                       || s.Content.Contains(searchString)
                                       || s.Submitter.UserName.Contains(searchString));
            }
            

            switch (sortOrder)
            {
                case "date_desc":
                    issues = issues.OrderByDescending(i => i.SubmitDate);
                    break;
                case "Priority":
                    issues = issues.OrderBy(i => i.Priority);
                    break;
                case "priority_desc":
                    issues = issues.OrderByDescending(i => i.Priority);
                    break;
                default:
                    issues = issues.OrderBy(i => i.SubmitDate);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(issues.ToPagedList(pageNumber, pageSize));
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
            if (issue.Submitter.Id != currentUserID && userManager.IsInRole(currentUserID, "User"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(issue);
        }
        [ChildActionOnly]
        public ActionResult IssueDetails(int? id)
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
            return PartialView("IssueDetails", issue);
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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string addComment)
        {
            var currentUserID = User.Identity.GetUserId();
            var currentUser = manager.FindById(currentUserID);

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var issueToUpdate = db.Issues.Find(id);

            if (issueToUpdate.Submitter.Id != currentUserID && userManager.IsInRole(currentUserID, "User"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            issueToUpdate.LastModifiedBy = currentUser;
            

            if (TryUpdateModel(issueToUpdate, ""))
            {
                try
                {
                    
                    var comment = new Comment();
                    comment.Date = DateTime.Now;
                    comment.Description = addComment;
                    comment.IssueID = issueToUpdate.ID;
                    comment.Submitter = currentUser;
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Edit");
                }
                catch (Exception ex)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(issueToUpdate);
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
            if (issue.Submitter.Id != currentUserID && !userManager.IsInRole(currentUserID, "Admin"))
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