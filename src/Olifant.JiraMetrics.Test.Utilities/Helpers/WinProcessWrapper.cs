using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public static class WinProcessWrapper
    {
        public static void Start(string cmd, string args, string workingDir = ".")
        {
            var startInfo = new ProcessStartInfo
                                {
                                    FileName = cmd,
                                    Arguments = args,
                                    WorkingDirectory = workingDir,
                                    RedirectStandardOutput = true,
                                    RedirectStandardError = true,
                                    UseShellExecute = false
                                };

            var process = new Process() { StartInfo = startInfo };
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

        public static void KillByName(string name)
        {
            foreach (var namedProcess in Process.GetProcessesByName(name))
            {
                namedProcess.Kill();
                if (!namedProcess.WaitForExit(5000))
                {
                    Console.WriteLine(string.Format("Failure killing process '{0}', exitcode:{1}",
                        namedProcess.ProcessName, namedProcess.ExitCode));
                }
            }
        }
    }
}
