using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Helpers;

namespace Common.HtmlHelper
{
    public class ConvertDateTime
    {
        public static object DateConverter(DateTime? dateTime = null, bool isHelper = true)
        {
            dateTime = dateTime != null ? dateTime : DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(dateTime.Value);
            var month = persianCalendar.GetMonth(dateTime.Value);
            var day = persianCalendar.GetDayOfMonth(dateTime.Value);

            string Date = year.ToString() + "/" + month.ToString() + "/" + day.ToString();
            if (isHelper)
            {
                string Html = $"{Date}";
                return new HtmlString(Html);
            }
            return Date;
        }
        public static HtmlString ConvertGregorianDateToPersian(DateTime? dateTime = null)
        {

            dateTime = dateTime != null ? dateTime : DateTime.Now;

            PersianCalendar persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(dateTime.Value);
            var month = persianCalendar.GetMonth(dateTime.Value);
            var day = persianCalendar.GetDayOfMonth(dateTime.Value);

            string Date = year.ToString() + "/" + month.ToString() + "/" + day.ToString();

            String Html = $"{Date}";
            return new HtmlString(Html);
        }
        public static HtmlString ConvertPersianDateToGregoian(string persianDate)
        {
            DateTime date = Convert.ToDateTime(persianDate, new CultureInfo("fa-IR"));
            return new HtmlString(date.ToString());
        }
    }
}
