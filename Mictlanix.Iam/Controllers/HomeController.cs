// 
// AccountController.cs
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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mictlanix.Iam.Models;

namespace Mictlanix.Iam.Controllers
{
    public class HomeController : Controller
    {
        SSContext db = new SSContext();

        ArrangementAlertRule[] alerts = 
        {
            new ArrangementAlertRule {
                Status = StatusEnum.Status01, 
                WarningDays = 3, 
                CriticalDays = 6, 
                Validation = ValidateStatusChange
            },
            new ArrangementAlertRule {
                Status = StatusEnum.Status02, 
                WarningDays = 3, 
                CriticalDays = 6, 
                Validation = ValidateStatusChange
            },
            new ArrangementAlertRule {
                Status = StatusEnum.Status05, 
                WarningDays = 3, 
                CriticalDays = 6, 
                Validation = ValidateStatusChange
            },
            new ArrangementAlertRule {
                Status = StatusEnum.Status06, 
                WarningDays = 60, 
                CriticalDays = 30,
                Validation = ValidateExpiryDate
            },
            new ArrangementAlertRule {
                Status = StatusEnum.Status08, 
                WarningDays = 60, 
                CriticalDays = 30,
                Validation = ValidateExpiryDate
            },
            new ArrangementAlertRule {
                Status = StatusEnum.Status09, 
                WarningDays = 15, 
                CriticalDays = 30, 
                Validation = ValidateStatusChange
            },
            new ArrangementAlertRule {
                Status = StatusEnum.Status13, 
                WarningDays = 15, 
                CriticalDays = 30, 
                Validation = ValidateStatusChange
            },
            new ArrangementAlertRule {
                Status = StatusEnum.Status15, 
                WarningDays = 30, 
                CriticalDays = 60, 
                Validation = ValidateStatusChange
            }
        };

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated ||
                !Helpers.HtmlHelpers.GetUser(null, User.Identity.Name).AllowAlerts)
            {
                return View();
            }

            List<Arrangement> normal = new List<Arrangement>();
            List<Arrangement> warning = new List<Arrangement>();
            List<Arrangement> critical = new List<Arrangement>();
            List<Arrangement> expired = new List<Arrangement>();
            List<Arrangement> valid = new List<Arrangement>();

            var list = from x in db.Arrangements
                       where x.ExpiryDate != null
                       select x;

            var qry = from x in db.Arrangements
                      where x.Status != (int)StatusEnum.Status10 && 
                            x.Status != (int)StatusEnum.Status16
                      select x;

            foreach (var item in qry.ToList())
            {
                ArrangementAlertRule alert = alerts.SingleOrDefault(x => x.Status == item.StatusEnum);

                if (alert != null && alert.Validation(item, alert.CriticalDays))
                {
                    critical.Add(item);
                }
                else if (alert != null && alert.Validation(item, alert.WarningDays))
                {
                    warning.Add(item);
                }
                else
                {
                    normal.Add(item);
                }
            }

            foreach (var item in list.ToList())
            {
                ArrangementAlertExpired expiry = new ArrangementAlertExpired {Valid = ValidateExpired };

                if (expiry.Valid(item))
                {
                    expired.Add(item);
                }
                else 
                {
                    valid.Add(item);
                }
            }

            return View("Alerts", new ArrangementAlerts 
                        { 
                            NormalList = normal, 
                            WarningList = warning, 
                            CriticalList = critical, 
                            ExpiredList = expired, 
                            ValidList = valid 
                        });
        }

        public ActionResult About()
        {
            return View();
        }
        
        public static bool ValidateStatusChange(Arrangement item, int days)
        {
            return (DateTime.Now - item.Statuses.Last().Date).Days >= days;
        }

        public static bool ValidateExpiryDate(Arrangement item, int days)
        {
            if (item.ExpiryDate == null)
                return true;

            return (item.ExpiryDate.Value - DateTime.Now).Days <= days;
        }

        public static bool ValidateExpired(Arrangement item)
        {
            return (item.ExpiryDate.Value - DateTime.Now).Days < 0;
        
        }
    }
}
