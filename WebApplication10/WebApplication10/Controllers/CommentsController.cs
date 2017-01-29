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
    public class CommentsController : ApplicationBaseController
    {
        private mrmhatreDataBaseEntities db = new mrmhatreDataBaseEntities();

        //// GET: Comments
        //public ActionResult Index(int? projectId,string stepName)
        //{
        //    return View(db.Comments.Where(sname =>sname.ProjectId ==projectId  &&  sname.StepName == stepName).ToList());
        //}

        public ActionResult Index(string stepName)
        {
            return View(db.Comments.Where(sname => sname.StepName == stepName).ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();
                comment.RowId = db.Comments.Count() + 1;
                int num = Convert.ToInt32(formCollection["ProjectId"]);
                comment.ProjectId = Convert.ToInt32(formCollection["ProjectId"]);
                comment.StepName = formCollection["StepName"];
                comment.CommentText = formCollection["CommentText"];
                comment.Commentor = User.Identity.Name;
                comment.Timestamp = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Index",new {stepName = formCollection["StepName"] });
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RowId,ProjectId,StepId,CommentText,Commentor,Timestamp")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
