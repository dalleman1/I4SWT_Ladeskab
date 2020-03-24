using System;

namespace LadeskabCore.Display
{
    public class Display : IDisplay
    {
        public void ConnectPhone()
        {
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
            Console.WriteLine("Phone is currently charging. To unlock, use RFID tag.\n");
        }

    }
}