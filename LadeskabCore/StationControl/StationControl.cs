using System;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace LadeskabCore.StationControl
{
    public class StationControl : IStationControl
    {
        public int _oldID { get; set; }
        public int _ID { get; set; }
        private CabinState _state;
        readonly ChargeControl.ChargeControl charger = new ChargeControl.ChargeControl();
        readonly Door.Door door = new Door.Door();
        readonly Display.Display display = new Display.Display();

        private enum CabinState
        {
            Available,
            Locked,
            DoorOpen
        };

        public StationControl(RFIDReader.RFIDReader RFIDPub, ChargeControl.ChargeControl CHARGEPub, Door.Door DoorPub)
        {
            RFIDPub.RaiseDetectEvent += HandleDetectEventRFID;
            CHARGEPub.RaisedChargeEvent += HandleDetectedEventCharge;
            DoorPub.RaisedChargeEvent += HandleDetectEventDoor;
        }

        private void HandleDetectEventDoor(object sender, EventArgs e)
        {

        }

        private void HandleDetectEventRFID(object sender, EventArgs e)
        { 
            CheckID(_oldID,e.ID);
        }

        private void HandleDetectedEventCharge(object sender, EventArgs e)
        {
            switch (e.state)
            {
                case Charging:
                    display.Charging();
                    break;
                case FullyCharged:
                    display.RemovePhone();
                    break;
                case NoConnection:
                    display.ConnectionError();
                    break;
                case Error:
                    Console.WriteLine("Phone not connected, please reconnect.");
                    break;
                default:
                    break;
            }
        }

        public bool Isconnected()
        {
            if (charger.IsConnected() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CheckID(int oldID, int ID)
        {
            _oldID = oldID;
            _ID = ID;

            switch (_state)
            {
                case CabinState.Available:
                    // Check for cabin connection
                    if (Isconnected())
                    {
                        UnlockDoor();
                        display.ConnectPhone();
                        _oldID = ID;
                        using (var writer = File.AppendText("logFile")) //Replace "logFile" with actual Logfile class 
                        {
                            writer.WriteLine(DateTime.Now + ": Cabin locked with RFID: {0}", ID);
                        }
                        display.Charging();
                        _state = CabinState.Locked;
                    }
                    else
                    {
                        display.ConnectionError();
                    }

                    break;

                case CabinState.DoorOpen:
                    // Ignore
                    break;

                case CabinState.Locked:
                    // Check for correct ID
                    if (ID == _oldID)
                    {
                        charger.StopCharge();
                        door.Unlock();
                        using (var writer = File.AppendText("logFile")) //Replace "logFile" with actual Logfile class 
                        {
                            writer.WriteLine(DateTime.Now + ": cabin unlocked with RFID: {0}", ID);
                        }

                        display.RemovePhone();
                        _state = CabinState.Available;
                    }
                    else
                    {
                        display.RFIDError();
                    }

                    break;
            }
        }

        public void ChargeMessage()
        {
            display.Charging();
        }

        

        public void LockDoor()
        {
            door.Lock();
        }

        public void UnlockDoor()
        {
            door.Unlock();
        }

        static void main(string[] args)
        {

        }

    }
}