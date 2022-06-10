using System.Text.RegularExpressions;

namespace PoorSkills.Commander.StringReplacement
{
    public static class SolutionGuidReplacer
    {
        private const string guidPattern = @"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?";
        public static string ChangeSlnGuids(string text)
        {
            var innerText = text[
                text
                .IndexOf("globalsection(projectconfigurationplatforms)", StringComparison.OrdinalIgnoreCase)..];

            Dictionary<string, string> guidsReplacement = new(StringComparer.OrdinalIgnoreCase);
            var matches = Regex.Matches(innerText, guidPattern);
            foreach (var match in matches)
            {
                guidsReplacement[match.ToString() ?? string.Empty] = Guid.NewGuid().ToString();
            }

            foreach (var guidReplacement in guidsReplacement)
            {
                var oldGuid = guidReplacement.Key.Replace("(", "").Replace(")", "").Replace("{", "").Replace("}", "");
                var newGuid = guidReplacement.Key.Replace(oldGuid, guidReplacement.Value).ToUpper();
                text = text.Replace(guidReplacement.Key, newGuid);
            }
            return text;
        }
    }
}
