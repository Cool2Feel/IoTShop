using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IoTShop.Common.Logic.Repositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public IEnumerable<Order> GetOrdersForUser(string userId)
        {
            return (from order in dbSet.Include(set => set.Device).Include(set => set.OrderLine)
                    where order.User.Id == userId && (order.OrderLine == null || order.OrderLine.Delivered == false)
                    select order).ToList<Order>();
        }
    }
}
