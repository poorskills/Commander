namespace PoorSkills.Commander.Copier
{
    public static class FilesAndFoldersCopier
    {
        public static int Copy(CopierOptions options)
        {
            try
            {
                var instructions = new InstructionGetter(options.Source,
                            options.Destination,GetExtensions(options.Excluded), GetExtensions(options.Included))
                            .GetInstructions(options.Source, options.Destination, new());

                if (instructions?.Count == 0)
                    return 0;

                foreach (var copyInstruction in
                    instructions
                    ?.Where(o => o.CopyType == CopyInstructionTypeEnum.Folder)
                    ?.ToList() ?? new())
                {
                    Directory.CreateDirectory(copyInstruction.NewPath);
                    WriteLine($"{copyInstruction.NewPath} Created");
                }


                foreach (var copyInstruction in
                    instructions
                    ?.Where(o => o.CopyType == CopyInstructionTypeEnum.File)
                    ?.Reverse()
                    ?.ToList() ?? new())
                {
                    if (File.Exists(copyInstruction.NewPath))
                        File.Delete(copyInstruction.NewPath);
                    File.Copy(copyInstruction.OldPath, copyInstruction.NewPath);
                    WriteLine($"{copyInstruction.OldPath} copied to {copyInstruction.NewPath}");
                }
                return 0;
            }
            catch (Exception)
            {
                return 1;
                throw;
            }

        }

        private static List<string> GetExtensions(string extensionsString)
        {
            var splitedExtensions = extensionsString?.Split(' ', StringSplitOptions.RemoveEmptyEntries)?.ToList() ?? new();
            List<string> result = new();
            foreach (var item in splitedExtensions)
            {
                result.Add("." + item.Replace(".", string.Empty));
            }
            return result;
        }
    }
}
