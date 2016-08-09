using IoTShop.Common.Logic.Models;
using IoTShop.Common.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.AspNet.Identity.EntityFramework;
using IoTShop.Common.Logic.Context;
using IoTShop.Server.WebApp.ViewModels;

namespace IoTShop.Server.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IUserStore<ApplicationUser> _store; 
        private UserManager<ApplicationUser> _userManager;

        public OrderController()
        {
            _orderService = new OrderService();
            _store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            _userManager = new UserManager<ApplicationUser>(_store);
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["status"] != null && TempData["message"] != null)
            {
                ViewBag.Status = TempData["status"].ToString();
                ViewBag.Message = TempData["message"].ToString();
            }

            return View(_orderService.GetOrdersForUser(User.Identity.GetUserId()));
        }

        [HttpPost]
        [Route("Order/Order/{id}")]
        public ActionResult Order(int id, int quantity)
        {
            try {
                _orderService.Order(id, quantity, User.Identity.GetUserId());
            }
            catch(Exception ex)
            {
                TempData["status"] = "error";
                TempData["message"] = ex.Message;
                
                return RedirectToAction(nameof(Index));
            }

            TempData["status"] = "success";
            TempData["message"] = "Your device is added to your shopping cart";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Order/Remove/{id}")]
        public ActionResult Remove(int id)
        {
            _orderService.Remove(id);

            TempData["status"] = "success";
            TempData["message"] = "The device is removed from your cart";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            return View(new CheckOutViewModel());
        }

        [HttpPost]
        public ActionResult CheckOut(CheckOutViewModel vm)
        {
            _orderService.CheckOut(_userManager.FindByNameAsync(User.Identity.Name).Result);

            TempData["status"] = "success";
            TempData["message"] = "Your orders are registered in our system and will be deliverd in ~3 days*";

            return RedirectToAction(nameof(Index));
        }
    }
}