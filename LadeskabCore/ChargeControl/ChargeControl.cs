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
        UsbChargerSimulator usb = new UsbChargerSimulator();

        public double CurrentUSBValue { get; set; }

        public ChargeControl(IUsbCharger usbCharger)
        {
            usbCharger.CurrentValueEvent += HandleChargeEvent;
        }

        public void HandleChargeEvent(object sender, CurrentEventArgs e)
        {
            CurrentUSBValue = e.Current;
            //if (CurrentUSBValue != usb.Connected)
            //{

            //}
            StartCharge();
        }
        public bool IsConnected()
        {
            return true;
        }

        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }
    }
}
