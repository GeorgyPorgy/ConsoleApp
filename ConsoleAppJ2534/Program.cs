using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2534;

namespace ConsoleAppJ2534
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nList avalible J2534 interfaces ...");
            List<PassThruDevice> AvalibleInterfaces = DetectPassThruDrv.ListDevices();

            if (AvalibleInterfaces.Count == 0 )
            {
                Console.WriteLine("\nInterface was not found!");
                return;
            }

            int IntfCntr = 1;
            foreach (PassThruDevice _interface in AvalibleInterfaces)
            {
                //1.Volvo Dice-234500
                //2.DruwTech DTC-271
                Console.WriteLine(string.Format("{0}.{1}   {2}",IntfCntr,_interface.Vendor, _interface.Name));
                IntfCntr++;
            }

            Console.WriteLine("Select interface : ");

            IPassThru Interface = DynamicPassThru.GetInstance("*.Dll");

            Console.WriteLine("Opening Device");
        }
    }
}
