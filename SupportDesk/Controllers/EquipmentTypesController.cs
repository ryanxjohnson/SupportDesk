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
    public class EquipmentTypesController : BaseController<EquipmentType>
    {
        protected sealed override String getControler()
        {
            return "EquipmentTypes";
        }

        protected sealed override DbSet<EquipmentType> getPrimaryTable()
        {
            return db.EquipmentTypes;
        }

        protected sealed override EquipmentType addSelectLists(EquipmentType model)
        {
            List<EquipmentType> alreadyUserOfAndThis = model.EquipmentTypes.ToList();
            alreadyUserOfAndThis.Add(model);
            model.AssignEquipmentUserOf = buildAssignment(db.EquipmentTypes, alreadyUserOfAndThis, model, "AddEquipmentUserOf");
            return model;
        }

        // GET: EquipmentTypes
        public ActionResult Index()
        {
            return View(db.EquipmentTypes.ToList());
        }

        protected override void processDelete(EquipmentType model)
        {
            model.EquipmentType1.Clear();
        }

        // POST: EquipmentTypes/AddEquipmentUserOf/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEquipmentUserOf(int? id, [Bind(Include = "S_ID")] String s_id)
        {
            return AddAssignment(id, s_id, db.EquipmentTypes, model => model.EquipmentTypes);
        }

        // POST: EquipmentTypes/RemoveEquipmentUserOf/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveEquipmentUserOf(long? id, [Bind(Include = "S_ID")] String s_id)
        {
            return RemoveAssignment(id, s_id, model => model.EquipmentTypes);
        }
    }
}
