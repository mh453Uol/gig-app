using Gig.Models.GigsViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Helper.Validation
{
    public class FutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            bool isValid = DateTime.TryParseExact(value.ToString(),
                "dd MMM yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out date);

            if (isValid & date > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            
            return new ValidationResult("Enter a date which is in the future e.g " +
                DateTime.Now.AddDays(10).ToString("dd MMM yyyy"));
        }

        private static bool MergeAttribute(IDictionary<string,string> attributes, 
            string key, string value)
        {
            if (attributes.ContainsKey(key)) return false;
            attributes.Add(key, value); return true;
        }
    }
}
