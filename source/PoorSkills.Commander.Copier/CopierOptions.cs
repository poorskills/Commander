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

        [Option('i', "included", HelpText = "Included extensions separated by space (if none is informed all will be considered)", Required = false)]
        public string Included { get; set; } = string.Empty;

        [Option('e', "excluded", HelpText = "Excluded extensions separated by space (if none is informed none will be considered)", Required = false)]
        public string Excluded { get; set; } = string.Empty;

        [Usage]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new("copy", new CopierOptions() { Source = @"C:\Users\Neo\source\repos\MyRepo", Destination = @"C:\Users\Neo\source\repos\NewRepo", Excluded = "dll exe pdb" });
                yield return new("copy", new CopierOptions() { Source = @"C:\Users\Neo\source\repos\MyRepo", Destination = @"C:\Users\Neo\source\repos\NewRepo", Excluded = ".dll .exe .pdb" });
                yield return new("copy", new CopierOptions() { Source = @"C:\Users\Neo\source\repos\MyRepo", Destination = @"C:\Users\Neo\source\repos\NewRepo", Included =  "cs" });
                yield return new("copy", new CopierOptions() { Source = @"C:\Users\Neo\source\repos\MyRepo", Destination = @"C:\Users\Neo\source\repos\NewRepo" });
            }
        }

    }
}
