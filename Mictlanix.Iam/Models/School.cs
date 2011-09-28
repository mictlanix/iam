// 
// School.cs
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
using System.Data.Entity.ModelConfiguration;
using Mictlanix.Iam.Properties;
using Mictlanix.Iam.Models.Validation;
using DataAnnotationsExtensions;


namespace Mictlanix.Iam.Models
{
    public enum TypeEnum
    {
        [Display(Name = "SchoolType_None", ResourceType = typeof(Resources))]
        None,
        [Display(Name = "SchoolType_HighSchool", ResourceType = typeof(Resources))]
        HighSchool,
        [Display(Name = "SchoolType_College", ResourceType = typeof(Resources))]
        College,
        [Display(Name = "SchoolType_University", ResourceType = typeof(Resources))]
        University,
    }
    public class School
    {
        [Key]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Número de School")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(250, MinimumLength = 2, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Name", ResourceType= typeof(Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(40, MinimumLength = 3, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ShortName", ResourceType = typeof(Resources))]
        public string ShortName { get; set; }

        [Display(Name = "Street", ResourceType = typeof(Resources))]
        [StringLength(250, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Street { get; set; }

        [Display(Name = "ExteriorNumber", ResourceType = typeof(Resources))]
        [StringLength(15, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string ExteriorNumber { get; set; }

        [Display(Name = "InteriorNumber", ResourceType = typeof(Resources))]
        [StringLength(15, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string InteriorNumber { get; set; }

        [Display(Name = "Neighborhood", ResourceType = typeof(Resources))]
        [StringLength(150, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Neighborhood { get; set; }

        [Display(Name = "Borough", ResourceType = typeof(Resources))]
        [StringLength(150, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Borough { get; set; }

        [Display(Name = "State", ResourceType = typeof(Resources))]
        [StringLength(80, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string State { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Resources))]
        [StringLength(80, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Country { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Resources))]
        [StringLength(5, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string ZipCode { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Resources))]
        [StringLength(45, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Phone { get; set; }

        [Display(Name = "PhoneExt", ResourceType = typeof(Resources))]
        [StringLength(5, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string PhoneExt { get; set; }

        [Email(ErrorMessageResourceName = "Validation_Email", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [StringLength(80, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Email { get; set; }

        [Url(ErrorMessageResourceName = "Validation_Url", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Website", ResourceType = typeof(Resources))]
        [StringLength(80, MinimumLength = 1, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Website { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "TypeSchool", ResourceType = typeof(Resources))]
        public int Type { get; set; }

        [NotMapped]
        public TypeEnum TypeEnum { get { return (TypeEnum)Type; } }
    }
}