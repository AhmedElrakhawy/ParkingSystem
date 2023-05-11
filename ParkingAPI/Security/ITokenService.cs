using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAPI.Security
{
    public interface ITokenService
    {
        ApiToken GenerateToken(int userId);
        bool ValidateToken(Guid TokenId);
        bool KillToken(int userId);
    }
}
