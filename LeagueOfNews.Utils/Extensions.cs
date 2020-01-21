using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LeagueOfNews.Utils
{
    public static class Extensions
    {
        public static Dictionary<string, string> ToDictionary(this object objectToConvert)
        {
            return objectToConvert.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(objectToConvert, null).ToString());
        }

        public static T FromDictionary<T>(this IDictionary<string, string> dictionaryToConvert) where T : new()
        {
            T target = new T();
            foreach (KeyValuePair<string, string> item in dictionaryToConvert)
            {
                Type type = target.GetType();
                PropertyInfo prop = type.GetProperty(item.Key);
                prop.SetValue(target, item.Value, null);
            }
            return target;
        }
    }
}
