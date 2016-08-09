using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Models
{
    public class OrderLine
    {
        public int ID { get; set; }
        public List<Order> Orders { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public bool Delivered { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is OrderLine)
            {
                return (obj as OrderLine).ID == ID;
            }

            return false;
        }
    }
}
