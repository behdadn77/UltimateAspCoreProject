using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAnnotation.User
{
    public class PasswordAttribute : DataTypeAttribute
    {
        private const DataType dataType = DataType.Password;
        private readonly bool isRequired;

        public PasswordAttribute(bool isRequired = false) : base(dataType)
        {
            this.isRequired = isRequired;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = "";
            string strValue = value as string;

            if (isRequired)
            {
                if (IsEmpty(strValue))
                {
                    errorMessage = "رمز عبور را وارد نمایید";
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }

        bool IsEmpty(string value) => String.IsNullOrEmpty(value);
    }
}
