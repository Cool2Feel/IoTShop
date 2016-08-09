using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IoTShop.Server.Api.Controllers
{
    public class DeviceController : ApiController
    {
        private IDeviceService _deviceService;

        public DeviceController()
        {
            _deviceService = new ApiDeviceService();
        }

        [HttpGet]
        [Route("api/devices/index")]
        public ApiData Index()
        {
            try
            {
                return new ApiSuccess<IEnumerable<Device>>()
                {
                    Data = _deviceService.GetDevices(),
                    Statut = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ApiError()
                {
                    Message = ex.Message,
                    Statut = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpGet]
        [Route("api/devices/details/{id}")]
        public ApiData Details(int id)
        {
            try
            {
                return new ApiSuccess<Device>()
                {
                    Data = _deviceService.GetDevice(id),
                    Statut = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ApiError()
                {
                    Message = ex.Message,
                    Statut = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
