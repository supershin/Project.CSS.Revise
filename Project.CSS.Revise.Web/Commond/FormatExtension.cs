﻿using Microsoft.VisualBasic;
using System.Globalization;

namespace Project.CSS.Revise.Web.Commond
{
    public static class FormatExtension
    {
        public static string StandardNumberFormat = "{0:#,##0.00}";
        public static string StandardNumberFormat2 = "#,##0";
        public static string StandardCulture = "en-US";
        public static string StandardDateFormat = "yyyy-MM-dd";
        public static string StandardDateTimeFormat = "yyyy-MM-dd HH:mm";
        public static Guid AsGuid(this Guid? value1)
        {
            return value1.HasValue ? value1.Value : Guid.Empty;
        }
        public static int AsInt(this int? value1)
        {
            return value1.HasValue ? value1.Value : 0;
        }
        public static long AsLong(this long? value1)
        {
            return value1.HasValue ? value1.Value : 0;
        }
        public static DateTime AsDate(this DateTime? value1)
        {
            return value1.HasValue ? value1.Value : DateTime.Now;
        }
        public static bool AsBool(this bool? value1)
        {
            return value1.HasValue ? value1.Value : false;
        }
        public static decimal AsDecimal(this decimal? value1)
        {
            return value1.HasValue ? value1.Value : 0;
        }

        public static int? ToInt(this string str)
        {
            int result;
            if (int.TryParse(str, out result))
                return result;

            return null;
        }
        public static DateTime? ToDate(this string str)
        {
            DateTime result;
            if (!string.IsNullOrEmpty(str.ToStringNullable()))
            {
                if (DateTime.TryParse(str, out result))
                    return result;
            }
            return null;
        }

        public static DateTime? ToDateFromddmmyyy(this string str)
        {
            if (!string.IsNullOrEmpty(str.ToStringNullable()))
            {
                DateTime result;

                // ลอง parse แบบกำหนด format ตรง ๆ
                if (DateTime.TryParseExact(
                    str.Trim(),
                    "dd/MM/yyyy",                          // ✅ รูปแบบวัน/เดือน/ปี
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out result))
                {
                    return result;
                }

                // fallback: ลอง parse แบบปกติ (สำหรับกรณีอื่น ๆ)
                if (DateTime.TryParse(str, out result))
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Converts a string in DD/MM/YYYY format to a string in YYYY-MM-DD format.
        /// </summary>
        /// <param name="str">The input date string in DD/MM/YYYY format.</param>
        /// <returns>The formatted date string in YYYY-MM-DD format, or an empty string if the input is invalid.</returns>
        public static string ToDateString(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime result;
                if (DateTime.TryParseExact(str, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                {
                    return result.ToString("yyyy-MM-dd"); // Convert to YYYY-MM-DD format
                }
            }
            return string.Empty; // Return an empty string for invalid input
        }

        public static string? ToStringNullable(this string? param)
        {
            return string.IsNullOrEmpty(param) ? null : param.Trim();
        }
        public static string ToStringEmpty(this string param)
        {
            return string.IsNullOrEmpty(param) ? string.Empty : param.Trim();
        }
        public static string ToStringNumber(this decimal number)
        {
            return string.Format(StandardNumberFormat, number);
        }
        public static string ToStringNumber(this long number)
        {
            return string.Format(StandardNumberFormat, number);
        }
        public static string? ToStringNumber(this decimal? number)
        {
            if (number == null)
            {
                return null;
            }
            return string.Format(StandardNumberFormat, number);
        }
        public static string ToStringNumber(this int number)
        {
            return number.ToString(StandardNumberFormat2);
        }
        public static string ToStringDateTime(this DateTime? dateValue)
        {
            var culture = CultureInfo.CreateSpecificCulture(StandardCulture);
            return dateValue.HasValue ? dateValue.Value.ToString(StandardDateTimeFormat, culture) : string.Empty;
        }
        public static string ToStringDateTime(this DateTime dateValue)
        {
            var culture = CultureInfo.CreateSpecificCulture(StandardCulture);
            return dateValue.ToString(StandardDateTimeFormat, culture);
        }
        public static string ToStringDate(this DateTime? dateValue)
        {

            var culture = CultureInfo.CreateSpecificCulture(StandardCulture);
            return dateValue.HasValue ? dateValue.Value.ToString(StandardDateFormat, culture) : string.Empty;
        }
        public static string ToStringDate(this DateTime dateValue)
        {

            var culture = CultureInfo.CreateSpecificCulture(StandardCulture);
            return dateValue.ToString(StandardDateFormat, culture);
        }
        public static string Right(this string sValue, int iMaxLength)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(sValue))
            {
                //Set valid empty string as string could be null
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                //Make the string no longer than the max length
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            //Return the string
            return sValue;
        }
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

        public static string NullTo2decimalplaces(object obj, string defaultString = "")
        {
            // Handle null or empty input
            if (obj == null || System.Convert.IsDBNull(obj) || obj.ToString().Trim() == "")
            {
                return defaultString;
            }

            if (double.TryParse(obj.ToString(), out double result))
            {
                if (result == 0)
                {
                    return ""; // Return empty string if the value is 0
                }

                // Check if it's a whole number
                if (result % 1 == 0)
                {
                    return ((int)result).ToString(); // Return as an integer if no decimal value
                }
                else
                {
                    return result.ToString("F2").TrimEnd('0').TrimEnd('.'); // Return up to two decimals
                }
            }

            // Return original value as string if not a valid number
            return obj.ToString();
        }


        /// <summary>
        /// NullToString
        /// คือ Function สำหรับแปลง Object ใดๆ เป็น string
        /// </summary>
        /// <param name="obj">Object ใดๆที่ต้องการเปลี่ยนเป็น string </param>
        /// <param name="defaultString"></param>
        /// <returns></returns>
        public static string NullToString(object obj, string defaultString = "")
        {
            return NullToAnyString(obj, defaultString);
        }
        public static string NullToAnyString(object obj, string defaultString = " - ")
        {
            string temp = defaultString;
            if (obj == null)
            {
                temp = defaultString;
            }
            else if (System.Convert.IsDBNull(obj))
            {
                temp = defaultString;
            }
            else if (obj.ToString().Trim() == "")
            {
                temp = defaultString;
            }
            else
            {
                temp = obj.ToString();
            }
            return temp;
        }
        /// <summary>
        /// คือ Function สำหรับแปลง Object ใดๆ เป็น int
        /// </summary>
        /// <param name="obj">Object ใดๆที่ต้องการเปลี่ยนเป็น int</param>
        /// <param name="defaultint"></param>
        /// <returns></returns>
        public static int Nulltoint(object obj, int defaultint = 0)
        {
            if (obj != null)
            { /* เผื่อสำหรับกรณีที่ส่งเข้ามาแบบ  1,123  ให้ตัดสตริง , ออกก่อน เพราะว่าเวลาไม่ตัดแล้ว convert ออกมาเป็น int จะได้ 0  */
                try { obj = obj.ToString().Replace(",", ""); } catch { }
            }

            int Temp = defaultint;
            if (obj == null)
            {
                Temp = defaultint;
            }
            else if (Information.IsDBNull(obj))
            {
                Temp = defaultint;
            }
            else if (obj.ToString().Trim() == "")
            {
                Temp = defaultint;
            }
            else if (Information.IsNumeric(obj.ToString()) == false)
            {
                Temp = defaultint;
            }
            else
            {
                int.TryParse(obj.ToString(), out Temp);
            }
            return Temp;
        }

        public static string ToStringFrom_DD_MM_YYYY_To_DD_MM_YYYY(object dateObject)
        {
            if (dateObject == null || dateObject == DBNull.Value)
            {
                return string.Empty;
            }

            if (DateTime.TryParse(dateObject.ToString(), out DateTime date))
            {
                return date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }

        public static Guid ConvertStringToGuid(object guidObject)
        {
            if (guidObject == null || guidObject == DBNull.Value)
            {
                return Guid.Empty;
            }

            if (Guid.TryParse(guidObject.ToString(), out Guid result))
            {
                return result;
            }

            return Guid.Empty;
        }

        public static string FormatDateToDayMonthNameYearTime(object dateObject)
        {
            if (dateObject == null || dateObject == DBNull.Value)
            {
                return string.Empty;
            }

            if (DateTime.TryParse(dateObject.ToString(), out DateTime date))
            {
                return date.ToString("dd-MMM-yyyy HH:mm", CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }

        public static string FormatDateToDayMonthNameYear(object dateObject)
        {
            if (dateObject == null || dateObject == DBNull.Value)
            {
                return string.Empty;
            }

            if (DateTime.TryParse(dateObject.ToString(), out DateTime date))
            {
                return date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }

        public static string FormatDateToThaiShortString(object dateObject)
        {
            if (dateObject == null || dateObject == DBNull.Value)
            {
                return string.Empty;
            }

            if (DateTime.TryParse(dateObject.ToString(), out DateTime date))
            {
                var thaiCulture = new CultureInfo("th-TH");

                // ตัวย่อวันในสัปดาห์
                string[] thaiDayShortNames = { "อา.", "จ.", "อ.", "พ.", "พฤ.", "ศ.", "ส." };
                string shortDayName = thaiDayShortNames[(int)date.DayOfWeek];

                int day = date.Day;
                string month = date.ToString("MMM", thaiCulture);
                int year = date.Year;

                return $"{shortDayName} {day} {month} {year}";
            }

            return string.Empty;
        }

    }
}
