using Microsoft.VisualBasic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection;


namespace Project.CSS.Revise.Web.Commond
{
    public static class FormatExtension
    {
        public static string StandardNumberFormat = "{0:#,##0.00}";
        public static string StandardNumberFormat2 = "#,##0";
        public static string StandardCulture = "en-US";
        public static string StandardDateFormat = "yyyy-MM-dd";
        public static string StandardDateTimeFormat = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// Copy matching property values from source to target.
        /// - Skips indexers
        /// - Handles null / Nullable<T> / enums / basic conversions
        /// - Set ignoreCase=true if you want case-insensitive matching
        /// </summary>
        public static void CopyTo(this object source, object target, bool ignoreCase = false)
        {
            if (source == null || target == null) return;

            var srcProps = source.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

            var trgProps = target.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetIndexParameters().Length == 0)
                .ToDictionary(
                    p => ignoreCase ? p.Name.ToLowerInvariant() : p.Name,
                    p => p
                );

            foreach (var sp in srcProps)
            {
                var key = ignoreCase ? sp.Name.ToLowerInvariant() : sp.Name;
                if (!trgProps.TryGetValue(key, out var tp)) continue;

                object val = sp.GetValue(source, null);
                if (val == null)
                {
                    // If target prop is value type (non-nullable), skip
                    if (Nullable.GetUnderlyingType(tp.PropertyType) != null)
                        tp.SetValue(target, null);
                    continue;
                }

                var targetType = Nullable.GetUnderlyingType(tp.PropertyType) ?? tp.PropertyType;

                try
                {
                    if (targetType.IsAssignableFrom(val.GetType()))
                    {
                        tp.SetValue(target, val);
                    }
                    else if (targetType.IsEnum)
                    {
                        object converted = val is string s
                            ? Enum.Parse(targetType, s, true)
                            : Enum.ToObject(targetType, val);
                        tp.SetValue(target, converted);
                    }
                    else
                    {
                        var converted = Convert.ChangeType(val, targetType, CultureInfo.InvariantCulture);
                        tp.SetValue(target, converted);
                    }
                }
                catch
                {
                    // swallow invalid conversions silently, as per your original behavior
                }
            }
        }

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

        /// <summary>
        /// แปลง object เป็น Guid อย่างปลอดภัย
        /// - null/DBNull/ค่าว่าง -> คืน defaultGuid (ค่าปริยาย Guid.Empty)
        /// - รับ Guid/Guid?/SqlGuid ตรง ๆ ได้
        /// - รับ string แล้ว Trim + ตัด {}, () และเครื่องหมาย quote ออกก่อน TryParse
        /// </summary>
        public static Guid NulltoGuid(object obj, Guid? defaultGuid = null)
        {
            var def = defaultGuid ?? Guid.Empty;

            if (obj == null || obj == DBNull.Value) return def;

            // Guid
            if (obj is Guid g) return g;

            // Guid? (boxed)
            if (obj is Guid?)
            {
                var ng = (Guid?)obj;
                if (ng.HasValue) return ng.Value;
                return def;
            }

            // SqlGuid
            if (obj is SqlGuid sg && !sg.IsNull) return sg.Value;

            // string
            var s = obj.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return def;

            // strip common wrappers
            s = s.Trim('{', '}', '(', ')', '\'', '"');

            return Guid.TryParse(s, out var parsed) ? parsed : def;
        }

        /// <summary>
        /// ConvertToShortNameUnit
        /// ใช้สำหรับแปลงค่าตัวเลขเป็นหน่วยย่อ เช่น 1,000 = 1.00 K, 1,000,000 = 1.00 MB
        /// </summary>
        /// <param name="obj">Object ที่เป็นตัวเลข</param>
        /// <param name="defaultString">ค่าที่คืนถ้า null หรือไม่ใช่ตัวเลข</param>
        /// <returns>string เช่น "16.85 M" หรือ "500"</returns>
        public static string ConvertToShortNameUnit(object obj, string defaultString = " - ")
        {
            if (obj is null) return defaultString;

            decimal number;

            switch (obj)
            {
                case decimal d: number = d; break;
                case double db: number = (decimal)db; break;
                case float f: number = (decimal)f; break;
                case long l: number = l; break;
                case int i: number = i; break;
                case string s:
                    var cleaned = s.Trim().Replace(",", "");
                    if (!decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out number) &&
                        !decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.CurrentCulture, out number))
                        return defaultString;
                    break;
                default:
                    var text = Convert.ToString(obj, CultureInfo.InvariantCulture);
                    if (!decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
                        return defaultString;
                    break;
            }

            var abs = Math.Abs(number);
            if (abs >= 1_000_000M)
            {
                // show millions with up to 2 decimals + " M"
                var m = number / 1_000_000M;
                return m.ToString("#,0.##", CultureInfo.InvariantCulture) + " M";
            }

            // below 1M: plain number, no decimals
            return number.ToString("#,0", CultureInfo.InvariantCulture);
        }

        public static string ConvertToShortUnit(object obj, string defaultString = "0")
        {
            string str = NullToAnyString(obj, defaultString);

            if (decimal.TryParse(str, out decimal number))
            {
                if (number >= 1_000_000)
                    return (number / 1_000_000M).ToString("0.##"); // No " MB"
                else if (number >= 1_000)
                    return (number / 1_000M).ToString("0.##");     // No " K"
                else
                    return number.ToString("0.##");
            }

            return defaultString;
        }

        //public static string ConvertToShortUnitV2(object obj, string type = "Value", string defaultString = "0")
        //{
        //    if (obj == null || obj == DBNull.Value) return defaultString;

        //    string str = NullToString(obj).Trim();
        //    decimal number;

        //    // parse safely (handle commas or dots)
        //    if (!decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
        //    {
        //        decimal.TryParse(str, NumberStyles.Any, CultureInfo.CurrentCulture, out number);
        //    }

        //    // if still invalid
        //    if (number == 0 && str != "0") return defaultString;

        //    if (type.Equals("Unit", StringComparison.OrdinalIgnoreCase))
        //    {
        //        // full number with thousand separator, no decimals
        //        return Math.Truncate(number).ToString("N0", CultureInfo.CurrentCulture);
        //    }
        //    else if (type.Equals("Value", StringComparison.OrdinalIgnoreCase))
        //    {
        //        // short version (no suffix)
        //        if (number >= 1_000_000M)
        //            return (number / 1_000_000M).ToString("0.00", CultureInfo.CurrentCulture);
        //        else if (number >= 1_000M)
        //            return (number / 1_000M).ToString("0.00", CultureInfo.CurrentCulture);
        //        else
        //            return number.ToString("0.00", CultureInfo.CurrentCulture);
        //    }

        //    return defaultString;
        //}
        public static string ConvertToShortUnitV2(object obj, string type = "Value", string defaultString = "0")
        {
            if (obj == null || obj == DBNull.Value) return defaultString;

            string str = NullToString(obj).Trim();
            if (!decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal number))
            {
                decimal.TryParse(str, NumberStyles.Any, CultureInfo.CurrentCulture, out number);
            }

            if (number == 0 && str != "0") return defaultString;

            if (type.Equals("Unit", StringComparison.OrdinalIgnoreCase))
            {
                // full number with thousand separator, no decimals
                return Math.Truncate(number).ToString("N0", CultureInfo.CurrentCulture);
            }
            else if (type.Equals("Value", StringComparison.OrdinalIgnoreCase))
            {
                // short version with comma separators
                if (number >= 1_000_000M)
                    return (number / 1_000_000M).ToString("N2", CultureInfo.CurrentCulture); // ✅ with comma
                else if (number >= 1_000M)
                    return (number / 1_000M).ToString("N2", CultureInfo.CurrentCulture); // ✅ with comma
                else
                    return number.ToString("N2", CultureInfo.CurrentCulture); // ✅ also with comma for small numbers
            }

            return defaultString;
        }

        public static string ConvertToShortUnithaveZero(object obj, string defaultString = "0.00")
        {
            string str = NullToAnyString(obj, defaultString);

            if (decimal.TryParse(str, out decimal number))
            {
                if (number >= 1_000_000)
                    return (number / 1_000_000M).ToString("0.00"); // ล้าน
                else if (number >= 1_000)
                    return (number / 1_000M).ToString("0.00");     // พัน
                else
                    return number.ToString("0.00");               // ค่าปกติ
            }

            return defaultString;
        }


        public static string ConvertToMoney(object obj, string defaultString = "0.00")
        {
            if (obj == null || obj == DBNull.Value)
                return defaultString;

            if (decimal.TryParse(obj.ToString(), out decimal number))
                return number.ToString("#,##0.00"); // comma separator, 2 decimals

            return defaultString;
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

        public static List<int> ParseCsvToIntList(string? csv)
        {
            var result = new List<int>();
            if (string.IsNullOrWhiteSpace(csv)) return result;

            foreach (var token in csv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                if (int.TryParse(token, out var val))
                    result.Add(val);
            }
            return result;
        }

        public static int SafeToInt(object? val)
        {
            if (val == null) return 0;
            if (val is int i) return i;
            if (int.TryParse(val.ToString(), out var parsed)) return parsed;
            return 0;
        }

    }
}
