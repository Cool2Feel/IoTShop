using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IoTShop.Common.Logic.Context;

namespace IoTShop.Common.Logic.Repositories
{
    public class DeviceRepo : GenericRepo<Device>
    {
        public override IEnumerable<Device> All()
        {
            return (from device in dbSet.Include(set => set.OS).Include(set => set.Framework)
                    select device).ToList<Device>();
        }

        public override Device GetByID(object id)
        {
            return (from device in dbSet.Include(set => set.OS).Include(set => set.Framework)
                    where device.ID == (int)id
                    select device).SingleOrDefault<Device>();
        }
    }
}
