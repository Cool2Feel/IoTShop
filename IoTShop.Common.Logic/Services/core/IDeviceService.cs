using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Services
{
    public interface IDeviceService
    {
        IEnumerable<Device> GetDevices();
        Device GetDevice(int id);
        void Insert(Device device, IEnumerable<int> selectedFrameworks, IEnumerable<int> selectedOSs);
        void Update(Device device, IEnumerable<int> selectedFrameworks, IEnumerable<int> selectedOSs);
    }
}
