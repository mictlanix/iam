// 
// Arrangement.cs
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
using System.Data.Entity;
using Mictlanix.Iam.Properties;
using Mictlanix.Iam.Models.Validation;

namespace Mictlanix.Iam.Models
{
    public class Arrangement
    {
        [Key, Column(Order = 0)]
        public int Year { get; set; }

        [Key, Column(Order = 1)]
        public int Serial { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [Display(Name = "ReceiptDate", ResourceType = typeof(Resources))]
        public DateTime ReceiptDate { get; set; }

        [ForeignKey("School")]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "Validation_WrongId", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "School", ResourceType = typeof(Resources))]
        public int SchoolId { get; set; }

        [ForeignKey("Organization")]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, int.MaxValue)]
        [Display(Name = "Organization", ResourceType = typeof(Resources))]
        public int OrganizationId { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Object", ResourceType = typeof(Resources))]
        public string Object { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Amount", ResourceType = typeof(Resources))]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Responsible", ResourceType = typeof(Resources))]
        [StringLength(250, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Responsible { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [Display(Name = "SignatureDate", ResourceType = typeof(Resources))]
        //[DateGreaterThan("ReceiptDate", ErrorMessageResourceName = "Validation_DateGreaterThan", ErrorMessageResourceType = typeof(Resources))]
        public DateTime? SignatureDate { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [Display(Name = "ValidFrom", ResourceType = typeof(Resources))]
        //[DateGreaterThan("SignatureDate", ErrorMessageResourceName = "Validation_DateGreaterThan", ErrorMessageResourceType = typeof(Resources))]
        public DateTime? ValidFrom { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [Display(Name = "ExpiryDate", ResourceType = typeof(Resources))]
        [DateGreaterThan("ValidFrom", ErrorMessageResourceName = "Validation_DateGreaterThan", ErrorMessageResourceType = typeof(Resources))]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Session", ResourceType = typeof(Resources))]
        [StringLength(250, MinimumLength = 2, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Session { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "SessionDate", ResourceType = typeof(Resources))]
        public DateTime? SessionDate { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Type", ResourceType = typeof(Resources))]
        [StringLength(250, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Type { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment", ResourceType = typeof(Resources))]
        [StringLength(1000, MinimumLength = 0, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Comment { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Status", ResourceType = typeof(Resources))]
        public int Status { get; set; }

        [ForeignKey("AssignedTo")]
        public string AssignedToId { get; set; }

        [Display(Name = "AssignedTo", ResourceType = typeof(Resources))]
        public virtual User AssignedTo { get; set; }
        public virtual School School { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<ArrangementStatus> Statuses { get; set; }

        [NotMapped]
        [Display(Name = "Serial", ResourceType = typeof(Resources))]
        public string SerialNumber { get { return string.Format("CV{0:00}{1:000}", Year.ToString().Substring(Year.ToString().Length - 2), Serial); } }

        [NotMapped]
        public StatusEnum StatusEnum { get { return (StatusEnum)Status; } }
    }
}