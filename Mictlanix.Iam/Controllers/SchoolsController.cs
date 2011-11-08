// 
// SchoolsController.cs
// 
// Author:
//   Eddy Zavaleta <eddy@mictlanix.org>
//   Eduardo Nieto <enieto@mictlanix.org>
// 
// Copyright (C) 2011 Eddy Zavaleta, Mictlanix (http://www.mictlanix.org)
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

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
    public class SchoolsController : Controller
    {
        private SSContext db = new SSContext();

        //
        // GET: /Schools/

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated ||
                !Helpers.HtmlHelpers.GetUser(null, User.Identity.Name).AllowReadSchools)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(db.Schools.OrderBy(x => x.ShortName).ToList());
        }

        //
        // GET: /Schools/Details/5

        public ViewResult Details(int id)
        {
            School Schools = db.Schools.Find(id);
            return View(Schools);
        }

        //
        // GET: /Schools/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Schools/Create

        [HttpPost]
        public ActionResult Create(School Schools)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(Schools);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(Schools);
        }
        
        //
        // GET: /Schools/Edit/5
 
        public ActionResult Edit(int id)
        {
            School Schools = db.Schools.Find(id);
            return View(Schools);
        }

        //
        // POST: /Schools/Edit/5

        [HttpPost]
        public ActionResult Edit(School Schools)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Schools).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Schools);
        }

        //
        // GET: /Schools/Delete/5
 
        public ActionResult Delete(int id)
        {
            School Schools = db.Schools.Find(id);
            return View(Schools);
        }

        //
        // POST: /Schools/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                School item = db.Schools.Find(id);
                db.Schools.Remove(item);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return View("DeleteUnsuccessful");
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}