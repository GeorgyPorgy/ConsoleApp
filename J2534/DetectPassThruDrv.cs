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

        static public List<PassThruDevice> ListDevices()
        {
            List<PassThruDevice> j2534Devices = new List<PassThruDevice>();

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

                j2534Devices.Add(new PassThruDevice(
                   (string)deviceKey.GetValue("Vendor", ""),
                   (string)deviceKey.GetValue("Name", ""),
                   (string)deviceKey.GetValue("ConfigApplication", ""),
                   (string)deviceKey.GetValue("FunctionLibrary", ""),

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
}
