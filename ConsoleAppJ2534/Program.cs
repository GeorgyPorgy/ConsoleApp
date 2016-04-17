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

            List<PassThruRegistryRecord> AvalibleRegistryRecord = DetectPassThruDrv.ListDevices();

            int SelectedOption =  ConsoleApp.SelectJ2534Interface(AvalibleRegistryRecord);

            if (SelectedOption == 0)
            {
                throw new System.ArgumentException("Failed to detect installed interface!", "original");
            }

            PassThruInterface CommunicationInterface = new PassThruInterface(ConsoleApp, AvalibleRegistryRecord[SelectedOption - 1]);

            CommunicationInterface.Connect();
            CommunicationInterface.Diconnect();         

        }
    }

    class UserInterface
    {
        private int SelectedOption;
        private int IntfCntr;

        public int SelectJ2534Interface (List<PassThruRegistryRecord> AvalibleInterfaces)
        {

            Console.WriteLine("\nList of avalible interfaces:");

            if (AvalibleInterfaces.Count == 0)
            {
                Console.WriteLine("\nInterface was not found!");
                return 0;
            }

            IntfCntr = 1;
            foreach (PassThruRegistryRecord _interface in AvalibleInterfaces)
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
        public void ShowTrafic()
        {

        }
    }

    class PassThruInterface
    {
        private IPassThru _Instance;
        private UserInterface _UI;
        private PassThruRegistryRecord _SelectedJ2534Device;
        private PassThruDevice device;


        public PassThruInterface (UserInterface UI, PassThruRegistryRecord SelectedJ2534Device)
        {
            _UI = UI;
            _SelectedJ2534Device = SelectedJ2534Device;
            _Instance = DynamicPassThru.GetInstance(_SelectedJ2534Device.FunctionLibrary);
        }

        ~PassThruInterface()
        {
            if (_Instance != null) _Instance.Dispose();

            _Instance = null;
        }


        public PassThruStatus Connect()
        {

            _UI.ShowMessage("Opening Device");
            device = PassThruDevice.GetInstance(_Instance);
            device.Open();

            return PassThruStatus.NoError;
        }
        
        public PassThruStatus ReadMsg()
        {
            return PassThruStatus.NoError;
        }

        public PassThruStatus SendMsg()
        {
            return PassThruStatus.NoError;
        }

        public PassThruStatus Diconnect()
        {
            _UI.ShowMessage("Closing device");
            device.Close();
            return PassThruStatus.NoError;
        }

    }
}
