using ParkingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingAPI.Security
{
    public class UserService : IUserService
    {
        public int Authenticate(string UserName , string Password)
        {
            using (var DbContext = new ParkingSystemEntities())
            {
                APIUser user = DbContext.APIUsers.Where(u => u.UserName == UserName && u.Password == Password).FirstOrDefault();
                if (user != null && user.UserId > 0)
                {
                    if ((bool)user.IsActive)
                    {
                        return user.UserId;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }
    }
}