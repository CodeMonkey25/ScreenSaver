using System;
using System.IO;

namespace ScreenSaver
{
    public class Settings
    {
        // Environment.SpecialFolder.CommonApplicationData => C:\ProgramData
        // Environment.SpecialFolder.ApplicationData => C:\Users\jsarley\AppData\Roaming
        // Environment.SpecialFolder.LocalApplicationData => C:\Users\jsarley\AppData\Local

        internal static string LocalApplicationDataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        internal static string JmsDataPath => Path.Combine(LocalApplicationDataPath, "JMS");
        internal static string DataPath => Path.Combine(JmsDataPath, "ScreenSaver");
        internal static string LogFile => Path.Combine(DataPath, "Logs", "daily_.log");
    }
}