using System;

namespace Search.Infrastructure.Extension
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parses the string to enum.
        /// </summary>
        /// <typeparam name="T">The enum type to parse</typeparam>
        /// <param name="value">The string value.</param>
        /// <returns>Enum</returns>
        /// Created By: linoy.mk 
        /// Created On: 05-03-2015 13:26
        public static T ParseEnum<T>(this string value) //This will throw exception if not parsed
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
