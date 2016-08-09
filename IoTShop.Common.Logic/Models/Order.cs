using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Device Device { get; set; }
        public int DeviceID { get; set; }
        public ApplicationUser User { get; set; }
        public string UserID { get; set; }
        public int Quantity { get; set; }
        public OrderLine OrderLine { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Order)
            {
                return (obj as Order).ID == ID;
            }

            return false;
        }
    }
}
