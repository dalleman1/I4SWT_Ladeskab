using System;

namespace LadeskabCore.RFIDReader
{
    public interface IRFIDReader
    {
        void ThreadRun();
        void OnDetectEvent(RFIDDetectedEventsArgs e);
        void RaiseRandomEvent(int id);
        event EventHandler<RFIDDetectedEventsArgs> RaiseDetectEvent;
    }
}
