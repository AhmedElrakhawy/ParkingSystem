using ParkingAPI.Models;
using ParkingAPI.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingAPI.Controllers
{
    [APIAuthenticationFilter]
    public class AuthenticateController : ApiController
    {
        public HttpResponseMessage Authenticate()
        {
            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;

                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
            return null;
        }

        private HttpResponseMessage GetAuthToken(int userId)
        {
            ITokenService _Token = new TokenService();

            var token = _Token.GenerateToken(userId);
            var Response = Request.CreateResponse(HttpStatusCode.OK, new ResponseObj()
            {
                Response = "Authorized",
                ResponseCode = "100", 
                ResponseMessage = "Authorized User", 
                ResponseMessageAr = "تم التسجيل بنجاح",
            });
            Response.Headers.Add("Token", token.authToken);
            Response.Headers.Add("expiryDate", ConfigurationManager.AppSettings["TokenExpiresDate"]);

            return Response;
        }
    }
}
