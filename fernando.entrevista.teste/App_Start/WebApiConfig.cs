using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace fernando.entrevista.teste
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{filtro}",
                defaults: new { filtro = RouteParameter.Optional }
            );
        }
    }
}
