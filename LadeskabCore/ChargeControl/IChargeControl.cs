using System;
using LadeskabCore.USBCharger;

namespace LadeskabCore.ChargeControl
{
    public interface IChargeControl
    {
        bool IsConnected();
        void StopCharge();
        void StartCharge();
        event EventHandler<ChargeTriggeredEventArgs> RaisedChargeEvent;
        void HandleChargeEvent(object sender, CurrentEventArgs e);
        void OnChargeEvent(ChargeTriggeredEventArgs e);
    }
}
