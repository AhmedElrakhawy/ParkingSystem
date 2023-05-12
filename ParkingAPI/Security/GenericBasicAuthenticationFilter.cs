using ParkingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ParkingAPI.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method , AllowMultiple = false)]
    public class GenericBasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        private  bool _IsActive = true;
        public GenericBasicAuthenticationFilter()
        {

        }
        public GenericBasicAuthenticationFilter(bool IsActive)
        {
            _IsActive = IsActive;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!_IsActive) return;

            var Identity = FetchAuthHeader(actionContext);
            if (Identity == null)
            {
                ChallengeAuthRequest(actionContext, _IsActive);
                return;
            }
            var genericPrincipal = new GenericPrincipal(Identity, null);
            Thread.CurrentPrincipal = genericPrincipal;

            if (!onAuthorizeUser(Identity.Name , Identity.Password , actionContext , out _IsActive) || !_IsActive)
            {
                ChallengeAuthRequest(actionContext, _IsActive);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        protected virtual BasicAuthenticationIdentity FetchAuthHeader(HttpActionContext actionContext)
        {
            string authHeaderValue = null;
            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && authRequest.Scheme == "Basic")
                authHeaderValue = authRequest.Parameter;

            if (string.IsNullOrEmpty(authHeaderValue))
                return null;

            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));

            var Credentials = authHeaderValue.Split(':');

            return Credentials.Length < 2 ? null : new BasicAuthenticationIdentity(Credentials[0], Credentials[1]);
        }

        private static void ChallengeAuthRequest(HttpActionContext actionContext , bool isActive)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;

            if (isActive)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, new ResponseObj() 
                {
                    Response = "unauthorized", 
                    ResponseCode = "401", 
                    ResponseMessage = "Success",
                    ResponseMessageAr = "فشل تسجيل الدخول"
                });

                actionContext.Response.Headers.Add("www-Authenticate", string.Format("Basic realm=\"{0}\"", host));
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, new ResponseObj()
                {
                    Response = "Inactive",
                    ResponseCode = "403",
                    ResponseMessage = "Success",
                    ResponseMessageAr = "فشل تسجيل الدخول"
                });
            }
            actionContext.Response.Headers.Add("www-Authenticate", string.Format("Basic realm=\"{0}\"", host));

        }
        protected virtual bool onAuthorizeUser(string UserName, string Password, HttpActionContext actionContext, out bool isActive)
        {
            isActive = false;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return false;
            }
            return true;
        }
    }
}