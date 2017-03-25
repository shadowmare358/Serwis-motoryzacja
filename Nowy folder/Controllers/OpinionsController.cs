using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracaInz.Models;
using Microsoft.AspNet.Identity;

namespace PracaInz.Controllers
{
    public class OpinionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Opinions
        public ActionResult Index(string search)
        {
            var opc = from s in db.Opinions
                           select s;
            //var averages = db.Averages.OrderByDescending(a => a.Srednia);
            if (!String.IsNullOrEmpty(search))
            {
                opc = opc.Where(s => s.Car.Model.Contains(search));
            }
           opc = db.Opinions.OrderBy(o => o.Car.Model);
          //  return View(opc.ToList());
            var userid = User.Identity.GetUserId();
            var opinions = db.Opinions.Include(o => o.Car);
            return View(opc.ToList());
        }

        // GET: Opinions/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opinion opinion = db.Opinions.Find(id);
            if (opinion == null)
            {
                return HttpNotFound();
            }
            return View(opinion);
        }

        // GET: Opinions/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {

            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model");
            return View();
        }

        // POST: Opinions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,CarId,Opinia,Ocena")] Opinion opinion)
        {
            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                opinion.UserId = userid;
                db.Opinions.Add(opinion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", opinion.CarId);
            return View(opinion);
        }

        // GET: Opinions/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opinion opinion = db.Opinions.Find(id);
            if (opinion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", opinion.CarId);
            return View(opinion);
        }

        // POST: Opinions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,CarId,Opinia,Ocena")] Opinion opinion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opinion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", opinion.CarId);
            return View(opinion);
        }

        // GET: Opinions/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opinion opinion = db.Opinions.Find(id);
            if (opinion == null)
            {
                return HttpNotFound();
            }
            return View(opinion);
        }

        // POST: Opinions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Opinion opinion = db.Opinions.Find(id);
            db.Opinions.Remove(opinion);
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
