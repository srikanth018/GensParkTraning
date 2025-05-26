using System;
using System.IO;

namespace DesignPatternExamples.Singleton
{
    public class LogfileService : ILogFileUpdater
    {
        private string _logFilePath;

        public LogfileService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void writeLog(string message)
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(message);
            }
        }

        public void writeLog(string username, string role, string action)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] User: {username} | Role: {role} | Action: {action}";
            writeLog(logEntry);
        }
    }
}
