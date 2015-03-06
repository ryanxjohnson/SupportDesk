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
    public class EquipmentsController : BaseController<Equipment>
    {
        protected sealed override String getControler()
        {
            return "Equipments";
        }

        protected sealed override DbSet<Equipment> getPrimaryTable()
        {
            return db.Equipments;
        }

        protected sealed override Equipment addSelectLists(Equipment model)
        {
            model.chooseDepartment_ID = buildSelectList(db.Departments, null, model.Department_ID);
            model.chooseAssignedTo_Employee_ID = buildSelectList(db.Employees, null, model.AssignedTo_Employee_ID);
            model.chooseType_ID = buildSelectList(db.EquipmentTypes, null, model.Type_ID);
            model.AssignTask = buildAssignment(db.Tasks, model.Tasks, model, "AddTask");
            model.AssignSoftwareItem = buildAssignment(db.SoftwareItems, model.SoftwareItems, model, "AddSoftwareItem");
            return model;
        }

        // GET: Equipments
        public ActionResult Index()
        {
            return View(db.Equipments.Include(e => e.Department).Include(e => e.Employee).Include(e => e.EquipmentType).ToList());
        }

        // POST: Equipments/AddTask/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.Tasks, model => model.Tasks);
        }

        // POST: Equipments/RemoveTask/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveTask(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.Tasks);
        }

        // POST: Equipments/AddSoftwareItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSoftwareItem(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.SoftwareItems, model => model.SoftwareItems);
        }

        // POST: Equipments/RemoveSoftwareItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveSoftwareItem(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.SoftwareItems);
        }
    }
}
