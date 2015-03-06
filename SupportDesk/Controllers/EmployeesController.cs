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
    public class EmployeesController : BaseController<Employee>
    {
        protected sealed override String getControler()
        {
            return "Employees";
        }

        protected sealed override DbSet<Employee> getPrimaryTable()
        {
            return db.Employees;
        }

        protected sealed override Employee addSelectLists(Employee model)
        {
            model.chooseDepartment_ID = buildSelectList(db.Departments, null, model.Department_ID);
            model.AssignSupportGroup = buildAssignment(db.SupportGroups, model.SupportGroups1, model, "AddSupportGroup");
            model.AssignTask = buildAssignment(db.Tasks, model.Tasks1, model, "AddTask");
            return model;
        }

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.Include(e => e.Department).ToList());
        }

        // POST: Employees/AddSupportGroup/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSupportGroup(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.SupportGroups, model => model.SupportGroups1);
        }

        // POST: Employees/RemoveSupportGroup/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveSupportGroup(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.SupportGroups1);
        }

        // POST: Employees/AddTask/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.Tasks, model => model.Tasks1);
        }

        // POST: Employees/RemoveTask/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveTask(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.Tasks1);
        }
    }
}
