﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationV4.Attributes
{
    public class NameValidationAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public NameValidationAttribute(int minLength)
        {
            _minLength = minLength;
            ErrorMessage = "The name must be at least {0} characters long.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value is string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ValidationResult("Name is required!");
                }

                if (name.Length < _minLength)
                {
                    return new ValidationResult(string.Format(ErrorMessage, _minLength));
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid value!");
        }
    }
}
