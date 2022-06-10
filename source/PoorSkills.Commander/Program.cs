using CommandLine.Text;
using PoorSkills.Commander.Registror;
using PoorSkills.Commander.StringReplacement;

public static class Program
{
    public static async Task Main(string[] args)
    {

        Action<ParserSettings> settings = o => o.CaseSensitive = false;
        settings += o => o.CaseInsensitiveEnumValues = false;
        Parser parser = new(settings);

        var parseResult = parser.ParseArguments<StringReplacementOptions>(args);
        CommanderRegistror.Register();
        await parseResult.MapResult(async options =>
            {
                switch (options)
                {
                    case StringReplacementOptions:
                        {
                            return await Task.FromResult(StringReplacer.Replace(options as StringReplacementOptions ?? new()));
                        }
                    default:
                        return await Task.FromResult(-1);
                }

            }, async errors =>
            {
                var helptext = HelpText.AutoBuild(parseResult, 150);
                helptext.AddEnumValuesToHelpText = true;
                if (!errors.Any(o => o.Tag == ErrorType.NoVerbSelectedError))
                    helptext.AddOptions(parseResult);

                var qtdeErrs = errors?.Count() ?? default;
                if (qtdeErrs > 0)
                {
                    Console.WriteLine(helptext);
                    string[] ar = { Console.ReadLine() ?? string.Empty };
                    await Main(ar);
                }
                return qtdeErrs;
            });
        await Task.CompletedTask;
    }
}