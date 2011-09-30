// 
// ArrangementsController.cs
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
using System.Text.RegularExpressions;

namespace Mictlanix.Iam.Controllers
{ 
    public class ArrangementsController : Controller
    {
        private SSContext db = new SSContext();

        //
        // GET: /Arrangements/


        public ActionResult Index()
        {
            if (!Request.IsAuthenticated ||
                !Helpers.HtmlHelpers.GetUser(null, User.Identity.Name).AllowReadArrangements)
            {
                return RedirectToAction("Index", "Home");  
            }

            return View(new Search<Arrangement>());
        }

        // POST: /Arrangements/Index

        [HttpPost]
        public ActionResult Index(Search<Arrangement> search)
        {
            Search<Arrangement> result = new Search<Arrangement>();

            if (ModelState.IsValid)
            {
                int year = 0;
                int serial = 0;

                Match match = Regex.Match(search.Pattern, @"CV(\d{2})(\d{0,3})$", RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    year = 2000 + int.Parse(match.Groups[1].Value);
                    int.TryParse(match.Groups[2].Value, out serial);
                }

                var qry = from x in db.Arrangements
                          where x.Organization.Name.Contains(search.Pattern) ||
                                x.School.Name.Contains(search.Pattern) ||
                                (year > 0 && x.Year == year && (serial == 0 || x.Serial == serial))
                          select x;

                result.Results = qry.ToList();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Index", result);
            }
            else
            {
                return View(result);
            }
        }

        //
        // GET: /Arrangements/Details/5

        public ViewResult Details(int year, int serial)
        {
            Arrangement datosconvenio = db.Arrangements.Find(year, serial);
            return View(datosconvenio);
        }

        //
        // GET: /Arrangements/CreateFromRequest

        public ActionResult CreateFromRequest(int id)
        {
            ArrangementRequest request = db.ArrangementRequests.Find(id);
            return View("Create", new Arrangement {
                SchoolId = request.SchoolId, 
                School = request.School,
                OrganizationId = request.OrganizationId,
                Organization = request.Organization,
                ReceiptDate = DateTime.Today,
                SignatureDate = DateTime.Today,
                ValidFrom = DateTime.Today,
                ExpiryDate = DateTime.Today
            });
        } 

        //
        // POST: /Arrangements/Create

        [HttpPost]
        public ActionResult Create(Arrangement item)
        {
            if (ModelState.IsValid)
            {
                item.Status = (int)StatusEnum.Status01;
                item.Year = DateTime.Now.Year;

                try
                {
                    item.Serial = db.Arrangements.Where(x => x.Year == item.Year).Select(x => x.Serial).Max() + 1;
                }
                catch (Exception)
                {
                    item.Serial = 1;
                }

                db.Arrangements.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");  
            }

            return View(item);
        }
        
        //
        // GET: /Arrangements/Edit/5
 
        public ActionResult Edit(int year, int serial)
        {
            Arrangement item = db.Arrangements.Find(year, serial);
            return View(item);
        }

        //
        // POST: /Arrangements/Edit/5

        [HttpPost]
        public ActionResult Edit(Arrangement datosconvenio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datosconvenio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datosconvenio);
        }

        //
        // GET: /Arrangements/ChangeStatus/2011/1

        public ActionResult ChangeStatus(int year, int serial)
        {
            Arrangement arrangement = db.Arrangements.Find(year, serial);
            return View(new ArrangementStatus { ArrangementYear = year, ArrangementSerial = serial, Status = arrangement.Status});
        }

        //
        // POST: /Arrangements/ChangeStatus/2011/1

        [HttpPost]
        public ActionResult ChangeStatus(ArrangementStatus item)
        {
            if (ModelState.IsValid)
            {
                // guardar ArrangementStatus en la db 
                // cambiar estado al Arrangement
                Arrangement arrangement = db.Arrangements.Find(item.ArrangementYear, item.ArrangementSerial);
                //DateTime date = DateTime.Parse(Request.Params["SessionDate"]);

                if (item.StatusEnum == StatusEnum.Status09)
                {
                    arrangement.SessionDate = item.SessionDate;
                }

                if (item.StatusEnum == StatusEnum.Status10)
                {
                    arrangement.Session = item.Session;
                }

                arrangement.Status = item.Status;
                item.Arrangement = arrangement;
                item.Date = DateTime.Now;
                item.CreatorId = User.Identity.Name;

                db.Statuses.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(item);
        }

        //
        // GET: /Arrangements/Delete/5

        public ActionResult Delete(int year, int serial)
        {
            Arrangement datosconvenio = db.Arrangements.Find(year, serial);
            return View(datosconvenio);
        }

        //
        // POST: /Arrangements/Delete/5/

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int year, int serial)
        {            
            Arrangement datosconvenio = db.Arrangements.Find(year, serial);
            db.Arrangements.Remove(datosconvenio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: /Arrangement/Assign
        public ViewResult Assign(int year, int serial)
        {
            Arrangement arrangement = db.Arrangements.Find(year, serial);
            return View("Assign", new Assignment { Arrangement = arrangement, ArrangementYear = year, ArrangementSerial = serial});
        }

        //
        // POST: /Arrangement/Assign

        [HttpPost]
        public ActionResult Assign(Assignment item)
        {
            if (ModelState.IsValid)
            {
                Arrangement arrangement = db.Arrangements.Find(item.ArrangementYear, item.ArrangementSerial);
                arrangement.AssignedToId = item.AssignedToId;
                arrangement.AssignedTo = db.Users.Find(item.AssignedToId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}