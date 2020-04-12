﻿using System;
namespace LadeskabCore.USBCharger
{
   
    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
        void SimulateConnected(bool connected);
        void SimulateOverload(bool overload);
    }
}
