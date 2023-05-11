using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ParkingAPI.Security
{
    public class TokenService : ITokenService
    {
        public ApiToken GenerateToken(int userId)
        {
            using (var context = new ParkingSystemEntities())
            {
                Guid uniqueKkey = Guid.NewGuid();
                DateTime IssuedOn = DateTime.Now;
                DateTime ExpiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["TokenExpiresDate"]));
                var Token = new ApiToken
                {
                    UserId = userId,
                    ExpiresOn = ExpiredOn.ToString(),
                    IssuedOn = IssuedOn.ToString(),
                    authToken = uniqueKkey.ToString()
                };
                context.ApiTokens.Add(Token);
                if (context.SaveChanges() > 0)
                {
                    return Token;
                }
                return null;
            }
        }

        public bool KillToken(int userId)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(Guid TokenId)
        {
            throw new NotImplementedException();
        }
    }
}