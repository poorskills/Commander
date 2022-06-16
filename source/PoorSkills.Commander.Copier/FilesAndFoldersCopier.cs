namespace PoorSkills.Commander.Copier
{
    public static class FilesAndFoldersCopier
    {
        public static int Copy(CopierOptions options)
        {
            try
            {
                var ignored = options
                    .Ignored
                    ?.Split(' ', StringSplitOptions.RemoveEmptyEntries)?.ToList() ?? new();

                List<string> ignoredExtensions = new();
                foreach (var item in ignored)
                {
                    ignoredExtensions.Add("." + item);
                }
                var instructions = new InstructionGetter()
                            .GetInstructions(options.Source,
                            options.Destination, ignoredExtensions, new());

                if (instructions?.Count == 0)
                    return 0;

                foreach (var copyInstruction in
                    instructions
                    ?.Where(o => o.CopyType == CopyInstructionTypeEnum.Folder)
                    ?.ToList() ?? new())
                {
                    Directory.CreateDirectory(copyInstruction.NewPath);
                    Console.WriteLine($"{copyInstruction.NewPath} Created");
                }


                foreach (var copyInstruction in
                    instructions
                    ?.Where(o => o.CopyType == CopyInstructionTypeEnum.File)
                    ?.Reverse()
                    ?.ToList() ?? new())
                {

                    File.Copy(copyInstruction.OldPath, copyInstruction.NewPath);
                    Console.WriteLine($"{copyInstruction.OldPath} copied to {copyInstruction.NewPath}");
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
