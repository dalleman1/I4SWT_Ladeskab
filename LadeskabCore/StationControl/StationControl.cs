using LadeskabCore.USBCharger;
using LadeskabCore.RFIDReader;
using LadeskabCore.Door;
using LadeskabCore.LogFile;
using LadeskabCore.ChargeControl;
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
        readonly ChargeControl.ChargeControl charger = new ChargeControl.ChargeControl(new UsbChargerSimulator());
        readonly Door.Door door = new Door.Door();
        readonly Display.Display display = new Display.Display();

        private enum CabinState
        {
            Available,
            Locked,
            DoorOpen
        };

        public StationControl(){}

        public StationControl(RFIDReader.RFIDReader RFIDPub, ChargeControl.ChargeControl CHARGEPub, Door.Door DoorPub)
        {
            RFIDPub.RaiseDetectEvent += HandleDetectEventRFID;
            CHARGEPub.RaisedChargeEvent += HandleDetectedEventCharge;
            DoorPub.DoorChangedEvent += HandleDetectEventDoor;
            //:O
        }

        public void HandleDetectEventDoor(object sender, DoorTriggeredEventArgs e)
        {
            
        }

        public void HandleDetectEventRFID(object sender, RFIDDetectedEventsArgs e)
        { 
            CheckID(_oldID,e.ID);
        }

        public void HandleDetectedEventCharge(object sender, ChargeTriggeredEventArgs e)
        {
            switch (e._chargeState)
            {
                case ChargeStates.Charging:
                    display.Charging();
                    break;
                case ChargeStates.FullyCharged:
                    display.RemovePhone();
                    break;
                case ChargeStates.NoConnection:
                    display.ConnectionError();
                    break;
                case ChargeStates.Error:
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
            Console.WriteLine("Hello");
        }

    }
}