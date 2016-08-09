using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Repositories;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepo _orderRepo;
        private IGenericRepo<OrderLine> _orderLineRepo;
        private IGenericRepo<Device> _deviceRepo;

        public OrderService()
        {
            _orderRepo = new OrderRepo();
            _orderLineRepo = new GenericRepo<OrderLine>();
            _deviceRepo = new DeviceRepo();
        }

        public void Order(int deviceId, int quantity, string userId)
        {
            Device device = _deviceRepo.GetByID(deviceId);
            
            _orderRepo.Insert(new Order()
            {
                Device = device,
                UserID = userId,
                Quantity = quantity
            });

            _orderRepo.SetEntityState(device, EntityState.Unchanged);
            _orderRepo.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersForUser(string userId)
        {
            return _orderRepo.GetOrdersForUser(userId);
        }
        
        public void Remove(int id)
        {
            _orderRepo.Delete(id);
            _orderRepo.SaveChanges();
        }

        public void CheckOut(ApplicationUser user)
        {
            IEnumerable<Order> orders = _orderRepo.GetOrdersForUser(user.Id);
            List<Order> newOrders = new List<Order>();

            orders.Select(order => {

                if (order.OrderLine == null)
                {
                    newOrders.Add(order);
                    _orderLineRepo.SetEntityState(order, EntityState.Unchanged);
                }

                return order;
            });
            
            OrderLine line = new OrderLine()
            {
                User = user,
                Orders = newOrders,
                Date = DateTime.Now,
                Delivered = false
            };

            _orderLineRepo.SetEntityState(user, EntityState.Unchanged);
            _orderLineRepo.Insert(line);
            _orderLineRepo.SaveChanges();

            //add to queue

            //string json = JsonConvert.SerializeObject(line);
        }
    }
}
