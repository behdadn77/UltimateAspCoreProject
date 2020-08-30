using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAnnotation.User
{
    public class FirstNameAttribute : DataTypeAttribute
    {
        public FirstNameAttribute(DataType dataType) : base(dataType)
        {
        }
    }
}
