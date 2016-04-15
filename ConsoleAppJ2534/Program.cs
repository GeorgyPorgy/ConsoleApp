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

            UserInterface ConsoleApp = new UserInterface();

            List<PassThruDevice> AvalibleInterfaces = DetectPassThruDrv.ListDevices();

            int SelectedOption =  ConsoleApp.SelectJ2534Interface(AvalibleInterfaces);

            if (SelectedOption != 0)
            {
                ConsoleApp.ShowMessage("Failed to load interface library");
                return;
            }

            IPassThru Interface = DynamicPassThru.GetInstance(AvalibleInterfaces[SelectedOption - 1].FunctionLibrary);

            ConsoleApp.ShowMessage("Opening Device");

            Interface.Dispose();
        }
    }

    class UserInterface
    {
        private int SelectedOption;
        private int IntfCntr;

        public int SelectJ2534Interface (List<PassThruDevice> AvalibleInterfaces)
        {

            Console.WriteLine("\nList of avalible interfaces:");

            if (AvalibleInterfaces.Count == 0)
            {
                Console.WriteLine("\nInterface was not found!");
                return 0;
            }

            IntfCntr = 1;
            foreach (PassThruDevice _interface in AvalibleInterfaces)
            {

                Console.WriteLine(string.Format("{0}.{1}      [{2}]", IntfCntr, _interface.Name, _interface.Vendor));
                IntfCntr++;
            }

            SelectedOption = 0;
            while (SelectedOption == 0)
            {
                Console.Write("\nSelect interface : ");
                if (int.TryParse(Console.ReadLine(), out SelectedOption))
                {
                    if (SelectedOption > 0 & SelectedOption <= AvalibleInterfaces.Count) break;
                }
                SelectedOption = 0;
            }

            return SelectedOption;
        }
        public void ShowMessage(string Msg)
        {
            Console.WriteLine("\n" + Msg);
        }
    }

    class PassThrueInterface
    {
        public int OpenChannel()
        {
            return 0;
        }
        
        public int CloseChannel()
        {
            return 0;
        }

    }
}
