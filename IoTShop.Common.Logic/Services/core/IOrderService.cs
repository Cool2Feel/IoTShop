using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Services
{
    public interface IOrderService
    {
        void Order(int deviceId, int quantity, string userId);
        IEnumerable<Order> GetOrdersForUser(string v);
        void Remove(int id);
        void CheckOut(ApplicationUser v);
    }
}
