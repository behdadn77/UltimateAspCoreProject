using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace DataAnnotation.User
{
    public class PasswordAttribute : DataTypeAttribute
    {
        private const DataType dataType = DataType.Password;
        private readonly bool isRequired;
        private readonly bool isRegistration = false;
        private readonly int minLength;
        private readonly int maxLenght;
        private readonly int reqDegit;
        private readonly int reqUppercase;
        private readonly int reqLowerCase;
        private readonly int reqSpecialChar;
        private readonly bool allowWhiteSpace;
        private Regex Format;

        public PasswordAttribute(bool isRequired = false) : base(dataType)
        {
            this.isRequired = isRequired;
        }
        public PasswordAttribute(bool isRequired = false, bool isRegistration = false, int minLength = 5, int maxLength = 30, int reqDegit = 0,
            int reqUppercase = 0, int reqLowerCase = 0, int reqSpecialChar = 0, bool allowWhiteSpace = true) : base(dataType)
        {
            this.isRequired = isRequired;
            this.isRegistration = isRegistration;
            this.minLength = minLength;
            this.maxLenght = maxLength;
            this.reqDegit = reqDegit;
            this.reqUppercase = reqUppercase;
            this.reqLowerCase = reqLowerCase;
            this.reqSpecialChar = reqSpecialChar;
            this.allowWhiteSpace = allowWhiteSpace;
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
            if (isRegistration)
            {
                bool meetsLengthRequirements = strValue.Length >= minLength && strValue.Length <= maxLenght;
                int upperCaseLetter = 0;
                int lowerCaseLetter = 0;
                int digit = 0;
                bool haswhiteSpace = false;
                int specialChar = 0;

                foreach (char c in strValue)
                {
                    if (char.IsUpper(c)) upperCaseLetter++;
                    else if (char.IsLower(c)) lowerCaseLetter++;
                    else if (char.IsDigit(c)) digit++;
                    else if (char.IsWhiteSpace(c)) haswhiteSpace = true;
                    else specialChar++;
                }

                if (meetsLengthRequirements &&
                    upperCaseLetter >= reqUppercase &&
                    lowerCaseLetter >= reqLowerCase &&
                    digit >= reqDegit &&
                    haswhiteSpace == allowWhiteSpace &&
                    specialChar >= reqSpecialChar)
                {
                    return ValidationResult.Success;
                }

                errorMessage = "رمز عبور باید شامل ";

                string min = "حداقل";
                string max = "حداکثر";
                string and = "و";
                string lowerCase = "کارکتر با حروف کوچک";
                string upperCase = "کارکتر با حروف بزرگ";
                string dig = "رقم";
                string special = "کارکتر ویژه مانند !@";


                if (!meetsLengthRequirements)
                    errorMessage += $"{min} {minLength} {and} {max} {maxLenght} ";
                if (upperCaseLetter < reqUppercase)
                    errorMessage += $"{min} {reqUppercase} {upperCase} ";
                if (lowerCaseLetter < reqLowerCase)
                    errorMessage += $"{min} {reqLowerCase} {lowerCase} ";
                if (digit < reqDegit)
                    errorMessage += $"{min} {reqDegit} {dig} ";
                if (!allowWhiteSpace && haswhiteSpace)
                    errorMessage += "بدون فاصله ";
                if (specialChar < reqSpecialChar)
                    errorMessage += $"{min} {reqSpecialChar} {special} ";

                errorMessage += "باشد ";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }

        bool IsEmpty(string value) => String.IsNullOrEmpty(value);
    }
}
