using McMaster.Extensions.CommandLineUtils;
using System.IO;

namespace Initializer
{
    public class GitProcess : ProcessBase
    {
        public GitProcess(string arguments, DirectoryInfo workingDirectory, IConsole console)
            : base("git", arguments, workingDirectory, console)
        {
        }
    }
}
