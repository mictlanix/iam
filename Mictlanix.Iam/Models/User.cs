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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mictlanix.Iam.Properties;

namespace Mictlanix.Iam.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(20, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "UserName", ResourceType = typeof(Resources))]
        public string UserName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FirstName", ResourceType = typeof(Resources))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "LastName", ResourceType = typeof(Resources))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string LastName { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [StringLength(250, MinimumLength = 6, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Email { get; set; }

        [Display(Name = "IsAdministrator", ResourceType = typeof(Resources))]
        public bool IsAdministrator { get; set; }

        [Display(Name = "AllowAlerts", ResourceType = typeof(Resources))]
        public bool AllowAlerts { get; set; }

        [Display(Name = "AllowReadSchools", ResourceType = typeof(Resources))]
        public bool AllowReadSchools { get; set; }

        [Display(Name = "AllowCreateSchools", ResourceType = typeof(Resources))]
        public bool AllowCreateSchools { get; set; }

        [Display(Name = "AllowEditSchools", ResourceType = typeof(Resources))]
        public bool AllowEditSchools { get; set; }

        [Display(Name = "AllowDeleteSchools", ResourceType = typeof(Resources))]
        public bool AllowDeleteSchools { get; set; }

        [Display(Name = "AllowReadOrganizations", ResourceType = typeof(Resources))]
        public bool AllowReadOrganizations { get; set; }

        [Display(Name = "AllowCreateOrganizations", ResourceType = typeof(Resources))]
        public bool AllowCreateOrganizations { get; set; }

        [Display(Name = "AllowEditOrganizations", ResourceType = typeof(Resources))]
        public bool AllowEditOrganizations { get; set; }

        [Display(Name = "AllowDeleteOrganizations", ResourceType = typeof(Resources))]
        public bool AllowDeleteOrganizations { get; set; }

        [Display(Name = "AllowReadRequests", ResourceType = typeof(Resources))]
        public bool AllowReadRequests { get; set; }

        [Display(Name = "AllowCreateRequests", ResourceType = typeof(Resources))]
        public bool AllowCreateRequests { get; set; }

        [Display(Name = "AllowEditRequests", ResourceType = typeof(Resources))]
        public bool AllowEditRequests { get; set; }

        [Display(Name = "AllowDeleteRequests", ResourceType = typeof(Resources))]
        public bool AllowDeleteRequests { get; set; }

        [Display(Name = "AllowReadArrangements", ResourceType = typeof(Resources))]
        public bool AllowReadArrangements { get; set; }

        [Display(Name = "AllowCreateArrangements", ResourceType = typeof(Resources))]
        public bool AllowCreateArrangements { get; set; }

        [Display(Name = "AllowEditArrangements", ResourceType = typeof(Resources))]
        public bool AllowEditArrangements { get; set; }

        [Display(Name = "AllowDeleteArrangements", ResourceType = typeof(Resources))]
        public bool AllowDeleteArrangements { get; set; }
    }
}