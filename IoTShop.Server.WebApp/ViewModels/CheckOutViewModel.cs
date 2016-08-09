using IoTShop.Common.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IoTShop.Server.WebApp.ViewModels
{
    public class CheckOutViewModel
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [Range(1000, 9999)]
        public int ZipCode { get; set; }
        [Required]
        public string Address { get; set; }
        public OrderLine Orderline { get; set; }
    }
}