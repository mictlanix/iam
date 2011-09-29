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
using Mictlanix.Iam.Properties;
using Mictlanix.Iam.Models.Validation;

namespace Mictlanix.Iam.Models
{
    public enum StatusEnum 
    {
        [Display(Name = "Status01", ResourceType = typeof(Resources))]
        Status01 = 1,
        [Display(Name = "Status02", ResourceType = typeof(Resources))]
        Status02,
        //[Display(Name = "Status03", ResourceType = typeof(Resources))]
        //Status03,
        //[Display(Name = "Status04", ResourceType = typeof(Resources))]
        //Status04,
        [Display(Name = "Status05", ResourceType = typeof(Resources))]
        Status05 = 5,
        [Display(Name = "Status06", ResourceType = typeof(Resources))]
        Status06,
        //[Display(Name = "Status07", ResourceType = typeof(Resources))]
        //Status07,
        [Display(Name = "Status08", ResourceType = typeof(Resources))]
        Status08 = 8,
        [Display(Name = "Status09", ResourceType = typeof(Resources))]
        Status09,
        [Display(Name = "Status10", ResourceType = typeof(Resources))]
        Status10,
        //[Display(Name = "Status11", ResourceType = typeof(Resources))]
        //Status11,
        //[Display(Name = "Status12", ResourceType = typeof(Resources))]
        //Status12,
        [Display(Name = "Status13", ResourceType = typeof(Resources))]
        Status13 =13,
        //[Display(Name = "Status14", ResourceType = typeof(Resources))]
        //Status14,
        [Display(Name = "Status15", ResourceType = typeof(Resources))]
        Status15 = 15,
        [Display(Name = "Status16", ResourceType = typeof(Resources))]
        Status16 
    }

    public class ArrangementStatus
    {
        [Key]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Folio", ResourceType = typeof(Resources))]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date", ResourceType = typeof(Resources))]
        public DateTime Date { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Status", ResourceType = typeof(Resources))]
        public int Status { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment", ResourceType = typeof(Resources))]
        [StringLength(500, MinimumLength = 0, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string Comment { get; set; }

        [ForeignKey("Creator")]
        [Display(Name = "Creator", ResourceType = typeof(Resources))]
        [StringLength(20, MinimumLength = 4, ErrorMessageResourceName = "Validation_StringLength", ErrorMessageResourceType = typeof(Resources))]
        public string CreatorId { get; set; }

        [ForeignKey("Arrangement"), Column(Order = 0)]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        public int ArrangementYear { get; set; }

        [ForeignKey("Arrangement"), Column(Order = 1)]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        public int ArrangementSerial { get; set; }


        public virtual User Creator { get; set; }

        [Display(Name = "Arrangement", ResourceType = typeof(Resources))]
        public virtual Arrangement Arrangement { get; set; }

        [NotMapped]
        public StatusEnum StatusEnum { get { return (StatusEnum)Status; } }

        [NotMapped]
        [Display(Name = "Session", ResourceType = typeof(Resources))]
        public string Session { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [Display(Name = "SessionDate", ResourceType = typeof(Resources))]
        public DateTime? SessionDate { get; set; }

    }
}