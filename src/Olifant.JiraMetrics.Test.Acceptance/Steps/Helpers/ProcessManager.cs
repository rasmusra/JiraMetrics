using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Helpers
{
    internal static class ProcessManager
    {
        private static readonly object LockObject = new object();
        private static Process process;

        internal static void Start(string cmd, string workingDir = ".")
        {
            lock (LockObject)
            {
                if (process != null)
                {
                    return;
                }

                var startInfo = new ProcessStartInfo
                                    {
                                        FileName = "cmd",
                                        Arguments = string.Format("/c \"{0}\"", cmd),
                                        WorkingDirectory = workingDir,
                                        RedirectStandardOutput = true,
                                        RedirectStandardError = true,
                                        UseShellExecute = false,
                                        CreateNoWindow = true
                                    };

                process = new Process() { StartInfo = startInfo };
                process.Start();

                if (process.HasExited)
                {
                    var stdErr = process.StandardError.ReadToEnd();

                    if (stdErr.Length > 0)
                    {
                        throw new Exception(
                            string.Format(
                                "Failed executing cmd: '{0}', stdErr='{1}', workDir='{2}'",
                                cmd,
                                stdErr,
                                workingDir));
                    }

                    if (process.ExitCode > 0)
                    {
                        throw new Exception(
                            string.Format(
                                "Failed executing cmd: '{0}', stdErr='{1}', workDir='{2}'",
                                cmd,
                                stdErr,
                                workingDir));
                    }
                }
            }
        }

        internal static void KillByName(params string[] processNames)
        {
            foreach (var name in processNames)
            {
                Process.GetProcessesByName(name)
                    .ToList()
                    .ForEach(
                        p =>
                        {
                            p.Kill();
                            p.WaitForExit(5000);
                        });
            }
        }
    }
}
