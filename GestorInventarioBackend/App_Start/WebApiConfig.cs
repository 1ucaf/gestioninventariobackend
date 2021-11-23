using GestorInventarioBackend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GestorInventarioBackend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.EnableCors();

            config.MessageHandlers.Add(new TokenValidationHandler());

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
