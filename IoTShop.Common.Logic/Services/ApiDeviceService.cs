using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTShop.Common.Logic.Models;

namespace IoTShop.Common.Logic.Services
{
    public class ApiDeviceService : DeviceService
    {
        public override IEnumerable<Device> GetDevices()
        {
            return _deviceRepo.All().Select(device =>
            {
                return UpdatePicture(device);
            }).ToList();
        }

        public override Device GetDevice(int id)
        {
            return UpdatePicture(_deviceRepo.GetByID(id));
        }

        private Device UpdatePicture(Device device)
        {   
            if (device.Picture != String.Empty && device.Picture != null)
            {
                device.Picture = "https://iotshopstorage.blob.core.windows.net/images/" + device.Picture;
            }
            else
            {
                device.Picture = null;
            }
            
            return device;
        }
    }
}
