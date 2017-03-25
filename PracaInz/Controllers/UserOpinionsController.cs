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
    public class UserOpinionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserOpinions
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var opuzytkownika = db.Opinions.Where(a => a.UserId == userid);
            // var opinions = db.Opinions.Include(o => o.Car).Include(o => o.User);
            if (opuzytkownika.Count() == 0)
            {
                return View("NoOpinions");
            }
            return View(opuzytkownika.ToList());
        }

        // GET: UserOpinions/Details/5
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

        // GET: UserOpinions/Create
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model");
         //   ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Imie");
            return View();
        }

        // POST: UserOpinions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarId,UserId,Opinia,Ocena")] Opinion opinion)
        {
            if (ModelState.IsValid)
            {
                db.Opinions.Add(opinion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", opinion.CarId);
        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Imie", opinion.UserId);
            return View(opinion);
        }

        // GET: UserOpinions/Edit/5
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
        //    ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Imie", opinion.UserId);
            return View(opinion);
        }

        // POST: UserOpinions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarId,UserId,Opinia,Ocena")] Opinion opinion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opinion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", opinion.CarId);
       //     ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Imie", opinion.UserId);
            return View(opinion);
        }

        // GET: UserOpinions/Delete/5
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

        // POST: UserOpinions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
