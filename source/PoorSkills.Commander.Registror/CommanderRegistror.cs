using System.Reflection;

namespace PoorSkills.Commander.Registror
{
    public static class CommanderRegistror
    {
        public static int Register()
        {
            try
            {
                var currentPathEnviroment = Environment.GetEnvironmentVariable("PATH");
                var executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if(currentPathEnviroment?.IndexOf(executingPath ?? string.Empty, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    Environment.SetEnvironmentVariable("PATH", currentPathEnviroment + ";" + executingPath,
                                             EnvironmentVariableTarget.User);
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }

        }
    }
}
