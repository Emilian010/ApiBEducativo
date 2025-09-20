using Api.Models.Utils;
using System.Reflection;

namespace Api.Models.Extensors
{

    public static class Extensors
    {
        public static string GetStringValue(this Enum value)
        {
            if (value != null)
            {
                Type type = value.GetType();

                FieldInfo fieldInfo = type.GetField(value.ToString());

                StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                    typeof(StringValueAttribute), false) as StringValueAttribute[];

                return attribs.Length > 0 ? attribs[0].StringValue : null;
            }
            else
            {
                return null;
            }
        }


        public static DateTime? ValidateDateTime(this DateTime? date)
        {
            return date == DateTime.MinValue
                ? null
                : date;
        }

    }
}
