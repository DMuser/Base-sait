using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class GroupsController : Controller
    {
        private Rab_BdEntities db = new Rab_BdEntities();

        // GET: Groups
        public ActionResult Index()
        {
            var group = db.Group.Include(g => g.Kurs1).Include(g => g.Teacher1);
            return View(group.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.Kurs = new SelectList(db.Kurs, "Id_Kurs", "Name_Kurs");
            ViewBag.Teacher = new SelectList(db.Teacher, "Id_Teacher", "Name_teacher");
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Group,Name_Group,Kurs,Teacher")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Group.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Kurs = new SelectList(db.Kurs, "Id_Kurs", "Name_Kurs", group.Kurs);
            ViewBag.Teacher = new SelectList(db.Teacher, "Id_Teacher", "Name_teacher", group.Teacher);
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kurs = new SelectList(db.Kurs, "Id_Kurs", "Name_Kurs", group.Kurs);
            ViewBag.Teacher = new SelectList(db.Teacher, "Id_Teacher", "Name_teacher", group.Teacher);
            return View(group);
        }

        // POST: Groups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Group,Name_Group,Kurs,Teacher")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kurs = new SelectList(db.Kurs, "Id_Kurs", "Name_Kurs", group.Kurs);
            ViewBag.Teacher = new SelectList(db.Teacher, "Id_Teacher", "Name_teacher", group.Teacher);
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Group.Find(id);
            db.Group.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
