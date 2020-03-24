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

        readonly Display.Display display = new Display.Display();

        private RFIDReader.RFIDReader reader;
        private ChargeControl.ChargeControl chargeControl;
        private Door.Door door;
        private LogFile.LogFile log;

        private enum CabinState
        {
            Available,
            Locked,
            DoorOpen
        };

        public StationControl()
        {
            reader = new RFIDReader.RFIDReader();
            chargeControl = new ChargeControl.ChargeControl();
            door = new Door.Door();
            log = new LogFile.LogFile(@"temp.txt");

            // Subscribe to event
            reader.RaiseDetectEvent += HandleDetectEventRFID;
            chargeControl.RaisedChargeEvent += HandleDetectedEventCharge;
            door.DoorChangedEvent += HandleDetectEventDoor;
        }

        
        public void HandleDetectEventDoor(object sender, DoorTriggeredEventArgs e)
        {
            switch(e._doorstate)
            {
                case DoorStates.DoorLocked:
                    {
                        display.Charging();
                    } break;
                case DoorStates.DoorUnlocked:
                    {
                        if(!Isconnected())
                        {
                            display.ConnectPhone();
                        }
                    } break;
                default:
                    break;
            }
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
                    reader.RaiseRandomEvent(123);
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
            if (chargeControl.IsConnected() == true)
            {
                return true;
            }
            else
            {
                return false;
            }         
        }

        public void StartCharge()
        {
            reader.RaiseRandomEvent(123);
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
                        door.Unlock();
                        log.LogDoorLocked(_ID);
                        display.ConnectPhone();
                        _oldID = ID;
                        chargeControl.StartCharge();
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
                        chargeControl.StopCharge();
                        door.Unlock();
                        log.LogDoorUnlocked(_ID);
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
    }
}