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
    public class TempsController : Controller
    {
        private Rab_BdEntities db = new Rab_BdEntities();

        // GET: Temps
        public ActionResult Index()
        {
            var temp = db.Temp.Include(t => t.Group).Include(t => t.Organizat).Include(t => t.Sotrudnick);
            return View(temp.ToList());
        }

        // GET: Temps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temp temp = db.Temp.Find(id);
            if (temp == null)
            {
                return HttpNotFound();
            }
            return View(temp);
        }

        // GET: Temps/Create
        public ActionResult Create()
        {
            ViewBag.Id_Group = new SelectList(db.Group, "Id_Group", "Name_Group");
            ViewBag.Id_Org = new SelectList(db.Organizat, "Id_Org", "Name_Org");
            ViewBag.Id_Stud = new SelectList(db.Sotrudnick, "Id_Sotrud", "Name");
            return View();
        }

        // POST: Temps/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Group,Id_Stud,Id_temp,Id_Org")] Temp temp)
        {
            if (ModelState.IsValid)
            {
                db.Temp.Add(temp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Group = new SelectList(db.Group, "Id_Group", "Name_Group", temp.Id_Group);
            ViewBag.Id_Org = new SelectList(db.Organizat, "Id_Org", "Name_Org", temp.Id_Org);
            ViewBag.Id_Stud = new SelectList(db.Sotrudnick, "Id_Sotrud", "Name", temp.Id_Stud);
            return View(temp);
        }

        // GET: Temps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temp temp = db.Temp.Find(id);
            if (temp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Group = new SelectList(db.Group, "Id_Group", "Name_Group", temp.Id_Group);
            ViewBag.Id_Org = new SelectList(db.Organizat, "Id_Org", "Name_Org", temp.Id_Org);
            ViewBag.Id_Stud = new SelectList(db.Sotrudnick, "Id_Sotrud", "Name", temp.Id_Stud);
            return View(temp);
        }

        // POST: Temps/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Group,Id_Stud,Id_temp,Id_Org")] Temp temp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Group = new SelectList(db.Group, "Id_Group", "Name_Group", temp.Id_Group);
            ViewBag.Id_Org = new SelectList(db.Organizat, "Id_Org", "Name_Org", temp.Id_Org);
            ViewBag.Id_Stud = new SelectList(db.Sotrudnick, "Id_Sotrud", "Name", temp.Id_Stud);
            return View(temp);
        }

        // GET: Temps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temp temp = db.Temp.Find(id);
            if (temp == null)
            {
                return HttpNotFound();
            }
            return View(temp);
        }

        // POST: Temps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Temp temp = db.Temp.Find(id);
            db.Temp.Remove(temp);
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
