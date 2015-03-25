using System;
using System.IO;
using Olifant.JiraMetrics.Test.Utilities.Helpers;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Helpers
{
    internal class WebServer
    {
        public const string Port = "8193";
        private static readonly FileInfo FakeStructureMapConfigFileInfo = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Test.Utilities\FakeStructureMap.xml");
        private static readonly FileInfo StructureMapConfig = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Web\StructureMap.xml");
        private static readonly FileInfo StructureMapConfigBackup = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Web\StructureMap.xml.backup");


        private static string ProgramFilesPath
        {
            get
            {
                return Environment.Is64BitOperatingSystem
                           ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                           : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }
        }

        private static string IisExe
        {
            get
            {
                return Path.Combine(ProgramFilesPath, "IIS Express", "iisexpress.exe");
            }
        }

        public static void StartIis()
        {
            var cmd = string.Format(
                "\"{0}\" /path:\"{1}\" /port:{2}",
                IisExe,
                new DirectoryInfo(@"..\..\..\Olifant.JiraMetrics.Web").FullName,
                Port);

            ProcessManager.Start(cmd);
        }

        public static void SetupFakes()
        {
            var realConfig = File.ReadAllText(StructureMapConfig.FullName);
            var fakeConfig = File.ReadAllText(FakeStructureMapConfigFileInfo.FullName);

            // only copy config-file to backup-file if config-file differs from fake config
            if (realConfig != fakeConfig)
            {
                StructureMapConfigBackup.Delete();
                StructureMapConfig.CopyTo(StructureMapConfigBackup.FullName);
                FakeStructureMapConfigFileInfo.CopyTo(StructureMapConfig.FullName, true);
            }
        }

        public static void RemoveFakes()
        {
            // if no backup exists we cannot restore anything, exit silently
            if (!StructureMapConfigBackup.Exists)
            {
                return;
            }

            StructureMapConfigBackup.CopyTo(StructureMapConfig.FullName, true);
        }
    }

}
