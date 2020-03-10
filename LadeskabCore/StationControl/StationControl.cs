using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabCore.Door;

namespace LadeskabCore.StationControl
{
    public class StationControl : IStationControl
    {
        public bool currentStatus { get; set; }

        public StationControl(IDoor Door)
        {
            Door.DoorChangedEvent += HandleStatusChangedEvent;
        }

        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }

        private void HandleStatusChangedEvent(object sender, DoorTriggeredEventArgs e)
        {
            currentStatus = e.Status;

            if (currentStatus == true)
            {
                UnlockDoor();
            }
            else
            {
                LockDoor();
            }
        }
    }
}
