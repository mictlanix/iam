// 
// ArrangementRequest.cs
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

namespace Mictlanix.Iam.Models
{
    public class ArrangementRequest
    {
        [Key]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Folio", ResourceType = typeof(Resources))]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        [Display(Name = "Object", ResourceType = typeof(Resources))]
        public string Object { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "LegalRequirement", ResourceType = typeof(Resources))]
        public string LegalRequirement { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "RequestForm", ResourceType = typeof(Resources))]
        public bool RequestForm { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ElectronicMedia", ResourceType = typeof(Resources))]
        public bool ElectronicMedia { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Annexes", ResourceType = typeof(Resources))]
        public bool Annexes { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Forms", ResourceType = typeof(Resources))]
        public bool Forms { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "LegalDocuments", ResourceType = typeof(Resources))]
        public bool LegalDocuments { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ResponsibleStatement", ResourceType = typeof(Resources))]
        public bool ResponsibleStatement { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "SchoolAutorization", ResourceType = typeof(Resources))]
        public bool SchoolAutorization { get; set; }

        [ForeignKey("Creator")]
        //[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Creator", ResourceType = typeof(Resources))]
        public string CreatorId { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment", ResourceType = typeof(Resources))]
        [StringLength(500, MinimumLength = 0)]
        public string Comment { get; set; }

        public virtual User Creator { get; set; }
        public virtual School School { get; set; }
        public virtual Organization Organization { get; set; }

        [NotMapped]
        public bool IsCorrect
        {
            get
            {
                return RequestForm && 
                       ElectronicMedia && 
                       Annexes && Forms && 
                       LegalDocuments && 
                       ResponsibleStatement;
            }
        }

    }
}