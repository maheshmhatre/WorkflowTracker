using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class ProjectController : ApplicationBaseController
    {
        // GET: Project
        public ActionResult Index()
        {
            string usr = User.Identity.Name;
            
            mrmhatreDataBaseEntities projectContext = new mrmhatreDataBaseEntities();
            UserRole userRole = new UserRole();
            userRole = projectContext.UserRoles.SingleOrDefault(u => u.Email == usr);
            List<Project> projectList;
            if (userRole.Role == "Admin")
            {
                projectList = projectContext.Projects.ToList();
            }
            else
            {
                projectList = projectContext.Projects.Where(proj => proj.CreatorEmail == usr).ToList();
            }
            return View(projectList);
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }
            return RedirectToAction("Index");
        }

        public ActionResult History(int id)
        {
            mrmhatreDataBaseEntities db = new mrmhatreDataBaseEntities();
            List<ProjectHistory> projHistory = db.ProjectHistories.Where(ids => ids.ProjectId == id).ToList();
            return View(projHistory);
        }

        public ActionResult Details(int id)
        {
            mrmhatreDataBaseEntities projectContext = new mrmhatreDataBaseEntities();
            Project project = projectContext.Projects.Single(proj => proj.ProjectId == id);
            return View(project);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            GlobalVariables.ProjectIdStatic = id;
            mrmhatreDataBaseEntities db = new mrmhatreDataBaseEntities();

            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Project proj = new Project();
            mrmhatreDataBaseEntities dc = new mrmhatreDataBaseEntities();
            proj = dc.Projects.Find(Convert.ToInt32(GlobalVariables.ProjectIdStatic));
            proj.ProjectEndDate = Convert.ToDateTime(formCollection["ProjectEndDate"]);
            proj.ActualProjectEndDate = Convert.ToDateTime(formCollection["ActualProjectEndDate"]);
            proj.Progress = Convert.ToInt32(formCollection["Progress"]);
            proj.Status = formCollection["Status"];
            dc.SaveChanges();

            ProjectHistory projectHistory = new ProjectHistory();
            projectHistory.RowId = dc.ProjectHistories.Count() + 1;
            projectHistory.ProjectId = Convert.ToInt32(GlobalVariables.ProjectIdStatic);
            projectHistory.ProjectStartDate = proj.ProjectStartDate;
            projectHistory.ProjectEndDate = Convert.ToDateTime(formCollection["ProjectEndDate"]);
            projectHistory.ActualProjectEndDate = Convert.ToDateTime(formCollection["ActualProjectEndDate"]);
            projectHistory.Status = formCollection["Status"];
            projectHistory.Progress = formCollection["Progress"];
            projectHistory.Timestamp = DateTime.Now;
            dc.ProjectHistories.Add(projectHistory);
            dc.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            ViewBag.Message = "Your application description page.";
            // get total nno of rows in project table
            int projectTableRowsCount = 0;

            using (mrmhatreDataBaseEntities dc = new mrmhatreDataBaseEntities())
            {
                Project project = new Project();
                projectTableRowsCount = dc.Projects.Count();
                project.ProjectId = projectTableRowsCount + 1;
                project.ProjectName = formCollection["ProjectName"];
                project.ProjectStartDate = Convert.ToDateTime(formCollection["ProjectStartDate"]);
                project.ProjectEndDate = Convert.ToDateTime(formCollection["ProjectEndDate"]);
                project.ActualProjectEndDate = Convert.ToDateTime(formCollection["ActualProjectEndDate"]);
                project.Status = formCollection["Status"];
                project.TimestampCreated = DateTime.Now;
                project.Progress = Convert.ToInt32(formCollection["Progress"]);
                project.CreatorEmail = User.Identity.Name;
                dc.Projects.Add(project);
                dc.SaveChanges();

                Step step = new Step();
                List<Step> steps = dc.Steps.Where(st => st.IsEnabled == true).ToList();

                foreach (var stepItem in steps)
                {
                    string name = stepItem.StepName;
                    Project_Steps projectSteps = new Project_Steps();
                    projectSteps.RowId = dc.Project_Steps.Count() + 1;
                    projectSteps.ProjectId = project.ProjectId;
                    projectSteps.StepName = name;
                    projectSteps.StartDateEstimated = null;
                    projectSteps.StartDateActual = null;
                    projectSteps.EndDateEstimated = null;
                    projectSteps.EndDateActual = null;
                    projectSteps.LastModified = DateTime.Now;
                    projectSteps.Status = null;
                    dc.Project_Steps.Add(projectSteps);
                    dc.SaveChanges();
                }

            }

            return RedirectToAction("Index");
            //  return View();
        }
    }
}