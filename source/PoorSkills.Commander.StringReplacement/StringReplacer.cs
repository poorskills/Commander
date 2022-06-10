using PoorSkills.Commander.StringReplacement.Enums;

namespace PoorSkills.Commander.StringReplacement
{
    public static class StringReplacer
    {
        public static int Replace(StringReplacementOptions options)
        {
            try
            {
                var replacementInstructions = InstructionsCollector
               .GetInstructions(options.Path, options.OldValue, options.NewValue, new());

                if (replacementInstructions?.Count == 0)
                    return 0;

                foreach (var replacementInstruction in
                    replacementInstructions
                    ?.Where(o => o.ReplacementType == ReplacementType.File)
                    ?.Reverse()
                    ?.ToList() ?? new())
                {

                    File.Move(replacementInstruction.OldPath, replacementInstruction.NewPath);
                }

                foreach (var replacementInstruction in
                    replacementInstructions
                    ?.Where(o => o.ReplacementType == ReplacementType.Directory)
                    ?.Reverse()
                    ?.ToList() ?? new())
                {
                    Directory.Move(replacementInstruction.OldPath, replacementInstruction.NewPath);
                }
                return 0;
            }
            catch (Exception)
            {
                return 1;
                throw;
            }
           
        }

    }
}
