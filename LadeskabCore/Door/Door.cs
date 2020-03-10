using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.Door
{
    public class Door : IDoor
    {
        private bool _oldStatus;
        public event EventHandler<DoorTriggeredEventArgs> DoorChangedEvent;

        public void Lock(bool newstatus)
        {
            if (newstatus != _oldStatus)
            {
                OnStatusChanged(new DoorTriggeredEventArgs { Status = newstatus });
                _oldStatus = newstatus;
                Console.WriteLine("\n Door locked");
            }
        }

        public void Run()
        {
            // TODO: Needs implementation
            throw new NotImplementedException();
        }

        public void Unlock(bool newstatus)
        {
            if (newstatus != _oldStatus)
            {
                OnStatusChanged(new DoorTriggeredEventArgs { Status = newstatus });
                _oldStatus = newstatus;
                Console.WriteLine("\n Door unlocked");
            }
        }

        protected virtual void OnStatusChanged(DoorTriggeredEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }
}
