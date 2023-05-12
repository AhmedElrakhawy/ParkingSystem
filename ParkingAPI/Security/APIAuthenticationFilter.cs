using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace ParkingAPI.Security
{
    public class APIAuthenticationFilter : GenericBasicAuthenticationFilter
    {
        public APIAuthenticationFilter()
        {
        }
        public APIAuthenticationFilter(bool isActive) : base(isActive)
        {

        }
        protected override bool onAuthorizeUser(string UserName , string Password , HttpActionContext actionContext , out bool isActive)
        {
            IUserService service = new UserService();
            isActive = true;
            if (service != null)
            {
                var userId = service.Authenticate(UserName, Password);
                if (userId > 0)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;

                    if (basicAuthenticationIdentity != null)
                    {
                        basicAuthenticationIdentity.UserId = userId;
                        return true;
                    }
                    else if (userId == -1)
                    {
                        isActive = false;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}