using NooBIT.ProjectInitializer.Processes;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace NooBIT.ProjectInitializer
{
    public class ProcessBase : IRunnable
    {
        private readonly ProcessStartInfo _startInfo;
        private readonly IConsole _console;

        public ProcessBase(string fileName, string arguments, DirectoryInfo workingDirectory, IConsole console)
        {
            _console = console;
            _startInfo = new ProcessStartInfo(fileName)
            {
                WorkingDirectory = workingDirectory.FullName,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = arguments
            };
        }

        public int Run() => throw new NotImplementedException();

        public async Task<int> RunAsync()
        {
            using (var process = new Process { StartInfo = _startInfo })
                return await process.RunAsync(_console);
        }
    }
}
