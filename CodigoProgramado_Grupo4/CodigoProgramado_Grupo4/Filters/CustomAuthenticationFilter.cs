using CodigoProgramado_Grupo4.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace CodigoProgramado_Grupo4.Filters
{
    public class CustomAuthenticationFilter : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = (Usuario)filterContext.HttpContext.Session["User"];
            if (user == null || !user.isAuthenticated)
            {
                Debug.WriteLine("Usuario no autenticado o sesión nula.");
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult) 
            {
                filterContext.Result = new RedirectResult("~/Acount/Login");
            }

        }
    }
}