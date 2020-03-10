using System;

namespace LadeskabCore.Display
{
    public class Display : IDisplay
    {
        StationControl.StationControl control = new StationControl.StationControl(); //Instance of StationControl.

        public void ConnectPhone()
        {
            if(control.Isconnected() != true) //checks wether stationcontrol is connected or not
                Console.WriteLine("Please connect your phone to the charging cabin.\n");
        }

        public void RemovePhone()
        {
            Console.WriteLine("Phone is fully charged. Please remove your phone.\n");
        }

        public void InsertRFID()
        {
            Console.WriteLine("Place RFID chip on the lock, please.\n");
        }

        public void ConnectionError()
        {
            Console.WriteLine("ERROR! The phone is not properply connected.\n");
        }

        public void RFIDError()
        {
            Console.WriteLine("ERROR! The chip does not currently belong to this cabin.\n");
        }

        public void Charging()
        {
            Console.WriteLine("User: " + control.ID + "'s phone is currently charging.\n");
        }

    }
}