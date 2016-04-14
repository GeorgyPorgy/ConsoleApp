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
               
                Console.WriteLine(string.Format("{0}.{1}      [{2}]",IntfCntr, _interface.Name, _interface.Vendor));
                IntfCntr++;
            }


            int SelectedOption = 0;
            while (SelectedOption == 0)
            {
                Console.Write("\nSelect interface : ");
                if (int.TryParse(Console.ReadLine(), out SelectedOption))
                {
                    if (SelectedOption > 0 & SelectedOption <= AvalibleInterfaces.Count) break;
                }
                SelectedOption = 0;
            }

            IPassThru Interface = DynamicPassThru.GetInstance(AvalibleInterfaces[SelectedOption - 1].FunctionLibrary);

            Console.WriteLine("Opening Device");


            Interface.Dispose();


        }
    }
}
