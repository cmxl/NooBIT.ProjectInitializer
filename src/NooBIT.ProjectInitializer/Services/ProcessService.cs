using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Threading.Tasks;

namespace NooBIT.ProjectInitializer.Services
{
    public class ProcessService
    {
        public async Task<int> InitGitFlow(DirectoryInfo workingDirectory, IConsole console)
            => await new GitProcess("flow init -d", workingDirectory, console).RunAsync();

        public async Task<int> CreateSolution(string projectName, DirectoryInfo workingDirectory, IConsole console)
            => await new DotnetProcess($"new sln -n {projectName}", workingDirectory, console).RunAsync();

        public async Task<int> CreateProject(string projectName, string projectType, DirectoryInfo workingDirectory, IConsole console)
            => await new DotnetProcess($"new {projectType} -n {projectName}", workingDirectory, console).RunAsync();

        public async Task<int> CreateTestProject(string projectName, DirectoryInfo workingDirectory, IConsole console)
            => await new DotnetProcess($"new xunit -n {projectName}.Tests", workingDirectory, console).RunAsync();

        public async Task<int> AddProjectsToSolution(string projectName, DirectoryInfo workingDirectory, IConsole console)
        {
            var exitcode = await new DotnetProcess($"sln add src\\{projectName}\\{projectName}.csproj", workingDirectory, console).RunAsync();
            return exitcode != 0
                ? exitcode
                : await new DotnetProcess($"sln add test\\{projectName}.Tests\\{projectName}.Tests.csproj", workingDirectory, console).RunAsync();
        }
    }
}
