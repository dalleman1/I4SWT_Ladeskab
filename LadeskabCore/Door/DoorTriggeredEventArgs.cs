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
        private DoorStates _doorstate { get; set; }

        public DoorTriggeredEventArgs(DoorStates state)
        {
            _doorstate = state;
        }
    }
}
