using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternExamples.Singleton
{
    public class LogFileManager
    {
        private static LogFileManager _instance;
        private LogfileService _logger;

        private LogFileManager()
        {
            string logFilePath = "C:\\Users\\srikanthm\\Documents\\GensParkTraning\\Day 15 - 23.05.2025\\DesignPatternExamples\\DesignPatternExamples\\Singleton\\logFile.txt";
            _logger = new LogfileService(logFilePath);
        }

        public static LogFileManager Instance()
        {
            if (_instance == null)
            {
                Console.WriteLine("Instance Create First time");
                _instance = new LogFileManager();
                
            }
            return _instance;
        }

        public void Log(string username, string role, string action)
        {
            _logger.writeLog(username, role, action);
        }
    }

}
