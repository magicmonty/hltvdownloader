using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagansoft.Aria2.Extensions
{
    public static class StringExtensions
    {
        public static bool AsBoolean(this string value, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            bool result;
            if (bool.TryParse(value, out result))
                return result;

            return defaultValue;
        }

        public static long AsLong(this string value, long defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            long result;
            if (long.TryParse(value, out result))
                return result;

            return defaultValue;
        }

        public static int AsInt(this string value, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            int result;
            if (int.TryParse(value, out result))
                return result;

            return defaultValue;
        }

        public static double AsDouble(this string value, double defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            double result;
            if (double.TryParse(value, out result))
                return result;

            return defaultValue;
        }

        public static T AsEnum<T>(this string value, T defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
            }

            return defaultValue;
        }

        public static IEnumerable<string> AsEnumerable(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return Enumerable.Empty<string>();

            return value.Split(',');
        }
    }
}

