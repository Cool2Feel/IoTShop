using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Services
{
    public class OSService :IOSService
    {
        private IGenericRepo<OS> _osRepo;

        public OSService()
        {
            _osRepo = new GenericRepo<OS>();
        }

        public IEnumerable<OS> GetAll()
        {
            return _osRepo.All();
        }
    }
}
