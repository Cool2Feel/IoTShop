using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Repositories;
using System.Data.Entity;

namespace IoTShop.Common.Logic.Services
{
    public class DeviceService : IDeviceService
    {
        internal IGenericRepo<Device> _deviceRepo;
        internal IGenericRepo<OS> _osRepo;
        internal IGenericRepo<Framework> _frameworkRepo;

        public DeviceService()
        {
            _deviceRepo = new DeviceRepo();
            _osRepo = new GenericRepo<OS>();
            _frameworkRepo = new GenericRepo<Framework>();
        }

        public virtual IEnumerable<Device> GetDevices()
        {
            return _deviceRepo.All();
        }

        public virtual Device GetDevice(int id)
        {
            return _deviceRepo.GetByID(id);
        }

        public void Insert(Device device, IEnumerable<int> selectedFrameworks, IEnumerable<int> selectedOSs)
        {
            DoAction(device, selectedFrameworks, selectedOSs, d => _deviceRepo.Insert(d));
        }

        public void Update(Device device, IEnumerable<int> selectedFrameworks, IEnumerable<int> selectedOSs)
        {
            DoAction(device, selectedFrameworks, selectedOSs, _deviceRepo.Update);
        }

        private void DoAction(Device device, IEnumerable<int> selectedFrameworks, IEnumerable<int> selectedOSs, DeviceServiceDelegate deviceAction)
        {
            device.Framework = GiveObjects<Framework>(selectedFrameworks, _frameworkRepo);
            device.OS = GiveObjects<OS>(selectedOSs, _osRepo);

            SetStateUnchanged<Framework>(device.Framework);
            SetStateUnchanged<OS>(device.OS);

            deviceAction(device);
            _deviceRepo.SaveChanges();
        }

        private void SetStateUnchanged<T>(IEnumerable<T> list) where T : class
        {
            foreach (T item in list)
            {
                _deviceRepo.SetEntityState(item, EntityState.Unchanged);
            }
        }

        private List<T> GiveObjects<T>(IEnumerable<int> selectedIds, IGenericRepo<T> repo) where T : class
        {
            List<T> objects = new List<T>();

            foreach (int id in selectedIds)
            {
                objects.Add(repo.GetByID(id));
            }

            return objects;
        }
    }
}
