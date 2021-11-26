using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensions
{
    public static class StringExtensions
    {
        public static string HashToMD5(this string inputStr)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(inputStr));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return false;
        }
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return false;
        }
        public static bool IsEmail(this string value)
        {
            var invalid = false;
            if (value.IsNullOrWhiteSpace())
                return false;

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(value,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsMobile(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return false;

            try
            {
                var number = long.Parse(value);
                if (value.Length != 11 || number < 9000000000 || number > 9999999999)
                {
                    return false;
                }
                else
                {
                    var result = (value[0] == '0' && value[1] == '9');// && (value[2] == '1' || value[2] == '2' || value[2] == '3');
                    return result;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }
        public static bool IsEqualto(this string value, string des)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (value == des)
                {
                    return true;
                }
            }
            return false;
        }
        public static string ToEnglishDigit(this string str)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
            {
                if (str.Contains(persian[j]))
                {
                    str = str.Replace(persian[j], j.ToString());
                }
            }

            return str;
        }
        public static string GetStandardFileName(this string url)
        {
            //The following reserved characters:

            //< (less than)
            //> (greater than)
            //: (colon)
            //" (double quote)
            /// (forward slash)
            //\ (backslash)
            //| (vertical bar or pipe)
            //? (question mark)
            //*(asterisk)

            url = url.TrimEnd();
            url = url.TrimStart();
            url = url.Replace("<", "-");
            url = url.Replace(">", "-");
            url = url.Replace(":", "-");
            url = url.Replace("\"", "-");
            url = url.Replace("/", "-");
            url = url.Replace("\\", "-");
            url = url.Replace("|", "-");
            url = url.Replace("?", "-");
            url = url.Replace("*", "-");

            url = url.Replace("    ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace(" ", "-");

            return url;
        }
        public static string GetStandardTitle(string url = "")
        {
            url = url.TrimEnd();
            url = url.TrimStart();
            url = url.Replace("    ", " ");
            url = url.Replace("    ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("   ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");
            url = url.Replace("  ", " ");

            return url.ToLower();
        }
        public static bool NotContains(this string value, string searchValue)
        {
            if (value.Contains(searchValue))
            {
                return false;
            }
            return false;
        }
        public static bool NotEquals(this string value, string searchValue)
        {
            if (value.Equals(searchValue))
            {
                return false;
            }
            return false;
        }

        public static DateTime ToGeorgianDateTime(this string str)
        {
            //PersianCalendar persianCalendar = new PersianCalendar();
            //return new DateTime(1391, 4, 7, persianCalendar);
            
            DateTime dt = DateTime.Parse(str, new CultureInfo("fa-IR"));
            // Get Utc Date
            return dt.ToUniversalTime();
        }
    }
}
