using CommandLine.Text;

namespace PoorSkills.Commander.StringReplacement
{
    [Verb("replace", HelpText = "Replace Names and Content")]
    public class StringReplacementOptions
    {
        [Option('p', "path", HelpText = "Defines the root path to change names and content", Required = true)]
        public string Path { get; set; } = string.Empty;


        [Option('o', "old", HelpText = "Defines the old text to be replaced", Required = true)]
        public string OldValue { get; set; } = string.Empty;

        [Option('n', "new", HelpText = "Defines the new text to take place on oldvalue", Required = true)]
        public string NewValue { get; set; } = string.Empty;

        [Option('g', "replaceguid", HelpText = "Defines if the Guids presents on .SLN files should be replaced. Default (true)", Required = false)]
        public bool ReplaceGuids { get; set; } = true;

        [Usage]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new("replace", new StringReplacementOptions() { Path = @"C:\Users\Neo\source\repos", OldValue = "OldNamespace", NewValue = "NewNamespace", ReplaceGuids = true });
                yield return new("replace", new StringReplacementOptions() { Path = @"C:\Users\Trinity\source\repos", OldValue = "Matrix", NewValue = "MatrixV2"});
            }
        }
            
    }
}
