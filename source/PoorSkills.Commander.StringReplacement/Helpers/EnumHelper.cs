using PoorSkills.Commander.StringReplacement.Extensions;

namespace PoorSkills.Commander.StringReplacement.Helpers
{
    public static class EnumHelper
    {
        public static List<string> GetEnumDescriptions<T>()
            where T : struct, Enum
        {
            List<string> result = new();
            foreach (Enum enumValue in Enum.GetValues(typeof(T)))
            {
                result.Add(enumValue.GetDescription());
            }
            return result;
        }
    }
}