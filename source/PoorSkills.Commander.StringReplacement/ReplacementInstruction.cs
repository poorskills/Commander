using PoorSkills.Commander.StringReplacement.Enums;

namespace PoorSkills.Commander.StringReplacement
{
    public readonly record struct ReplacementInstruction
    {
        public ReplacementInstruction(ReplacementType replacementType, string oldPath, string newPath)
        {
            ReplacementType = replacementType;
            OldPath = oldPath;
            NewPath = newPath;
        }
        public ReplacementType ReplacementType { get; }
        public string OldPath { get; }
        public string NewPath { get; }
    }
}
