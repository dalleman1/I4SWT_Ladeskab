using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.ChargeControl
{
    public enum ChargeStates
    {
        NoConnection,
        FullyCharged,
        Charging,
        Error
    }

    public class ChargeTriggeredEventArgs : EventArgs
    {
        private ChargeStates _chargeState { get; set; }

        public ChargeTriggeredEventArgs(ChargeStates state)
        {
            _chargeState = state;
        }
    }
}
