using LadeskabCore.USBCharger;
using LadeskabCore.RFIDReader;
using LadeskabCore.Door;
using LadeskabCore.LogFile;
using LadeskabCore.ChargeControl;
using System;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using LadeskabCore.Display;

namespace LadeskabCore.StationControl
{
    public class StationControl : IStationControl
    {
        public int _oldID { get; set; }
        public int _ID { get; set; }
        private CabinState _state;

        private IDisplay display;
        private IRFIDReader reader;
        private IChargeControl chargeControl;
        private IDoor door;
        private ILogFile log;

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
            display = new Display.Display();

            // Subscribe to event
            reader.RaiseDetectEvent += HandleDetectEventRFID;
            chargeControl.RaisedChargeEvent += HandleDetectEventCharge;
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

        public void HandleDetectEventCharge(object sender, ChargeTriggeredEventArgs e)
        {
            switch (e._chargeState)
            {
                case ChargeStates.Charging:
                    display.Charging();
                    break;
                case ChargeStates.FullyCharged:
                    display.RemovePhone();
                    reader.RaiseRandomEvent(123);
                    break;
                case ChargeStates.NoConnection:
                    Console.WriteLine("No phone is curently connected.\n");
                    display.ConnectPhone();
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
                        _oldID = ID;
                        chargeControl.StartCharge();
                        display.Charging();
                        _state = CabinState.Locked;
                    }
                    else
                    {
                        display.ConnectPhone();
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