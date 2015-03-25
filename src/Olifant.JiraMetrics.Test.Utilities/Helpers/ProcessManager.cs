using System;
using System.Diagnostics;
using System.Linq;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public static class ProcessManager
    {
        private static readonly object LockObject = new object();
        private static Process _process;

        public static void Start(string cmd, string workingDir = ".")
        {
            lock (LockObject)
            {
                if (_process != null)
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

                _process = new Process() { StartInfo = startInfo };
                _process.Start();

                if (_process.HasExited)
                {
                    var stdErr = _process.StandardError.ReadToEnd();

                    if (stdErr.Length > 0)
                    {
                        throw new Exception(
                            string.Format(
                                "Failed executing cmd: '{0}', stdErr='{1}', workDir='{2}'",
                                cmd,
                                stdErr,
                                workingDir));
                    }

                    if (_process.ExitCode > 0)
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
