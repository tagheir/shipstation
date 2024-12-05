using System;

namespace Generics.Services.DatabaseService.AdoNet
{
    public static class StringUtils
    {
        public static object To(this string value, Type t)
        {
            return Convert.ChangeType(value, t);
        }
        public static object ToBoolean(this string value)
        {
            return value == "1" || value?.ToLower() == "true";
        }
    }
}
