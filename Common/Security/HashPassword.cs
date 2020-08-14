using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Helpers;
using System.Web;
using Common.Enumeration;

namespace Common.Security
{
    public class HashPassword
    {
        public string HashText(string text, HashAlorightm hashAlorightm= HashAlorightm.MD5)
        {
            string result = "";
            if (hashAlorightm == HashAlorightm.MD5)
            {
                //string NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(MyPass, "MD5");
                //return HashPassword(text);
            }
            if (hashAlorightm == HashAlorightm.SHA1)
            {
                result= Crypto.SHA1(text);
            }
            if (hashAlorightm == HashAlorightm.SHA256)
            {
                result = Crypto.SHA256(text);
            }
            if (hashAlorightm == HashAlorightm.RFC_2898)
            {
                result = Crypto.HashPassword(text);
            }
            return result;
        }
    }
}
