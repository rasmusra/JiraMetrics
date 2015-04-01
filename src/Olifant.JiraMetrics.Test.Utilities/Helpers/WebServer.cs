using System;
using System.IO;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public class WebServer
    {
        public const string Port = "8193";
        private static readonly FileInfo FakeStructureMapConfig = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Test.Utilities\Fakes\FakeStructureMap.xml");
        private static readonly FileInfo StructureMapConfig = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Web\StructureMap.xml");
        private static readonly FileInfo BackupOfStructureMapConfig = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Web\StructureMap.xml.backup");


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
            var fakeConfig = File.ReadAllText(FakeStructureMapConfig.FullName);

            // only copy config-file to backup-file if config-file differs from fake config
            if (realConfig != fakeConfig)
            {
                BackupOfStructureMapConfig.Delete();
                StructureMapConfig.CopyTo(BackupOfStructureMapConfig.FullName);
                FakeStructureMapConfig.CopyTo(StructureMapConfig.FullName, true);
            }
        }

        public static void RemoveFakes()
        {
            // if no backup exists we cannot restore anything, exit silently
            if (!BackupOfStructureMapConfig.Exists)
            {
                return;
            }

            BackupOfStructureMapConfig.CopyTo(StructureMapConfig.FullName, true);
        }
    }

}
