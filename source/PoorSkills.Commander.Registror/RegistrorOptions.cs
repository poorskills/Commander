using CommandLine.Text;

namespace PoorSkills.Commander.StringReplacement
{
    [Verb("register",  HelpText = "Register pskcmd globally (Requires Elevation) - Note: It is going to be registered on its current location")]
    public class RegistrorOptions
    {
        [Usage]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new("register", new RegistrorOptions());
            }
        }
            
    }
}
