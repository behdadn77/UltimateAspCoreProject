
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Common.Enumeration
{
    public static class Enumeration
    {
        public static List<SelectListItem> GetSelectValueEnum<T>(string selectedValue) where T : struct, IConvertible
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var eVal in Enum.GetValues(typeof(T)))
            {
                list.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(T), eVal).Replace("_", " "),
                    Value = ((byte)eVal).ToString(),
                    Selected = (eVal.ToString() == selectedValue) ? true : false
                });
            }

            return list;
        }
        public static List<string> GetAll<T>() where T : struct, IConvertible
        {
            List<string> list = new List<string>();
            foreach (var eVal in Enum.GetValues(typeof(T)))
            {
                list.Add(Enum.GetName(typeof(T), eVal).Replace("_", " "));
            }
            return list;
        }
    }
    
}
