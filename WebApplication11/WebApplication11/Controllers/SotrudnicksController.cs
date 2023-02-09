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
    public class SotrudnicksController : Controller
    {
        private Rab_BdEntities db = new Rab_BdEntities();

        // GET: Sotrudnicks
        public ActionResult Index()
        {
            var sotrudnick = db.Sotrudnick.Include(s => s.Organizat);
            return View(sotrudnick.ToList());
        }

        // GET: Sotrudnicks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sotrudnick sotrudnick = db.Sotrudnick.Find(id);
            if (sotrudnick == null)
            {
                return HttpNotFound();
            }
            return View(sotrudnick);
        }

        // GET: Sotrudnicks/Create
        public ActionResult Create()
        {
            ViewBag.Org = new SelectList(db.Organizat, "Id_Org", "Name_Org");
            return View();
        }

        // POST: Sotrudnicks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Sotrud,Name,Org")] Sotrudnick sotrudnick)
        {
            if (ModelState.IsValid)
            {
                db.Sotrudnick.Add(sotrudnick);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Org = new SelectList(db.Organizat, "Id_Org", "Name_Org", sotrudnick.Org);
            return View(sotrudnick);
        }

        // GET: Sotrudnicks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sotrudnick sotrudnick = db.Sotrudnick.Find(id);
            if (sotrudnick == null)
            {
                return HttpNotFound();
            }
            ViewBag.Org = new SelectList(db.Organizat, "Id_Org", "Name_Org", sotrudnick.Org);
            return View(sotrudnick);
        }

        // POST: Sotrudnicks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Sotrud,Name,Org")] Sotrudnick sotrudnick)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sotrudnick).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Org = new SelectList(db.Organizat, "Id_Org", "Name_Org", sotrudnick.Org);
            return View(sotrudnick);
        }

        // GET: Sotrudnicks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sotrudnick sotrudnick = db.Sotrudnick.Find(id);
            if (sotrudnick == null)
            {
                return HttpNotFound();
            }
            return View(sotrudnick);
        }

        // POST: Sotrudnicks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sotrudnick sotrudnick = db.Sotrudnick.Find(id);
            db.Sotrudnick.Remove(sotrudnick);
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
