using CommandLine.Text;

namespace PoorSkills.Commander.Copier
{
    [Verb("copy", HelpText = "Copy files or structure")]
    public class CopierOptions
    {
        [Option('s', "source", HelpText = "Source directory path", Required = true)]
        public string Source { get; set; } = string.Empty;

        [Option('d', "destination", HelpText = "Destination path", Required = true)]
        public string Destination { get; set; } = string.Empty;

        [Option('i', "ignore", HelpText = "Extensions to ignore separated by space", Required = false)]
        public string Ignored { get; set; } = string.Empty;

        [Usage]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new("copy", new CopierOptions() { Source = @"C:\Users\Neo\source\repos\MyRepo", Destination = @"C:\Users\Neo\source\repos\NewRepo", Ignored = "cs dll exe pdb" });
                yield return new("copy", new CopierOptions() { Source = @"C:\Users\Neo\source\repos\MyRepo", Destination = @"C:\Users\Neo\source\repos\NewRepo" });
            }
        }

    }
}
