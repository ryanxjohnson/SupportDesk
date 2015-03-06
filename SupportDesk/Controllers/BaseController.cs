using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using SupportDesk.Models;
using System.Data.Entity.Infrastructure;

namespace SupportDesk.Controllers
{
    public abstract class BaseController<M> : Controller where M : class, HasID, new()
    {
        protected Entities db = new Entities();

        protected abstract String getControler();

        protected abstract DbSet<M> getPrimaryTable();

        protected Assignment buildAssignment<T>(IEnumerable<T> source, IEnumerable<T> except, M model, String action)
            where T : class, Selectable
        {
            return new Assignment()
            {
                Controller = getControler(),
                Action = action,
                ID = model.ID,
                selectList = buildSelectList(source, except, (long?)null)
            };
        }

        protected abstract M addSelectLists(M model);

        protected List<SelectListItem> buildSelectList<T>(IEnumerable<T> source, IEnumerable<T> except, String s_id)
            where T : class, Selectable
        {
            long? s_id_long = null;
            try
            {
                s_id_long = long.Parse(s_id);
            }
            catch (Exception) { }
            return buildSelectList(source, except, s_id_long);
        }

        protected List<SelectListItem> buildSelectList<T>(IEnumerable<T> source, IEnumerable<T> except, long? s_id)
            where T : class, Selectable
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (T item in except != null ? source.ToList().Except(except) : source)
            {
                list.Add(item.toSelectListItem(s_id));
            }
            return list;
        }

        // GET
        protected ActionResult DetailsInternal(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ID = id;
            M model = getPrimaryTable().Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(addSelectLists(model));
        }

        // GET: Tasks/Details/5
        public ActionResult Details(long? id)
        {
            return DetailsInternal(id);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View(addSelectLists(new M()));
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(M model)
        {
            if (ModelState.IsValid)
            {
                getPrimaryTable().Add(processCreate(model));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addSelectLists(model));
        }

        protected virtual M processCreate(M model) { return model; }

        // GET: Tasks/Edit/5
        public ActionResult Edit(long? id)
        {
            return DetailsInternal(id);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(M model)
        {
            if (ModelState.IsValid)
            {
                DbEntityEntry<M> entry = db.Entry(model);
                entry.State = EntityState.Modified;
                processEdit(entry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addSelectLists(model));
        }

        protected virtual void processEdit(DbEntityEntry<M> entry) { }

        // GET: Tasks/Delete/5
        public ActionResult Delete(long? id)
        {
            return DetailsInternal(id);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
            if (ModelState.IsValid)
            {
                DbSet<M> primaryTable = getPrimaryTable();
                M model = primaryTable.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                processDelete(model);
                primaryTable.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected virtual void processDelete(M model) { }

        protected ActionResult AddAssignment<T>(long? id, String s_id,
            DbSet<T> otherTable, Func<M, ICollection<T>> getList)
            where T : class, Selectable
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M model = getPrimaryTable().Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (s_id != null && ModelState.IsValid)
                {
                    T item = otherTable.Find(long.Parse(s_id));
                    if (item == null)
                    {
                        return HttpNotFound();
                    }
                    getList(model).Add(item);
                    db.SaveChanges();
                }
            }
            catch (FormatException) { }

            return RedirectToAction("Edit", new { id = id });
        }

        protected ActionResult RemoveAssignment<T>(long? id, String s_id, Func<M, ICollection<T>> getList)
            where T : class, HasID
        {
            try
            {
                long s_id_long = long.Parse(s_id);
                ICollection<T> list = getList(getPrimaryTable().Find(id));
                list.Remove(list.Single(x => x.ID.Equals(s_id_long)));
                db.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("Edit", new { id = id });
        }

        protected sealed override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}