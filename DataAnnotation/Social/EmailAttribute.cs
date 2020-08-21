using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace DataAnnotation.Social
{
    public class EmailAttribute : DataTypeAttribute
    {

        private const DataType dataType = DataType.EmailAddress;
        private readonly bool isRequired;
        private Regex Format = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public EmailAttribute(bool isRequired = false) : base(dataType)
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
                    errorMessage = "آدرس ایمیل را وارد نمایید";
                    return new ValidationResult(errorMessage);
                }
            }
            if (!(Format.Match(strValue)).Success)
            {
                errorMessage = "فرمت آدرس ایمیل صحیح نمیباشد";
                return new ValidationResult(errorMessage);
            }


            return ValidationResult.Success;
        }

        bool IsEmpty(string value) => String.IsNullOrEmpty(value);

    }
}
