using Microsoft.Reporting.WebForms;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ASPNETWebAppMVCStudentApp.Models;

namespace ASPNETWebAppMVCStudentApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly SchoolDBEntities _context;

        public ReportController()
        {
            _context = new SchoolDBEntities(); // Your EF context
        }

        public ActionResult Index()
        {
            var model = new CourseViewModel
            {
                Courses = _context.Courses.Select(c => new SelectListItem
                {
                    Value = c.CourseID.ToString(),
                    Text = c.Title
                })
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Generate(int SelectedDepartmentID, string action)
        {
            var localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/CourseReport.rdlc");

            var courses = _context.Courses
                .Where(c => c.DepartmentID == SelectedDepartmentID)
                .Select(c => new
                {
                    c.CourseID,
                    c.Title,
                    c.Credits,
                    c.DepartmentID
                }).ToList();

            var reportDataSource = new ReportDataSource("CourseDataSet", courses);
            localReport.DataSources.Add(reportDataSource);

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;

            var renderedBytes = localReport.Render(
                reportType,
                null,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            if (action == "export")
            {
                return File(renderedBytes, "application/pdf", "CourseReport.pdf");
            }
            else // Preview
            {
                return File(renderedBytes, "application/pdf");
            }
        }
    }
}