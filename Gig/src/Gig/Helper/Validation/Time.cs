using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Helper.Validation
{
    public class Time: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime time;

            bool isValid = DateTime.TryParseExact(
                value.ToString(), 
                "HH:mm", 
                CultureInfo.CurrentCulture, 
                DateTimeStyles.None, 
                out time);

            if (isValid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Enter a valid time e.g 12:00");
        }
    }
}
