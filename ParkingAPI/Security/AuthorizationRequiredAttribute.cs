using ParkingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ParkingAPI.Security
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "Token";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ITokenService Tokenservice = new TokenService();

            if (actionContext.Request.Headers.Contains(Token)) 
            {
                var TokenValue = actionContext.Request.Headers.GetValues(Token).First();
                if (Tokenservice != null && !Tokenservice.ValidateToken(Guid.Parse(TokenValue)))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new ResponseObj()
                    {
                        Response = "Token Invalid",
                        ResponseCode = "401", 
                        ResponseMessage = "Token is not exist", 
                        ResponseMessageAr ="فشل تسجيل الدخول"
                    });
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new ResponseObj()
                    {
                        Response = "Token attribute not found in the request header",
                        ResponseCode = "401",
                        ResponseMessage = "Token is not exist",
                        ResponseMessageAr = "فشل تسجيل الدخول"
                    });
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
}