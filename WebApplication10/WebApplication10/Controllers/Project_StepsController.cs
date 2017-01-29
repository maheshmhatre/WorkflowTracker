using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication10;

namespace WebApplication10.Controllers
{
    public class Project_StepsController : ApplicationBaseController
    {
        private mrmhatreDataBaseEntities db = new mrmhatreDataBaseEntities();

        // GET: Project_Steps
        public ActionResult Index(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                // initialize class values
                GlobalVariables.ProjectIdStatic = id;
              
                Project projectObject = new Project();
                projectObject = db.Projects.Single(proj => proj.ProjectId == id);
                ViewBag.ProjectName = projectObject.ProjectName;
                return View(db.Project_Steps.Where(proj => proj.ProjectId == id).ToList());
            }
            else
            {
                RedirectToAction("Login", "Account");
            }
            return View();
        }

        // GET: Project_Steps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_Steps project_Steps = db.Project_Steps.Find(id);
            if (project_Steps == null)
            {
                return HttpNotFound();
            }
            GlobalVariables.StepNameStatic = project_Steps.StepName;
            return View(project_Steps);
        }

        // GET: Project_Steps/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report(string reportName)
        {
            mrmhatreDataBaseEntities db = new mrmhatreDataBaseEntities();
            if (reportName != null)
            {
                Comment comment = new Comment();
                comment.RowId = db.Comments.Count() + 1;
                comment.ProjectId = GlobalVariables.ProjectIdStatic;
                comment.StepName = GlobalVariables.StepNameStatic;
                comment.CommentText = reportName;
                comment.Commentor = User.Identity.Name;
                comment.Timestamp = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Index",new { id = GlobalVariables.ProjectIdStatic });
        }

        // POST: Project_Steps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RowId,ProjectId,StepName,StartDateEstimated,StartDateActual,EndDateEstimated,EndDateActual,LastModified,Status")] Project_Steps project_Steps)
        {
            if (ModelState.IsValid)
            {
                db.Project_Steps.Add(project_Steps);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project_Steps);
        }

        // GET: Project_Steps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_Steps project_Steps = db.Project_Steps.Find(id);
            GlobalVariables.ProjectIdStatic = project_Steps.ProjectId;
            GlobalVariables.StepNameStatic = project_Steps.StepName;
            if (project_Steps == null)
            {
                return HttpNotFound();
            }
            return View(project_Steps);
        }

        // POST: Project_Steps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                Project_Steps projSteps = new Project_Steps();
                mrmhatreDataBaseEntities dc = new mrmhatreDataBaseEntities();
                projSteps = dc.Project_Steps.Single( id => id.ProjectId == GlobalVariables.ProjectIdStatic  && id.StepName == GlobalVariables.StepNameStatic);
                projSteps.StartDateEstimated = Convert.ToDateTime(formCollection["StartDateEstimated"]);
                projSteps.StartDateActual = Convert.ToDateTime(formCollection["StartDateActual"]);
                projSteps.EndDateEstimated = Convert.ToDateTime(formCollection["EndDateEstimated"]);
                projSteps.EndDateActual = Convert.ToDateTime(formCollection["EndDateActual"]);
                projSteps.LastModified = DateTime.Now;
                projSteps.Status = formCollection["Status"];
                dc.SaveChanges();
                return View(projSteps);
            }
            return RedirectToAction("Index", GlobalVariables.ProjectIdStatic);
        }

        // GET: Project_Steps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_Steps project_Steps = db.Project_Steps.Find(id);
            if (project_Steps == null)
            {
                return HttpNotFound();
            }
            return View(project_Steps);
        }

        // POST: Project_Steps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project_Steps project_Steps = db.Project_Steps.Find(id);
            db.Project_Steps.Remove(project_Steps);
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
