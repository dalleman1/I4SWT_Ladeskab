using System;
using System.IO;
using System.Reflection.Emit;

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

        public StationControl(RFIDReader.RFIDReader publisher)
        {
            publisher.RaiseDetectEvent += HandleDetectEvent;
        }

        private void HandleDetectEvent(object sender, EventArgs e)
        { 
            CheckID(_oldID,e.ID);
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
                        StartCharge();
                        _oldID = ID;
                        using (var writer = File.AppendText("logFile")) //Replace "logFile" with actual Logfile class 
                        {
                            writer.WriteLine(DateTime.Now + ": Cabin locked with RFID: {0}", ID);
                        }
                        ChargeMessage();
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

        public void StartCharge()
        {
            charger.StartCharge();
        }

        public void StopCharge()
        {
            charger.StopCharge();
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