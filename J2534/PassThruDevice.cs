using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2534
{
    public class PassThruDevice
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

        public PassThruDevice (string _Vendor, string _Name, string _FunctionLibrary, string _ConfigApplication,
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
