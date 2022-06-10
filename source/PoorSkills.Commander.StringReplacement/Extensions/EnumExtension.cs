using System.ComponentModel;

namespace PoorSkills.Commander.StringReplacement.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            var descriptionAttribute = enumValue?.GetType()
                ?.GetField(enumValue.ToString())
                ?.GetCustomAttributes(false)
                ?.SingleOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttribute?.Description ?? enumValue?.ToString() ?? string.Empty;
        }
    }
}
