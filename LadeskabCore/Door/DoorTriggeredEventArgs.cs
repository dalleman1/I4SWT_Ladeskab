using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.Door
{
    public class DoorTriggeredEventArgs : EventArgs
    {
        public bool Status { get; set; }
    }
}
