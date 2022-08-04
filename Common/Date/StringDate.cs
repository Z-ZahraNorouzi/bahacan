using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Date
{
    public class StringDate
    {
        private const string dateFormat = "yyyy/MM/dd";
        private const string fullYear = "1300";

        public static string Now
        {
            get
            {
                return FromDateTime(DateTime.Now);
            }
        }
        public static string NowDateTime
        {
            get
            {
                return FromDateTime(DateTime.Now, "yyyy/MM/dd HH:mm");
            }
        }

        public static string FromDateTime(DateTime date, string format = "yyyy/MM/dd")
        {
            return date.ToFaString(format);

        }

        public static DateTime ToDateTime(string date)
        {
            var parts = date.Split('/'); //ex. 1391/1/19
            if (parts.Length != 3)
                return new DateTime();
            int year, month, day;
            var index = getIndexes(dateFormat);

            if (int.TryParse(parts[index.Year], out year) && int.TryParse(parts[index.Month], out month) && int.TryParse(parts[index.Day], out day))
                return new DateTime(year, month, day, new System.Globalization.PersianCalendar());
            return new DateTime();
        }

        public static string Normalize(string date)
        {
            if (string.IsNullOrEmpty(date))
                return date;
            var parts = date.Split('/'); //ex. 1391/1/19
            if (parts.Length != 3)
                return "";
            var index = getIndexes(dateFormat);
            if (parts[index.Year].Length == 4 && parts[index.Month].Length == 2 && parts[index.Day].Length == 2)
                return date;

            if (parts[index.Year].Length > 4 || parts[index.Month].Length > 2 || parts[index.Day].Length > 2 || parts[index.Year].Length == 0 || parts[index.Month].Length == 0 || parts[index.Day].Length == 0)
                return "";

            string result = "{0}/{1}/{2}";
            string yearPart = fullYear.Substring(0, 4 - parts[index.Year].Length) + parts[index.Year];
            string monthPart = (parts[index.Month].Length == 1 ? "0" : "") + parts[index.Month];
            string dayPart = (parts[index.Day].Length == 1 ? "0" : "") + parts[index.Day];

            result = result.Replace("{" + index.Year + "}", yearPart);
            result = result.Replace("{" + index.Month + "}", monthPart);
            result = result.Replace("{" + index.Day + "}", dayPart);
            return result;
        }

        private static Index getIndexes(string date)
        {
            var formatParts = date.Split('/').ToList();
            var yearFormat = formatParts.First(x => x.ToLower().IndexOf('y') != -1);
            var monthFormat = formatParts.First(x => x.ToLower().IndexOf('m') != -1);
            var dayFormat = formatParts.First(x => x.ToLower().IndexOf('d') != -1);
            return new Index()
            {
                Year = formatParts.IndexOf(yearFormat),
                Month = formatParts.IndexOf(monthFormat),
                Day = formatParts.IndexOf(dayFormat)
            };
        }

        class Index
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
        }
    }
}
