using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IoTShop.Common.Logic.Models
{
    [NotMapped]
    public class ApiData
    {
        public HttpStatusCode Statut { get; set; }
    }

    [NotMapped]
    public class ApiSuccess<TClass> : ApiData where TClass : class
    {
        public TClass Data { get; set; }
    }

    [NotMapped]
    public class ApiError: ApiData
    {
        public string Message { get; set; }
    }
}
