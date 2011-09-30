// 
// HtmlHelpers.cs
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
using System.Data;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Mictlanix.Iam.Helpers
{
    public static class HtmlHelpers
    {
        public static User GetUser(this HtmlHelper helper, string username)
        {
            SSContext db = new SSContext();
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }

        public static List<User> GetUsers(this HtmlHelper helper)
        {
            SSContext db = new SSContext();
            return db.Users.OrderBy(x => x.FirstName).OrderBy(x => x.LastName).ToList();
        }

        public static List<School> GetSchools(this HtmlHelper helper)
        {
            SSContext db = new SSContext();
            return db.Schools.OrderBy(x => x.Name).ToList();
        }

        public static List<Organization> GetOrganizations(this HtmlHelper helper)
        {
            SSContext db = new SSContext();
            return db.Organizations.OrderBy(x => x.Name).ToList();
        }

        public static string GetMenuClass(this HtmlHelper html, string controller)
        {
            string ctl = html.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            return ctl == controller ? "gbz0l" : string.Empty;
        }

        public static string GetMenuClass(this HtmlHelper html, string controller, string action)
        {
            string ctl = html.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string atn = html.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
            return ctl == controller && atn == action ? "gbz0l" : string.Empty;
        }

        public static string GetDisplayName(this Enum member)
        {
            string display_name = member.ToString();

            var prop_info = member.GetType().GetField(display_name);
            var attrs = prop_info.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attrs.Count() != 0)
                display_name = ((DisplayAttribute)attrs[0]).GetName();

            return display_name;
        }
    }
}