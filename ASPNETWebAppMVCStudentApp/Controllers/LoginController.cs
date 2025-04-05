using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETWebAppMVCStudentApp;

public class LoginController : Controller
{
    private SchoolDBEntities db = new SchoolDBEntities();

    // GET: /Login
    public ActionResult Index()
    {
        return View();
    }

    // POST: /Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Index(string username, string password)
    {
        if (ModelState.IsValid)
        {
            var user = db.tblUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Successful login
                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                return RedirectToAction("Index", "Courses"); // Redirect to Courses page
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
            }
        }
        return View();
    }
}