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
                return default(T);

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
    }
}
