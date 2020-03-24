using System;

namespace LadeskabCore.RFIDReader
{
    public class RFIDDetectedEventsArgs : EventArgs
    {
        public RFIDDetectedEventsArgs(int id_)
        {
            ID = id_;
        }
        public int ID { get; set;}
    }
}
