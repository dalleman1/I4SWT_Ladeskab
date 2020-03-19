using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LadeskabCore
{
    interface ILogFile
    {

        void LogDoorLocked(int id);
        void LogDoorUnlocked(int id);
    }
}
