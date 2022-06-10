using PoorSkills.Commander.StringReplacement.Enums;
using PoorSkills.Commander.StringReplacement.Extensions;
using PoorSkills.Commander.StringReplacement.Helpers;

namespace PoorSkills.Commander.StringReplacement
{
    public static class InstructionsCollector
    {

        public static List<ReplacementInstruction> GetInstructions(string path,
            string oldText,
            string newText,
            List<ReplacementInstruction> replacementInstructions)
        {
            if (replacementInstructions == default)
                replacementInstructions = new();
            List<string> possibleExtensions = EnumHelper.GetEnumDescriptions<FileTypesEnum>() ?? new();
            try
            {
                foreach (string dir in Directory.GetDirectories(path)?.ToList() ?? new())
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        if (File.Exists(file))
                        {
                            FileInfo fileInfo = new(file);
                            if (!string.IsNullOrEmpty(fileInfo.Extension)
                                && possibleExtensions.Any(x => x.Equals(fileInfo.Extension, StringComparison.OrdinalIgnoreCase)))
                            {
                                string? text = File.ReadAllText(file);
                                if (text.Contains(oldText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                                {
                                    if (fileInfo.Extension.Equals(FileTypesEnum.sln.GetDescription(),
                                            StringComparison.OrdinalIgnoreCase))
                                    {
                                        text = SolutionGuidReplacer.ChangeSlnGuids(text);
                                    }

                                    text = text.Replace(oldText ?? string.Empty, newText, StringComparison.OrdinalIgnoreCase);
                                    File.WriteAllText(file, text);
                                }
                            }
                            if (fileInfo.Name.Contains(oldText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                            {
                                string newPath = Path.Combine(fileInfo.DirectoryName ?? string.Empty, fileInfo.Name.Replace(oldText, newText, StringComparison.OrdinalIgnoreCase));
                                replacementInstructions?.Add(new(ReplacementType.File, file, newPath));
                            }
                        }
                        Console.WriteLine(file);
                    }
                    DirectoryInfo directoryInfo = new(dir);
                    if (directoryInfo.Name.Contains(oldText ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                    {
                        string newPath = Path.Combine(directoryInfo.Parent?.FullName ?? string.Empty,
                            directoryInfo.Name.Replace(oldText ?? string.Empty, newText, StringComparison.OrdinalIgnoreCase));
                        replacementInstructions?.Add(new(ReplacementType.Directory, dir, newPath));
                    }
                    GetInstructions(dir, oldText ?? string.Empty, newText, replacementInstructions ?? new());
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return replacementInstructions ?? new();
        }
    }

}
