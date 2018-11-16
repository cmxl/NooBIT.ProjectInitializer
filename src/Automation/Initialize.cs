using Initializer.Services;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Initializer
{
    [Command(Name = "dotnet init",
        FullName = "dotnet-init",
        Description = "Creates a dotnet project with common folder structures and initializes it with git flow",
        ExtendedHelpText = "Creates a dotnet project with common folder structures and initializes it with git flow")]
    [HelpOption]
    public class Initialize
    {
        [Argument(0, Name = "path", Description = "The path of the working directory")]
        [DirectoryExists]
        public string Path { get; }

        [Required(ErrorMessage = "You need to provide a valid dotnet project type (e.g. classlib, console, etc.)")]
        [Option("-t|--type", CommandOptionType.SingleValue, Description = "The type of project")]
        public string ProjectType { get; }

        [Required(ErrorMessage = "You need to provide a name fpr the project")]
        [Option("-n|--name", CommandOptionType.SingleValue, Description = "The type of project")]
        public string ProjectName { get; }

        public async Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            var processService = new ProcessService();
            var webService = new WebService();

            console.WriteLine("Creating directories");
            var directoryService = new DirectoryService(Path, ProjectName);
            directoryService.CreateAll();

            console.WriteLine("Create README.md");
            File.Create(System.IO.Path.Combine(directoryService.ProjectDirectory.FullName, "README.md"));

            console.WriteLine("Add .gitignore from GitHub's gitignore repository");
            await webService.CreateGitIgnore(directoryService.ProjectDirectory);

            console.WriteLine("Create solution");
            var exitcode = await processService.CreateSolution(ProjectName, directoryService.ProjectDirectory, console);

            console.WriteLine("Create project");
            exitcode += await processService.CreateProject(ProjectName, ProjectType, directoryService.Src, console);

            console.WriteLine("Create test project");
            exitcode += await processService.CreateTestProject(ProjectName, directoryService.Test, console);

            console.WriteLine("Add projects to solution");
            exitcode += await processService.AddProjectsToSolution(ProjectName, directoryService.ProjectDirectory, console);

            console.WriteLine("Initialize git flow");
            exitcode += await processService.InitGitFlow(directoryService.ProjectDirectory, console);

            return exitcode;
        }



    }
}
