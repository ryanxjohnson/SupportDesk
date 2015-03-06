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
    public class SupportGroupsController : BaseController<SupportGroup>
    {
        protected sealed override String getControler()
        {
            return "SupportGroups";
        }

        protected sealed override DbSet<SupportGroup> getPrimaryTable()
        {
            return db.SupportGroups;
        }

        protected sealed override SupportGroup addSelectLists(SupportGroup model)
        {
            model.chooseOverseer_Employee_ID = buildSelectList(db.Employees, null, model.Overseer_Employee_ID);
            model.AssignEmployee = buildAssignment(db.Employees, model.Employees, model, "AddEmployee");
            return model;
        }

        // GET: SupportGroups
        public ActionResult Index()
        {
            return View(db.SupportGroups.Include(s => s.Employee).ToList());
        }

        // POST: SoftwareItems/AddEmployee/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.Employees, model => model.Employees);
        }

        // POST: SoftwareItems/RemoveEmployee/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveEmployee(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.Employees);
        }
    }
}
