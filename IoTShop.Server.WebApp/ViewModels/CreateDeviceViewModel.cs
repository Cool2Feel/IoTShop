using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTShop.Server.WebApp.ViewModels
{
    public class CreateDeviceViewModel
    {
        public SelectList AllOSs { get; set; }
        public SelectList AllFrameworks { get; set; }
        public List<int> SelectedOSs { get; set; }
        public List<int> SelectedFrameworks { get; set; }
        public Device TheDevice { get; set; }
    }
}