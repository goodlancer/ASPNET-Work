using Microsoft.AspNetCore.Mvc;

using ScolptioCRMWebApi.ApplicationContext;

using System;

namespace ScolptioCRMWebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        public SecurityContext SecurityContext
        {
            get
            {
                return (SecurityContext)Activator.CreateInstance(typeof(SecurityContext), HttpContext.User.Claims);
            }
        }

    }
}
