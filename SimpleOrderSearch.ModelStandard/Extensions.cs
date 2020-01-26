using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SimpleOrderSearch.Model
{
    public static class Extensions
    {
        public static Nullable<T> ToNullableValue<T>(this string input) where T : struct
        {
            if (string.IsNullOrEmpty(input))
                return null;

            Nullable<T> result = new Nullable<T>();
            try
            {
                IConvertible convertibleString = (IConvertible)input;
                result = new Nullable<T>((T)convertibleString.ToType(typeof(T), CultureInfo.CurrentCulture));
            }
            catch (InvalidCastException)
            {

            }
            catch (FormatException)
            {

            }

            return result;
        }


        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
