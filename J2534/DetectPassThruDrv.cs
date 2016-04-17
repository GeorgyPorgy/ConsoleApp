using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2534
{
    static public class DetectPassThruDrv
    {
        private const string Registry_path = "Software\\PassThruSupport.04.04";

        static public List<PassThruRegistryRecord> ListDevices()
        {
            List<PassThruRegistryRecord> j2534Devices = new List<PassThruRegistryRecord>();

            RegistryKey localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,RegistryView.Registry32).OpenSubKey(Registry_path);
            if (localKey == null)
            {
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,RegistryView.Registry64).OpenSubKey(Registry_path);
                if (localKey == null)
                    return j2534Devices;
            }

            foreach (string device in localKey.GetSubKeyNames())
            {
                RegistryKey deviceKey = localKey.OpenSubKey(device);
                if (deviceKey == null)
                    continue;

                j2534Devices.Add(new PassThruRegistryRecord(
                   (string)deviceKey.GetValue("Vendor", ""),
                   (string)deviceKey.GetValue("Name", ""),
                   (string)deviceKey.GetValue("FunctionLibrary", ""),
                   (string)deviceKey.GetValue("ConfigApplication", ""),

                   (int)deviceKey.GetValue("CAN", 0),
                   (int)deviceKey.GetValue("ISO15765", 0),
                   (int)deviceKey.GetValue("J1850PWM", 0),
                   (int)deviceKey.GetValue("J1850VPW", 0),
                   (int)deviceKey.GetValue("ISO9141", 0),
                   (int)deviceKey.GetValue("ISO14230", 0),
                   (int)deviceKey.GetValue("SCI_A_ENGINE", 0),
                   (int)deviceKey.GetValue("SCI_A_TRANS", 0),
                   (int)deviceKey.GetValue("SCI_B_ENGINE", 0),
                   (int)deviceKey.GetValue("SCI_B_TRANS", 0),
                   (int)deviceKey.GetValue("DiCECompatible", 0)));

                deviceKey.Close();
            }

            localKey.Close();
            return j2534Devices;
        }
    }

    public class PassThruRegistryRecord
    {
        public string Vendor { get; set; }
        public string Name { get; set; }
        public string FunctionLibrary { get; set; }
        public string ConfigApplication { get; set; }
        public int CANChannels { get; set; }
        public int ISO15765Channels { get; set; }
        public int J1850PWMChannels { get; set; }
        public int J1850VPWChannels { get; set; }
        public int ISO9141Channels { get; set; }
        public int ISO14230Channels { get; set; }
        public int SCI_A_ENGINEChannels { get; set; }
        public int SCI_A_TRANSChannels { get; set; }
        public int SCI_B_ENGINEChannels { get; set; }
        public int SCI_B_TRANSChannels { get; set; }
        public int DiCECompatible { get; set; }

        public PassThruRegistryRecord(string _Vendor, string _Name, string _FunctionLibrary, string _ConfigApplication,
            int _CANChannels, int _ISO15765Channels, int _J1850PWMChannels, int _J1850VPWChannels, int _ISO9141Channels,
            int _ISO14230Channels, int _SCI_A_ENGINEChannels, int _SCI_A_TRANSChannels, int _SCI_B_ENGINEChannels,
            int _SCI_B_TRANSChannels, int _DiCECompatible)
        {
            this.Vendor = _Vendor;
            this.Name = _Name;
            this.FunctionLibrary = _FunctionLibrary;
            this.ConfigApplication = _ConfigApplication;
            this.CANChannels = _CANChannels;
            this.ISO15765Channels = _ISO15765Channels;
            this.J1850PWMChannels = _J1850PWMChannels;
            this.J1850VPWChannels = _J1850VPWChannels;
            this.ISO9141Channels = _ISO9141Channels;
            this.ISO14230Channels = _ISO14230Channels;
            this.SCI_A_ENGINEChannels = _SCI_A_ENGINEChannels;
            this.SCI_A_TRANSChannels = _SCI_A_TRANSChannels;
            this.SCI_B_ENGINEChannels = _SCI_B_ENGINEChannels;
            this.SCI_B_TRANSChannels = _SCI_B_TRANSChannels;
            this.DiCECompatible = _DiCECompatible;
        }

        public bool IsCANSupported
        {
            get { return (CANChannels > 0 ? true : false); }
        }

        public bool IsISO15765Supported
        {
            get { return (ISO15765Channels > 0 ? true : false); }
        }

        public bool IsJ1850PWMSupported
        {
            get { return (J1850PWMChannels > 0 ? true : false); }
        }

        public bool IsJ1850VPWSupported
        {
            get { return (J1850VPWChannels > 0 ? true : false); }
        }

        public bool IsISO9141Supported
        {
            get { return (ISO9141Channels > 0 ? true : false); }
        }

        public bool IsISO14230Supported
        {
            get { return (ISO14230Channels > 0 ? true : false); }
        }

        public bool IsSCI_A_ENGINESupported
        {
            get { return (SCI_A_ENGINEChannels > 0 ? true : false); }
        }

        public bool IsSCI_A_TRANSSupported
        {
            get { return (SCI_A_TRANSChannels > 0 ? true : false); }
        }

        public bool IsSCI_B_ENGINESupported
        {
            get { return (SCI_B_ENGINEChannels > 0 ? true : false); }
        }

        public bool IsSCI_B_TRANSSupported
        {
            get { return (SCI_B_TRANSChannels > 0 ? true : false); }
        }

        public bool IsDiCECompatible
        {
            get { return (DiCECompatible > 0 ? true : false); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
