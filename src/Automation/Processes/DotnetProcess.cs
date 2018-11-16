using McMaster.Extensions.CommandLineUtils;
using System.IO;

namespace Initializer
{
    public class DotnetProcess : ProcessBase
    {
        public DotnetProcess(string arguments, DirectoryInfo workingDirectory, IConsole console)
            : base("dotnet", arguments, workingDirectory, console)
        {
        }
    }
}
