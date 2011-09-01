using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mictlanix.Iam.Models;

namespace Mictlanix.Iam.Controllers
{ 
    public class UsersController : Controller
    {
        private SSContext db = new SSContext();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            //if (!Request.IsAuthenticated ||
            //    !Helpers.HtmlHelpers.GetUser(null, User.Identity.Name).IsAdministrator)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(string id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}