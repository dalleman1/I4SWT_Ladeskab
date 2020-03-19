using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.RFIDReader
{
    class RFIDDetectedEventsArgs : EventArgs
    {
        public RFIDDetectedEventsArgs(int id_)
        {
            ID = id_;
        }
        private int ID { get; }
    }
}
