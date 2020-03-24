using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.RFIDReader
{
    public interface IRFIDReader
    {
        void ThreadRun();
        void OnDetectEvent(RFIDDetectedEventsArgs e);
    }
}
