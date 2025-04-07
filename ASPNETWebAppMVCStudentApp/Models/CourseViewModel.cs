using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETWebAppMVCStudentApp.Models
{
    public class CourseViewModel
    {
        public int SelectedCourseID { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
    public class DepartmentViewModel
    {
        public int SelectedDepartmentID { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}