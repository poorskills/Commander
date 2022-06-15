using FluentAssertions;
using PoorSkills.Commander.StringReplacement;
using System.Reflection;

namespace PoorSkills.Commander.Tests
{
    [TestClass]
    [TestCategory("Replacement Tests")]
    public class ReplacementStringTests
    {
        [TestMethod ("Test replacement in nested folders - Valid")]
        public void Replacement_in_nested_folders_valid()
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            StringReplacementOptions options = new() { Path = location, OldValue = "test", NewValue = "NEwValue" };
            StringReplacer.Replace(options);
            var dockerFileLocation = Path.Combine(location, "dockerfile");
            var csClassLocation = Path.Combine(location, "NewValueReplacement", "NewValueReplacementClass.cs");
            var csProjLocation = Path.Combine(location, "NewValueReplacement", "Subfolder", "CSprojnewvalue.csproj");
            File.ReadAllText(dockerFileLocation).Should().Be("#this is a NEwValue");
            File.ReadAllText(csClassLocation).Should().Be("// this is another NEwValue.");
            File.ReadAllText(csProjLocation).Should().Be("<xml> </xml> <!--this is the NEwValue-->");
        }
    }
}