using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAnnotation.Social
{
    public class TelephoneAttribute : ValidationAttribute
    {
        private readonly bool isRequired;
        private readonly string region = null;
        private PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRequired">Checks if the value is empty</param>
        /// <param name="region">Specify a region default is IR not required if number starts with country code (null)</param>
        public TelephoneAttribute(bool isRequired = false, string region = "IR")
        {
            this.isRequired = isRequired;
            this.region = region;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = "";
            string strValue = value as string;
            PhoneNumber phoneNum;
            try
            {
                phoneNum = phoneNumberUtil.Parse(strValue, region);
            }
            catch (Exception)
            {
                errorMessage = "فرمت شماره صحیح نیست";
                return new ValidationResult(errorMessage);
            }

            if (isRequired)
            {
                if (IsEmpty(strValue))
                {
                    errorMessage = "شماره تلفن نمیتواند خالی باشد";
                    return new ValidationResult(errorMessage);
                }
            }

            if (!phoneNumberUtil.IsValidNumber(phoneNum))
            {
                errorMessage = "فرمت شماره صحیح نیست";
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }

        bool IsEmpty(string value) => String.IsNullOrEmpty(value);

    }
}
