using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using J2534;

namespace ConsoleAppJ2534
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool _continue = true;
            UserInterface ConsoleApp = new UserInterface();

            List<PassThruRegistryRecord> AvalibleRegistryRecord = DetectPassThruDrv.ListDevices();

            int SelectedOption =  ConsoleApp.SelectJ2534Interface(AvalibleRegistryRecord);

            if (SelectedOption == 0)
            {
                throw new System.ArgumentException("Failed to detect installed interface!", "original");
            }

            PassThruInterface CommunicationInterface = new PassThruInterface(ConsoleApp, AvalibleRegistryRecord[SelectedOption - 1]);

            CommunicationInterface.Connect();

            ConsoleApp.ShowMessage("Starting thread");
            CommunicationInterface.StartThread();

            ConsoleApp.ShowMessage("Enter QUIT to exit");

            while (CommunicationInterface._continue)
            {
                if (ConsoleApp.ReadLine()) CommunicationInterface.StopThread(); 
            }

            ConsoleApp.ShowMessage("Terminating thread");

            CommunicationInterface.Diconnect();         
        }
    }

    class UserInterface
    {
        private int SelectedOption;
        private int IntfCntr;
        private StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

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

        public bool ReadLine()
        {
            string message = Console.ReadLine();

            if (stringComparer.Equals("quit", message)) return true;

            return false;

        }
    }

    class PassThruInterface
    {
        private IPassThru _Instance;
        private UserInterface _UI;
        private PassThruRegistryRecord _SelectedJ2534Device;
        private PassThruDevice _device;
        private PassThruChannel _channel;
        private Thread readThread = null;
        public volatile bool _continue;


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
            string _FWVer, _DllVer , _ApiVer ;

            _UI.ShowMessage("Opening Device");
            _device = PassThruDevice.GetInstance(_Instance);
            _device.Open();

            _device.ReadVersion(out _FWVer, out _DllVer, out _ApiVer);
            _UI.ShowMessage(string.Format("Fw V{0}\nDll V{1}\nApi V{2}", _FWVer, _DllVer.Replace(',','.') , _ApiVer));

            _UI.ShowMessage("Opening Channel");
            _channel = _device.OpenChannel(
                PassThruProtocol.Can_XON_XOFF  ,
                PassThruConnectFlags.SETEK_Specific | PassThruConnectFlags.Can29BitID,
                PassThruBaudRate.Rate125K);

            _channel.InitializeSsm();


            return PassThruStatus.NoError;
        }
        
        public PassThruStatus Diconnect()
        {
            _UI.ShowMessage("Closing Channel");
            _channel.Close();

            _UI.ShowMessage("Closing Device");
            _device.Close();
            return PassThruStatus.NoError;
        }

        public void StartThread()
        {
            if (readThread == null)
            {
                readThread = new Thread(Read);
                _continue = true;

                readThread.Start();
            }
        }

        public void StopThread()
        {
            if (readThread != null)
            {
                _continue = false;
                readThread.Join();
            }

            readThread = null;
        }

        private void Read()
        {
            PassThruMsg received = new PassThruMsg(PassThruProtocol.Can_XON_XOFF);

            while (_continue)
            {
                /*bool success = _channel.ReadMessage(received, TimeSpan.FromMilliseconds(1000));

                if (success)
                {
                    _UI.ShowMessage(string.Format("Data {0}", received.Data.ToString()));
                }*/
                
            }

        }

    }
}
