using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.Door
{
    public interface IDoor
    {
        void Run();

        void Lock(bool newstatus);

        void Unlock(bool newstatus);

        event EventHandler<DoorTriggeredEventArgs> DoorChangedEvent;
    }
}
