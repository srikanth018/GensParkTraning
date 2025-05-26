using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternExamples.Singleton
{
    public interface ILogFileUpdater
    {
        void writeLog(string message);
    }
}
