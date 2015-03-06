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
    public class SoftwareItemsController : BaseController<SoftwareItem>
    {
        protected sealed override String getControler()
        {
            return "SoftwareItems";
        }

        protected sealed override DbSet<SoftwareItem> getPrimaryTable()
        {
            return db.SoftwareItems;
        }

        protected sealed override SoftwareItem addSelectLists(SoftwareItem model)
        {
            model.AssignEquipment = buildAssignment(db.Equipments, model.Equipments, model, "AddEquipment");
            return model;
        }

        // GET: SoftwareItems
        public ActionResult Index()
        {
            return View(db.SoftwareItems.ToList());
        }

        // POST: SoftwareItems/AddEquipment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEquipment(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.Equipments, model => model.Equipments);
        }

        // POST: SoftwareItems/RemoveEquipment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveEquipment(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.Equipments);
        }
    }
}
