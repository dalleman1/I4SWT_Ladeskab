using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeskabCore.ChargeControl
{
    public interface IChargeControl
    {
        bool IsConnected();

        void StopCharge();

        void StartCharge();
    }
}
