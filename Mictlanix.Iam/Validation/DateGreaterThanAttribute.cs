//
// DateGreaterThanAttribute.cs
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
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mictlanix.Iam.Models.Validation
{
    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {
        const string default_error_message = "'{0}' must be greater than '{1}'";
        string property_name;

        public DateGreaterThanAttribute(string basePropertyName)
            : base(default_error_message)
        {
            property_name = basePropertyName;
        }

        //Override default FormatErrorMessage Method
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, property_name);
        }

        //Override IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get PropertyInfo Object
            var basePropertyInfo = validationContext.ObjectType.GetProperty(property_name);

            //Get Value of the property
            var startDate = (DateTime?)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var attrs = basePropertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
            string property = attrs.Length == 0 ? property_name : ((DisplayAttribute)attrs[0]).GetName();

            //Actual comparision
            if (value == null || (DateTime)value <= startDate)
            {
                var message = string.Format(ErrorMessageString, validationContext.DisplayName, property);
                return new ValidationResult(message);
            }

            //Default return - This means there were no validation error
            return null;
        }

    }


}
