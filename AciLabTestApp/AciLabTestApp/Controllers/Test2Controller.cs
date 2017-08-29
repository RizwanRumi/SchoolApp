using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AciLabTestApp;

namespace AciLabTestApp.Controllers
{
    public class Test2Controller : Controller
    {
        private StudentDBEntities db = new StudentDBEntities();

        // GET: /Test2/
        public ActionResult Index()
        {
            return View(db.tblLogins.ToList());
        }

        // GET: /Test2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLogin tbllogin = db.tblLogins.Find(id);
            if (tbllogin == null)
            {
                return HttpNotFound();
            }
            return View(tbllogin);
        }

        // GET: /Test2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Test2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="loginId,StudentName,StudentEmail,Password")] tblLogin tbllogin)
        {
            if (ModelState.IsValid)
            {
                db.tblLogins.Add(tbllogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbllogin);
        }

        // GET: /Test2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLogin tbllogin = db.tblLogins.Find(id);
            if (tbllogin == null)
            {
                return HttpNotFound();
            }
            return View(tbllogin);
        }

        // POST: /Test2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="loginId,StudentName,StudentEmail,Password")] tblLogin tbllogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbllogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbllogin);
        }

        // GET: /Test2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLogin tbllogin = db.tblLogins.Find(id);
            if (tbllogin == null)
            {
                return HttpNotFound();
            }
            return View(tbllogin);
        }

        // POST: /Test2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLogin tbllogin = db.tblLogins.Find(id);
            db.tblLogins.Remove(tbllogin);
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
