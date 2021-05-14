using System;
using System.ComponentModel.DataAnnotations;

namespace MMO_RPG
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if ((DateTime.Now - date).Ticks < 0)
            {
                return new ValidationResult("CreationTime Must Be In The Past");
            }

            return ValidationResult.Success;
        }
    }
}