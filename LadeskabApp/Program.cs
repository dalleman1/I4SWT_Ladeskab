using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeskabCore;
using LadeskabCore.StationControl;
using System.Windows.Input;

namespace LadeskabApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            IStationControl Controller = new StationControl();

            while(!Controller.Isconnected())
            {
                
            }

            Controller.StartCharge();

            while(true) { 
                
            }
        }
    }
}
