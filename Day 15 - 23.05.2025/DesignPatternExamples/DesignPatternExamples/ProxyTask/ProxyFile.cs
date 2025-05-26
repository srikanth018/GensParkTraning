using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternExamples.Singleton;

namespace DesignPatternExamples.ProxyTask
{
    public class ProxyFile : IFile
    {
        private IFile _fileService;
        private User _user;

        public ProxyFile(IFile fileService, User user)
        {
            _fileService = fileService;
            _user = user;
        }

        public void Read()
        {
            switch (_user.Role.ToLower())
            {
                case "admin":
                    LogFileManager.Instance().Log(_user.UserName, _user.Role, "Read sensitive file");
                    Console.WriteLine("[Access Granted] Reading sensitive file content...");
                    _fileService.Read();
                    break;
                case "user":
                    LogFileManager.Instance().Log(_user.UserName, _user.Role, "Read file metadata");
                    Console.WriteLine("[Access Limited] Showing file metadata...");
                    Console.WriteLine("Filename: Project.config | Size: 14KB | Created: 2023-05-10");
                    break;
                case "guest":
                    LogFileManager.Instance().Log(_user.UserName, _user.Role, "Attempted to read sensitive file");
                    Console.WriteLine("[Access Denied] You do not have permission to read this file.");
                    break;
                default:
                    Console.WriteLine("[Access Denied] Unknown role.");
                    break;
            }
        }
    }

}
