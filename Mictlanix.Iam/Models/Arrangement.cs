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
    public class Arrangement
    {
        [Key]
        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Número de Expedientes")]
        [DisplayFormat(DataFormatString = "CV11{0:000}")]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Entrada")]
        public DateTime? FechaEntrada { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Dependencia Politécnica")]
        public string Dependencia { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Contraparte")]
        public string Contraparte { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Objeto del Convenio")]
        public string Objeto { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Monto")]
        [Column(TypeName = "money")]
        public decimal Monto { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Responsable Técnico")]
        public string ResponsableTecnico { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Firma")]
        [DateGreaterThan("FechaEntrada", ErrorMessageResourceName = "Validation_DateGreaterThan", ErrorMessageResourceType = typeof(Resources))]
        public DateTime? FechaFirma { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]        
        [Display(Name = "Vigencia del Convenio")]
        [DateGreaterThan("FechaFirma", ErrorMessageResourceName = "Validation_DateGreaterThan", ErrorMessageResourceType = typeof(Resources))]
        public DateTime? VigenciaConvenio { get; set; }

        [Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Tipo de Instrumento Jurídico")]
        public string Tipo { get; set; }
    }
}