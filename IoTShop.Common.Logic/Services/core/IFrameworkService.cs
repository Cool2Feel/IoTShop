using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Services
{
    public interface IFrameworkService
    {
        IEnumerable<Framework> GetAll();
    }
}
