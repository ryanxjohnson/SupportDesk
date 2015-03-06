using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupportDesk.Models;

namespace SupportDesk.Controllers
{
    public class DepartmentsController : BaseController<Department>
    {
        protected sealed override String getControler()
        {
            return "Departments";
        }

        protected sealed override DbSet<Department> getPrimaryTable()
        {
            return db.Departments;
        }

        protected sealed override Department addSelectLists(Department model)
        {
            return model;
        }

        // GET: Departments
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }
    }
}
