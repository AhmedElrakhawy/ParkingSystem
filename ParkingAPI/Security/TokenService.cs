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
            try
            {
                using (var Context = new ParkingSystemEntities())
                {
                    var token = Context.ApiTokens.FirstOrDefault(x => x.UserId == userId);

                    Context.Entry(token).State = System.Data.EntityState.Deleted;

                    if (Context.SaveChanges() > 0)
                        return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }

        public bool ValidateToken(Guid TokenId)
        {
            try
            {
                using (var Context = new ParkingSystemEntities())
                {
                    var token = Context.ApiTokens.FirstOrDefault(x => x.TokenId == int.Parse(TokenId.ToString()));

                    if (token != null && DateTime.Parse(token.ExpiresOn) > DateTime.Now)
                        return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
    }
}