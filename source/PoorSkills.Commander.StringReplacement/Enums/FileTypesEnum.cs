using System.ComponentModel;

namespace PoorSkills.Commander.StringReplacement.Enums
{
    public enum FileTypesEnum
    {
        [Description(".cs")]
        cs,
        [Description(".csproj")]
        CsProj,
        [Description(".xml")]
        xml,
        [Description(".sln")]
        sln
    }
}
