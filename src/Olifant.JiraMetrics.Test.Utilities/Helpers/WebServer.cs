using System;
using System.IO;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public class WebServer
    {
        public const string Port = "8193";
        private static readonly FileInfo StructureMapConfigFileInfo = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Web\StructureMap.xml");
        private static readonly FileInfo BackupOfStructureMapConfigFileInfo = new FileInfo(@"..\..\..\Olifant.JiraMetrics.Web\StructureMap.xml.backup");


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

        public static void SetupFakes(string fakeStructureMapPath)
        {
            var fakeStructureMapConfigFileInfo = new FileInfo(fakeStructureMapPath);
            var realConfig = File.ReadAllText(StructureMapConfigFileInfo.FullName);
            var fakeConfig = File.ReadAllText(fakeStructureMapConfigFileInfo.FullName);

            // only copy config-file to backup-file if config-file differs from fake config
            if (realConfig != fakeConfig)
            {
                BackupOfStructureMapConfigFileInfo.Delete();
                StructureMapConfigFileInfo.CopyTo(BackupOfStructureMapConfigFileInfo.FullName);
                fakeStructureMapConfigFileInfo.CopyTo(StructureMapConfigFileInfo.FullName, true);
            }
        }

        public static void RemoveFakes()
        {
            // if no backup exists we cannot restore anything, exit silently
            if (!BackupOfStructureMapConfigFileInfo.Exists)
            {
                return;
            }

            BackupOfStructureMapConfigFileInfo.CopyTo(StructureMapConfigFileInfo.FullName, true);
        }
    }

}
