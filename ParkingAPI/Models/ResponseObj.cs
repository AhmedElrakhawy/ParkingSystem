using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingAPI.Models
{
    public class ResponseObj
    {
        public string Response { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseMessageAr { get; set; }
        public object Data { get; set; }
    }
}