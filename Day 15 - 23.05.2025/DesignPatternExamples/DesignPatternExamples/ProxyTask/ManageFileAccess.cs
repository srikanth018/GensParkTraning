using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternExamples.ProxyTask
{
    public class ManageFileAccess
    {
        public User GetData()
        {
            Console.Write("Enter your username:");
            string userName = Console.ReadLine();
            Console.Write("Enter your role (admin/user/guest):");
            string role = Console.ReadLine();
            return new User
            {
                UserName = userName,
                Role = role
            };
        }
        public void Run()
        {
            User user = GetData();
            IFile fileService = new FileService();
            IFile proxyFile = new ProxyFile(fileService,user);
            proxyFile.Read();

        }
    }
}
