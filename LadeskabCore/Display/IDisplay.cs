namespace LadeskabCore.Display
{
    public interface IDisplay
    {
        void ConnectPhone();
        void RemovePhone();
        void InsertRFID();
        void ConnectionError();
        void RFIDError();
        void Charging();
    }
}