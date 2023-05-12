using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAPI.Security
{
    public interface IUserService
    {
        int Authenticate(string UserName, string Password);
    }
}
