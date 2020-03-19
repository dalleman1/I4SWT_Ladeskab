using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LadeskabCore.Door
{
    public class Door : IDoor
    {
        public event EventHandler<DoorTriggeredEventArgs> DoorChangedEvent;

        public void Lock()
        {
            OnDoorEvent(new DoorTriggeredEventArgs(DoorStates.DoorLocked));
        }

        public void Unlock()
        {
            OnDoorEvent(new DoorTriggeredEventArgs(DoorStates.DoorUnlocked));
        }

        protected virtual void OnDoorEvent(DoorTriggeredEventArgs e)
        {
            EventHandler<DoorTriggeredEventArgs> handler = DoorChangedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
