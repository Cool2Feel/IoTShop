using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Repositories
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        IEnumerable<Order> GetOrdersForUser(string userId);
    }
}
