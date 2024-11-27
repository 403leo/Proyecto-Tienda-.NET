using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CodigoProgramado_Grupo4.Filters
{
    public class ResultLoggingFilter : FilterAttribute, IResultFilter
    {

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Debug.WriteLine("Rseultado enviado al cliente: " + filterContext.RouteData.Values["action"]);
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Debug.WriteLine("Resultado en proceso de envio al cliente: " + filterContext.RouteData.Values["action"]);
        }
    }
}