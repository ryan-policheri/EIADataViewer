using Microsoft.Extensions.Logging;

namespace DotNetCommon.Logging.File
{
    public class FileLoggerConfig
    {
        public FileLoggerConfig(string logFileDirectory, string logFileFullPath)
        {
            LogFileDirectory = logFileDirectory;
            LogFileFullPath = logFileFullPath;
        }

        public string LogFileDirectory { get; }
        public string LogFileFullPath { get; }

        public bool CanLog(LogLevel logLevel)
        {
            return true;
        }
    }
}
