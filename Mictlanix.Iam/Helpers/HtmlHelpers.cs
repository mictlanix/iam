﻿// 
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Mictlanix.Iam.Models;

namespace Mictlanix.Iam.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html,
                                                string imagePath,
                                                string action,
                                                object routeValues)
        {
            return ActionImage(html, imagePath, action, null, routeValues, null);
        }

        public static MvcHtmlString ActionImage(this HtmlHelper html,
                                                string imagePath,
                                                string action,
                                                object routeValues,
                                                object htmlAttributes)
        {
            return ActionImage(html, imagePath, action, null, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionImage(this HtmlHelper html,
                                                string imagePath,
                                                string action,
                                                string controller,
                                                object routeValues,
                                                object htmlAttributes)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");

            imgBuilder.MergeAttribute("src", url.Content(imagePath));

            if (htmlAttributes != null)
            {
                imgBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }

            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
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
            string display_name = Enum.GetName(member.GetType(), member);

            var prop_info = member.GetType().GetField(display_name);
            var attrs = prop_info.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attrs.Count() != 0)
                display_name = ((DisplayAttribute)attrs[0]).GetName();

            return display_name;
        }

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
            return db.Schools.OrderBy(x => x.ShortName).ToList();
        }

        public static List<Organization> GetOrganizations(this HtmlHelper helper)
        {
            SSContext db = new SSContext();
            return db.Organizations.OrderBy(x => x.ShortName).ToList();
        }
    }
}