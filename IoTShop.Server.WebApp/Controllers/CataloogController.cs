using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Services;
using IoTShop.Server.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTShop.Server.WebApp.Controllers
{
    [Authorize]
    public class CataloogController : Controller
    {
        private IDeviceService _deviceService;
        private IOSService _osService;
        private IFrameworkService _frameworkService;

        public CataloogController()
        {
            _deviceService = new DeviceService();
            _osService = new OSService();
            _frameworkService = new FrameworkService();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (TempData["status"] != null && TempData["message"] != null)
            {
                ViewBag.Status = TempData["status"].ToString();
                ViewBag.Message = TempData["message"].ToString();
            }

            if (TempData["id"] != null)
            {
                ViewBag.Id = TempData["id"];
            }

            return View(_deviceService.GetDevices());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Create new";
            
            return View(new CreateDeviceViewModel()
            {
                AllFrameworks = new SelectList(_frameworkService.GetAll(), "ID", "Name"),
                AllOSs = new SelectList(_osService.GetAll(), "ID", "Name")
            });
        }

        [HttpPost]
        public ActionResult Create(CreateDeviceViewModel deviceVm)
        {
            _deviceService.Insert(new Device()
            {
                RentPrice = deviceVm.TheDevice.RentPrice,
                Price = deviceVm.TheDevice.Price,
                Name = deviceVm.TheDevice.Name,
                Picture = deviceVm.TheDevice.Picture,
                Description = deviceVm.TheDevice.Description,
                Stock = deviceVm.TheDevice.Stock
            }, deviceVm.SelectedFrameworks, deviceVm.SelectedOSs);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Cataloog/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            TempData["status"] = "warning";
            TempData["message"] = "confirm to delete";
            TempData["id"] = id;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Cataloog/ConfirmDelete/{id}")]
        public ActionResult ConfirmDelete(int id, string confirmd)
        {
            switch (confirmd)
            {
                case "yes":
                    TempData["status"] = "success";
                    TempData["message"] = "Device is deleted";
                    break;

                case "no":
                    TempData["status"] = "success";
                    TempData["message"] = "Deletion canceled";
                    break;

                default:
                    TempData["status"] = "error";
                    TempData["message"] = "Error occured while deletion.";
                    break;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Cataloog/edit/{id}")]
        public ActionResult Edit(int id)
        {
            CreateDeviceViewModel vm = new CreateDeviceViewModel()
            {
                TheDevice = _deviceService.GetDevice(id),
                AllFrameworks = new SelectList(_frameworkService.GetAll(), "ID", "Name"),
                AllOSs = new SelectList(_osService.GetAll(), "ID", "Name")
            };

            ViewBag.Title = "Edit " + vm.TheDevice.Name;
            
            return View(vm);
        }

        [HttpPost]
        [Route("Cataloog/edit/{id}")]
        public ActionResult Edit(int id, CreateDeviceViewModel deviceVm)
        {
            try
            {
                _deviceService.Update(new Device()
                {
                    RentPrice = deviceVm.TheDevice.RentPrice,
                    Price = deviceVm.TheDevice.Price,
                    Name = deviceVm.TheDevice.Name,
                    Picture = deviceVm.TheDevice.Picture,
                    Description = deviceVm.TheDevice.Description,
                    Stock = deviceVm.TheDevice.Stock
                }, deviceVm.SelectedFrameworks, deviceVm.SelectedOSs);

                TempData["status"] = "success";
                TempData["message"] = "Device updated";
            }
            catch (Exception ex)
            {
                TempData["status"] = "error";
                TempData["message"] = $"{ex.Message} / {ex.InnerException?.Message}" ;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}