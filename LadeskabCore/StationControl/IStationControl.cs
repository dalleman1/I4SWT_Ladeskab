using System;
using LadeskabCore.RFIDReader;
using LadeskabCore.Door;
using LadeskabCore.ChargeControl;

namespace LadeskabCore.StationControl
{
    public interface IStationControl
    {
        bool Isconnected();
        void CheckID(int oldID, int ID);
        void ChargeMessage();
        void LockDoor();
        void UnlockDoor();
        void HandleDetectEventRFID(object sender, RFIDDetectedEventsArgs e);
        void HandleDetectEventDoor(object sender, DoorTriggeredEventArgs e);
        void HandleDetectedEventCharge(object sender, ChargeTriggeredEventArgs e);
    }

}