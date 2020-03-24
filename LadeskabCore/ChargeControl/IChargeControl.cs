namespace LadeskabCore.ChargeControl
{
    public interface IChargeControl
    {
        bool IsConnected();

        void StartCharge();

        void StopCharge();
    }
}