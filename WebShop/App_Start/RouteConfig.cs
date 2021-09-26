﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebShop.Models;

namespace WebShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            Data.user_logined = "";
            Data.is_logined = 0;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Sales",
                "",
                new { area = "Sales", controller = "HomeSales", action = "Home" }
            );

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "Sales", controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}