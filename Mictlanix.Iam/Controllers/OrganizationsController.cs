// 
// OrganizationsController.cs
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
    public class OrganizationsController : Controller
    {
        private SSContext db = new SSContext();

        //
        // GET: /Organizations/

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated ||
                !Helpers.HtmlHelpers.GetUser(null, User.Identity.Name).AllowReadOrganizations)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(db.Organizations.ToList());
        }

        //
        // GET: /Organizations/Details/5

        public ViewResult Details(int id)
        {
            Organization empresa = db.Organizations.Find(id);
            return View(empresa);
        }

        //
        // GET: /Organizations/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Organizations/Create

        [HttpPost]
        public ActionResult Create(Organization empresa)
        {
            if (ModelState.IsValid)
            {
                db.Organizations.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(empresa);
        }
        
        //
        // GET: /Organizations/Edit/5
 
        public ActionResult Edit(int id)
        {
            Organization empresa = db.Organizations.Find(id);
            return View(empresa);
        }

        //
        // POST: /Organizations/Edit/5

        [HttpPost]
        public ActionResult Edit(Organization empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        //
        // GET: /Organizations/Delete/5
 
        public ActionResult Delete(int id)
        {
            Organization empresa = db.Organizations.Find(id);
            return View(empresa);
        }

        //
        // POST: /Organizations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Organization empresa = db.Organizations.Find(id);
            db.Organizations.Remove(empresa);
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