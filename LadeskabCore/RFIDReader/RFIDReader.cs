using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.RFIDReader
{
    public class RFIDReader : IRFIDReader
    {
        public event EventHandler<RFIDDetectedEventsArgs> RaiseDetectEvent;

        public void ThreadRun()
        {
            // This code receives a signal from a imaginary RFID-reader.
            // When this signal is passed we trigger our RFIDDetectedEvent.
            // Furthermore there is some id associated with the reader.
        }

        public void RaiseRandomEvent(int id)
        {
            OnDetectEvent(new RFIDDetectedEventsArgs(id));
        }

        protected virtual void OnDetectEvent(RFIDDetectedEventsArgs e)
        {
            EventHandler<RFIDDetectedEventsArgs> handler = RaiseDetectEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }
    }
}
