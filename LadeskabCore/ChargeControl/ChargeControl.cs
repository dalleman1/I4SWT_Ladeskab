using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabCore.USBCharger;

namespace LadeskabCore.ChargeControl
{
    public class ChargeControl : IChargeControl
    {
        public IUsbCharger usb;

        public event EventHandler<ChargeTriggeredEventArgs> RaisedChargeEvent;

        public ChargeControl()
        {
            usb = new UsbChargerSimulator();
            usb.CurrentValueEvent += HandleChargeEvent;
        }

        public void HandleChargeEvent(object sender, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                OnChargeEvent(new ChargeTriggeredEventArgs(ChargeStates.NoConnection));
            }
            if (e.Current <= 500 && e.Current > 5)
            {
                OnChargeEvent(new ChargeTriggeredEventArgs(ChargeStates.Charging));
            }
            else if (e.Current > 0 && e.Current <= 5)
            {
                OnChargeEvent(new ChargeTriggeredEventArgs(ChargeStates.FullyCharged));
            }
            if (e.Current > 500)
            {
                OnChargeEvent(new ChargeTriggeredEventArgs(ChargeStates.Error));
            }
        }
        public bool IsConnected()
        {
            if (usb.Connected == true)
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
            usb.StartCharge();
        }

        public void StopCharge()
        {
            usb.StopCharge();
        }

        protected virtual void OnChargeEvent(ChargeTriggeredEventArgs e)
        {
            EventHandler<ChargeTriggeredEventArgs> handler = RaisedChargeEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
