using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTShop.Common.Logic.Models
{
    public class OS
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Device> Devices { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is OS)
            {
                return (obj as OS).ID == ID;
            }

            return false;
        }
    }
}