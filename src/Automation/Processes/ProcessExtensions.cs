using McMaster.Extensions.CommandLineUtils;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Initializer.Processes
{
    public static class ProcessExtensions
    {
        public static Task<int> RunAsync(this Process process, IConsole console = default)
        {
            var tcs = new TaskCompletionSource<int>();

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;

            process.Exited += (s, e) => tcs.SetResult(process.ExitCode);

            if (console != default)
                PrepareProcess(process, console);

            var started = process.Start();
            if (!started)
            {
                tcs.SetException(new InvalidOperationException($"Could not start process {process}"));
            }

            if (console != default)
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }

            return tcs.Task;
        }

        private static void PrepareProcess(Process process, IConsole console)
        {
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += (sender, args) => console.WriteLine(args.Data);
            process.ErrorDataReceived += (sender, args) => console.Error.WriteLine(args.Data);
        }
    }
}
