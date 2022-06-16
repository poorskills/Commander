namespace PoorSkills.Commander.Copier
{
    public class InstructionGetter
    {
        public InstructionGetter(string? source, string? target, List<string>? ignoredExtensions, List<string>? includedExtensions)
        {
            _rootSource = source != default ? source : string.Empty;
            _finalDestination = target != default ? target : string.Empty;
            _ignoredExtensions = ignoredExtensions != default ? ignoredExtensions : new();
            _includedExtensions = includedExtensions != default ? includedExtensions : new();
        }


        private readonly string _rootSource;
        private readonly string _finalDestination;
        private readonly List<string> _ignoredExtensions;
        private readonly List<string> _includedExtensions;
        public List<CopyInstruction> GetInstructions(string source,
            string destination,
            List<CopyInstruction> copyInstructions)
        {
            try
            {
                foreach (string file in Directory.GetFiles(source))
                {
                    if (File.Exists(file))
                    {
                        FileInfo fileInfo = new(file);
                        if (_ignoredExtensions.Any(o => o.Equals(fileInfo.Extension,
                            StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }
                        if (_includedExtensions.Count > 0 &&
                            !_includedExtensions.Any(o => o.Equals(fileInfo.Extension, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }
                        copyInstructions?.Add(new(file, file.Replace(_rootSource, _finalDestination),
                            CopyInstructionTypeEnum.File));
                    }
                }

                foreach (string dir in Directory.GetDirectories(source)?.ToList() ?? new())
                {
                    DirectoryInfo directoryInfo = new(dir);

                    copyInstructions?.Add(new(dir, dir.Replace(_rootSource ?? string.Empty, _finalDestination),
                        CopyInstructionTypeEnum.Folder));

                    GetInstructions(dir ?? string.Empty, destination,
                        copyInstructions ?? new());
                }
            }
            catch (Exception excpt)
            {
                WriteLine(excpt.Message);
            }
            return copyInstructions ?? new();
        }
    }

}
