using System.Collections.Generic;

namespace IoTShop.Common.Logic.Models
{
    public class Framework
    {
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
            if (obj is Framework)
            {
                return (obj as Framework).ID == ID;
            }

            return false;
        }
    }
}