using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : ApplicationBaseController
    {
        mrmhatreDataBaseEntities dc = new mrmhatreDataBaseEntities();
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                UserRole userRole = new UserRole();
                userRole = dc.UserRoles.SingleOrDefault(user => user.Email == User.Identity.Name);
                ViewBag.Role = userRole.Role;
                
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}