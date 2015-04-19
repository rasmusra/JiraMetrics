using System;
using System.IO;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public class IisExpressManager
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

        public static void Start()
        {
            var iisExe = Path.Combine(ProgramFilesPath, "IIS Express", "iisexpress.exe");
            var iisArgs = string.Format("/path:\"{0}\" /port:{1}",
                new DirectoryInfo(@"..\..\..\Olifant.JiraMetrics.Web").FullName, Port);

            WinProcessWrapper.Start(
                iisExe, 
                iisArgs);
        }

        public static void Kill()
        {
            WinProcessWrapper.KillByName("iisexpress");
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
