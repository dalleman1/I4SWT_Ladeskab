using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.Door
{
    public enum DoorStates
    {
        DoorUnlocked,
        DoorLocked
    }

    public class DoorTriggeredEventArgs : EventArgs
    {
        public DoorStates _doorstate { get; private set; }
        public DoorTriggeredEventArgs(DoorStates state)
        {
            _doorstate = state;
        }
    }
}