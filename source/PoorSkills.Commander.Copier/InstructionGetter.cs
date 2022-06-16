namespace PoorSkills.Commander.Copier
{
    public class InstructionGetter
    {
        private string _source;
        private string _target;
        public List<CopyInstruction> GetInstructions(string source,
            string destination,
            List<string> ignoredInstruction,
            List<CopyInstruction> copyInstructions)
        {
            try
            {
                if (_source == default)
                    _source = source;
                if (_target == default)
                    _target = destination;
                
                foreach (string file in Directory.GetFiles(source))
                {
                    if (File.Exists(file))
                    {
                        FileInfo fileInfo = new(file);
                        if (ignoredInstruction.Any(o => o.Equals(fileInfo.Extension, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }
                        copyInstructions?.Add(new(file, file.Replace(_source, _target), CopyInstructionTypeEnum.File));
                    }
                }

                foreach (string dir in Directory.GetDirectories(source)?.ToList() ?? new())
                {
                    DirectoryInfo directoryInfo = new(dir);
                    copyInstructions?.Add(new(dir, dir.Replace(_source ?? string.Empty, _target), CopyInstructionTypeEnum.Folder));
                    GetInstructions(dir ?? string.Empty, destination,ignoredInstruction, copyInstructions ?? new());
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return copyInstructions ?? new();
        }
    }

}
