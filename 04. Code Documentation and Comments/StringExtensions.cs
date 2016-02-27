namespace Telerik.ILS.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A static class that extends the functionality of the String class with multiple methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a String to its MD5Hash representation.
        /// </summary>
        /// <param name="input">A string to be converted.</param>
        /// <returns>A string of the MD5 code equivelent to the input.</returns>
        public static string ToMd5Hash(this string input)
        {
            var md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return builder.ToString();
        }
        /// <summary>
        /// Converts a string to a positive or negative boolean value.
        /// Positive words yield a "true" bool.
        /// </summary>
        /// <param name="input">String containing positive or negative words such as "true", "ok", "yes", "1", "да"</param>
        /// <returns>Returns a positive bool if the input contains positive words. In all other cases returns negative bool</returns>
        public static bool ToBoolean(this string input)
        {
            ///Initializes an array of expected positive strings
            var stringTrueValues = new[] { "true", "ok", "yes", "1", "да" };
            ///Checks if any of the positive strings is contained in the input - if yes it returns true. Otherwise it returns false.
            return stringTrueValues.Contains(input.ToLower());
        }
        /// <summary>
        /// Converts the string to a short(System.Int16) if possible.
        /// </summary>
        /// <param name="input">String consisting of integer number in the range of -32,768 to 32,767 to be converted to short(Signed 16-bit integer)</param>
        /// <returns>The short(System.Int16) representation of the string or 0 if input is invalid.</returns>
        public static short ToShort(this string input)
        {
            short shortValue;
            short.TryParse(input, out shortValue);
            return shortValue;
        }
        /// <summary>
        /// Converts a string to int(Systen.Int32).
        /// </summary>
        /// <param name="input">String consisting of integer number in the range of -2,147,483,648 to 2,147,483,647 to be converted to int(System.Int32)</param>
        /// <returns>The int(System.Int32) representation of the string or 0 if input is invalid.</returns>
        public static int ToInteger(this string input)
        {
            int integerValue;
            int.TryParse(input, out integerValue);
            return integerValue;
        }
        /// <summary>
        /// Converts a string to long(System.Int64)
        /// </summary>
        /// <param name="input">String consisting of integer number in the range of –9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 to be converted to long(System.Int64)</param>
        /// <returns>The long(System.Int32) representation of the string or 0 if input is invalid.</returns>
        public static long ToLong(this string input)
        {
            long longValue;
            long.TryParse(input, out longValue);
            return longValue;
        }
        /// <summary>
        /// Converts a string to DateTime object.
        /// </summary>
        /// <param name="input">String consisting of valid date and/or time data</param>
        /// <returns>The DateTime representation of the input or the default value for DateTime object.</returns>
        public static DateTime ToDateTime(this string input)
        {
            DateTime dateTimeValue;
            DateTime.TryParse(input, out dateTimeValue);
            return dateTimeValue;
        }
        /// <summary>
        /// Makes the first letter of the input string capital.
        /// </summary>
        /// <param name="input">Input string which first letter is going to be capitalized.</param>
        /// <returns>The string with capitalized first letter.</returns>
        public static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + input.Substring(1, input.Length - 1);
        }
        /// <summary>
        /// Extracts the string between two substrings.
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="startString">The first string to search from</param>
        /// <param name="endString">The end string</param>
        /// <param name="startFrom">Where to start the search from. Default is index 0.</param>
        /// <returns></returns>
        public static string GetStringBetween(this string input, string startString, string endString, int startFrom = 0)
        {
            //Overrites the input string to search in less text.
            input = input.Substring(startFrom);
            //Resets startForm to 0 in case other value was set in the call.
            startFrom = 0;
            if (!input.Contains(startString) || !input.Contains(endString))
            {
                return string.Empty;
            }
            
            var startPosition = input.IndexOf(startString, startFrom, StringComparison.Ordinal) + startString.Length;
            if (startPosition == -1)
            {
                return string.Empty;
            }

            var endPosition = input.IndexOf(endString, startPosition, StringComparison.Ordinal);
            if (endPosition == -1)
            {
                return string.Empty;
            }

            return input.Substring(startPosition, endPosition - startPosition);
        }
        /// <summary>
        /// Coverts Cyrillic input string to Latin output string.
        /// </summary>
        /// <param name="input">String input in Cyrillic alphabet.</param>
        /// <returns>The lating representation of the input.</returns>
        public static string ConvertCyrillicToLatinLetters(this string input)
        {
            var bulgarianLetters = new[]
                                       {
                                           "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п",
                                           "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ь", "ю", "я"
                                       };
            var latinRepresentationsOfBulgarianLetters = new[]
                                                             {
                                                                 "a", "b", "v", "g", "d", "e", "j", "z", "i", "y", "k",
                                                                 "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h",
                                                                 "c", "ch", "sh", "sht", "u", "i", "yu", "ya"
                                                             };
            for (var i = 0; i < bulgarianLetters.Length; i++)
            {
                input = input.Replace(bulgarianLetters[i], latinRepresentationsOfBulgarianLetters[i]);
                input = input.Replace(bulgarianLetters[i].ToUpper(), latinRepresentationsOfBulgarianLetters[i].CapitalizeFirstLetter());
            }

            return input;
        }
        /// <summary>
        /// Converts Latin string to their Cyrillic representation.
        /// </summary>
        /// <param name="input">String in Latin letters.</param>
        /// <returns>The Cyrillic representation of the letter.</returns>
        public static string ConvertLatinToCyrillicKeyboard(this string input)
        {
            var latinLetters = new[]
                                   {
                                       "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                                       "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
                                   };

            var bulgarianRepresentationOfLatinKeyboard = new[]
                                                             {
                                                                 "а", "б", "ц", "д", "е", "ф", "г", "х", "и", "й", "к",
                                                                 "л", "м", "н", "о", "п", "я", "р", "с", "т", "у", "ж",
                                                                 "в", "ь", "ъ", "з"
                                                             };

            for (int i = 0; i < latinLetters.Length; i++)
            {
                input = input.Replace(latinLetters[i], bulgarianRepresentationOfLatinKeyboard[i]);
                input = input.Replace(latinLetters[i].ToUpper(), bulgarianRepresentationOfLatinKeyboard[i].ToUpper());
            }

            return input;
        }
        /// <summary>
        /// Converst a wrong user name to valid user name by removing invalid symbols and converting the string to latin.
        /// </summary>
        /// <param name="input">Any input string.</param>
        /// <returns>Valid string for User name.</returns>
        public static string ToValidUsername(this string input)
        {
            input = input.ConvertCyrillicToLatinLetters();
            return Regex.Replace(input, @"[^a-zA-z0-9_\.]+", string.Empty);
        }
        /// <summary>
        /// Replaces invalid file name to valid file name by converting to latin and replacing blank spaces with a dash.
        /// </summary>
        /// <param name="input">Invalid file name string.</param>
        /// <returns>Valid file name string.</returns>
        public static string ToValidLatinFileName(this string input)
        {
            input = input.Replace(" ", "-").ConvertCyrillicToLatinLetters();
            return Regex.Replace(input, @"[^a-zA-z0-9_\.\-]+", string.Empty);
        }
        /// <summary>
        /// Gets the first x characters in a string.
        /// </summary>
        /// <param name="input">String to get the first characters from.</param>
        /// <param name="charsCount">The number of characters to extract.</param>
        /// <returns>String of the first x characters.</returns>
        public static string GetFirstCharacters(this string input, int charsCount)
        {
            return input.Substring(0, Math.Min(input.Length, charsCount));
        }
        /// <summary>
        /// Gets the file extention in a file name.
        /// </summary>
        /// <param name="fileName">The full file name with the extention at the end.</param>
        /// <returns>The extention of the file.</returns>
        public static string GetFileExtension(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }

            string[] fileParts = fileName.Split(new[] { "." }, StringSplitOptions.None);
            if (fileParts.Count() == 1 || string.IsNullOrEmpty(fileParts.Last()))
            {
                return string.Empty;
            }

            return fileParts.Last().Trim().ToLower();
        }
        /// <summary>
        /// Converts the file extenstion to the full contet type(MIME type).
        /// </summary>
        /// <param name="fileExtension">The short file extention string.</param>
        /// <returns>The full conten type(MIME type) string.</returns>
        public static string ToContentType(this string fileExtension)
        {
            var fileExtensionToContentType = new Dictionary<string, string>
                                                 {
                                                     { "jpg", "image/jpeg" },
                                                     { "jpeg", "image/jpeg" },
                                                     { "png", "image/x-png" },
                                                     {
                                                         "docx",
                                                         "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                                     },
                                                     { "doc", "application/msword" },
                                                     { "pdf", "application/pdf" },
                                                     { "txt", "text/plain" },
                                                     { "rtf", "application/rtf" }
                                                 };
            if (fileExtensionToContentType.ContainsKey(fileExtension.Trim()))
            {
                return fileExtensionToContentType[fileExtension.Trim()];
            }

            return "application/octet-stream";
        }
        /// <summary>
        /// Converts a string to a byte array.
        /// </summary>
        /// <param name="input">A string to be converted to byte array.</param>
        /// <returns>A byte array representation of the input.</returns>
        public static byte[] ToByteArray(this string input)
        {
            var bytesArray = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytesArray, 0, bytesArray.Length);
            return bytesArray;
        }
    }
}
