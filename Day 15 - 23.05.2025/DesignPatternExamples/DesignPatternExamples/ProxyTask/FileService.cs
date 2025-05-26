using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternExamples.ProxyTask
{
    public class FileService : IFile
    {
        public void Read()
        {
            Console.WriteLine("Sensitive File Content: Project Configuration");
        }
    }
}
