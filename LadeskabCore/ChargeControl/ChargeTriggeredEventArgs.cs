﻿using System;

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
        public ChargeStates _chargeState { get; private set; }
        public ChargeTriggeredEventArgs(ChargeStates state)
        {
            _chargeState = state;
        }
    }
}
