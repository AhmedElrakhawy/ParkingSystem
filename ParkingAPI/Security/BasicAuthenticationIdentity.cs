using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ParkingAPI.Security
{
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public BasicAuthenticationIdentity(string userName , string password) : base(userName , "Basic")
        {
            UserName = userName;
            Password = password;
        }
    }
}