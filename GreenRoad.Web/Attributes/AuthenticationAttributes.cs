using GreenRoad.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenRoad.Web.Attributes
{
    //public class AuthenticationAttribute : AuthorizationFilterAttribute
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        private readonly TokenHelper _helper;

        public AuthenticationAttribute()
        {
            _helper = new TokenHelper();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (_helper.AccessToken == null)
            {
                filterContext.HttpContext.Response.RedirectToRoute("Login");
            }
        }
    }
}