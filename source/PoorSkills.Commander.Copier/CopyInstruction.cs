namespace PoorSkills.Commander.Copier
{
    public readonly record struct CopyInstruction
    {
        public CopyInstruction(string oldPath, string newPath, CopyInstructionTypeEnum copyType)
        {
            OldPath = oldPath;
            NewPath = newPath;
            CopyType = copyType;
        }
        public string OldPath { get; }
        public string NewPath { get; }
        public CopyInstructionTypeEnum CopyType { get;}
    }
}
