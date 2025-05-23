using DesignPatternExamples.ProxyTask;
using DesignPatternExamples.Singleton;

namespace DesignPatternExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogFileManager.Instance();
            ManageFileAccess manageFileAccess = new ManageFileAccess();
            manageFileAccess.Run();
        }
    }
}
