using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Services
{
    public class FrameworkService : IFrameworkService
    {
        private IGenericRepo<Framework> _framworkRepo;

        public FrameworkService()
        {
            _framworkRepo = new GenericRepo<Framework>();
        }

        public IEnumerable<Framework> GetAll()
        {
            return _framworkRepo.All();
        }
    }
}
