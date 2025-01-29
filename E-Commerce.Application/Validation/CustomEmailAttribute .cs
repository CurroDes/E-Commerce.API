using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_Commerce.Application.Validation
{
    public class CustomEmailAttribute : ValidationAttribute
    {
        // Expresión regular para validar un correo electrónico con un formato más estricto
        private static readonly Regex EmailRegex = new Regex(
            @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*))@((?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?|((\d{1,3}\.){3}\d{1,3}))$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var email = value.ToString();

            if (!EmailRegex.IsMatch(email))
            {
                return new ValidationResult("El formato del correo electrónico no es válido.");
            }

            return ValidationResult.Success;
        }
    }
}
