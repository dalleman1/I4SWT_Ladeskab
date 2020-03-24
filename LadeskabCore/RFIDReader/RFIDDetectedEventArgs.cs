using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.RFIDReader
{
    public class RFIDDetectedEventsArgs : EventArgs
    {
        public RFIDDetectedEventsArgs(int id_)
        {
            ID = id_;
        }
        public int ID { get; private set;}
    }
}
