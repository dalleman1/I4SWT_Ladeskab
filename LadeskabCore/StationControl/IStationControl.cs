using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.StationControl
{
    public interface IStationControl
    {
        void LockDoor();

        void UnlockDoor();
    }
}
