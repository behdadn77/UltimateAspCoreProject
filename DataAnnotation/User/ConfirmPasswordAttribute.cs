using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAnnotation.User
{
    public class ConfirmPasswordAttribute : CompareAttribute
    {
        public ConfirmPasswordAttribute(string otherProperty) : base(otherProperty)
        {

        }

        public override string FormatErrorMessage(string name)
        {
            return "رمز عبور و تکرار آن یکسان نمی باشد";
        }
    }
}
