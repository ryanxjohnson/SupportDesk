using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupportDesk.Models;

namespace SupportDesk.Controllers
{
    public class TasksController : BaseController<Task>
    {
        protected sealed override String getControler()
        {
            return "Tasks";
        }

        protected sealed override DbSet<Task> getPrimaryTable()
        {
            return db.Tasks;
        }

        protected sealed override Task addSelectLists(Task model)
        {
            model.chooseSubmitter_Employee_ID = buildSelectList(db.Employees, null, model.Submitter_Employee_ID);
            model.chooseAssigned_Group_ID = buildSelectList(db.SupportGroups, null, model.Assigned_Group_ID);
            model.AssignEmployee = buildAssignment(db.Employees, model.Employees, model, "AddEmployee");
            model.AssignEquipment = buildAssignment(db.Equipments, model.Equipments, model, "AddEquipment");
            return model;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            return View(db.Tasks.Include(t => t.Employee).Include(t => t.SupportGroup).ToList());
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public sealed override ActionResult Create([Bind(Include = "ID,Status,Summary,Due,Priority,Notes,Submitter_Employee_ID,Assigned_Group_ID")] Task model)
        {
            return base.Create(model);
        }

        protected sealed override Task processCreate(Task model)
        {
            model.Submitted = DateTime.Now;
            return model;
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public sealed override ActionResult Edit([Bind(Include = "ID,Status,Summary,Due,Priority,Notes,Submitter_Employee_ID,Assigned_Group_ID")] Task model)
        {
            return base.Edit(model);
        }

        protected sealed override void processEdit(DbEntityEntry<Task> entry)
        {
            entry.Property(x => x.Submitted).IsModified = false;
        }

        // POST: Tasks/AddEmployee/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.Employees, model => model.Employees);
        }

        // POST: Tasks/RemoveEmployee/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveEmployee(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.Employees);
        }

        // POST: Tasks/AddEquipment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEquipment(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.Equipments, model => model.Equipments);
        }

        // POST: Tasks/RemoveEquipment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveEquipment(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.Equipments);
        }
    }
}
