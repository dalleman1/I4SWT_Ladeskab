namespace LadeskabCore.StationControl
{
    public class StationControl : IStationControl
    {
        public int _oldID { get; set; }
        public int _ID { get; set; }
        ChargeControl.ChargeControl CC = new ChargeControl.ChargeControl();

        public bool Isconnected()
        {
            if (CC.IsConnected() == true)
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

            if (oldID == ID)
            {
                //do something
            }

        }

        public void ChargeMessage()
        {

        }

        public void StartCharge()
        {

        }

        public void StopCharge()
        {

        }

        public void LockDoor()
        {

        }

        public void UnlockDoor()
        {

        }

    }
}