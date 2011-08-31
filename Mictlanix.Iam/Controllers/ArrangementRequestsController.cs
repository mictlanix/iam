// 
// ArrangementRequestsController.cs
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
    public class ArrangementRequestsController : Controller
    {
        SSContext db = new SSContext();

        //
        // GET: /ArrangementRequests/

        public ViewResult Index()
        {
            return View(db.ArrangementRequests.ToList());
        }

        // POST: /Products/Index

        [HttpPost]
        public ActionResult Index(Search search)
        {
            if (ModelState.IsValid)
            {
                int id;
                bool use_id;

                use_id = int.TryParse(search.Pattern, out id);

                var qry = from x in db.ArrangementRequests
                          where x.Organization.Contains(search.Pattern) ||
                                x.School.Contains(search.Pattern) ||
                                (use_id && x.Id == id)
                          select x;

                return View(qry.Take(100).ToList());
            }

            return View(new List<ArrangementRequest>());
        }

        //
        // GET: /ArrangementRequests/Details/5

        public ViewResult Details(int id)
        {
            ArrangementRequest revisionarreglo = db.ArrangementRequests.Find(id);
            return View(revisionarreglo);
        }

        //
        // GET: /ArrangementRequests/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ArrangementRequests/Create

        [HttpPost]
        public ActionResult Create(ArrangementRequest revisionarreglo)
        {
            if (ModelState.IsValid)
            {
                db.ArrangementRequests.Add(revisionarreglo);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(revisionarreglo);
        }
        
        //
        // GET: /ArrangementRequests/Edit/5
 
        public ActionResult Edit(int id)
        {
            ArrangementRequest revisionarreglo = db.ArrangementRequests.Find(id);
            return View(revisionarreglo);
        }

        //
        // POST: /ArrangementRequests/Edit/5

        [HttpPost]
        public ActionResult Edit(ArrangementRequest revisionarreglo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revisionarreglo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(revisionarreglo);
        }

        //
        // GET: /ArrangementRequests/Delete/5
 
        public ActionResult Delete(int id)
        {
            ArrangementRequest revisionarreglo = db.ArrangementRequests.Find(id);
            return View(revisionarreglo);
        }

        //
        // POST: /ArrangementRequests/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ArrangementRequest revisionarreglo = db.ArrangementRequests.Find(id);
            db.ArrangementRequests.Remove(revisionarreglo);
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