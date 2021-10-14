using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Sales.Controllers
{
    public class LoginController : Controller
    {
        Shop db = new Shop();
       
        // GET: Login
       
        public ActionResult Login()
        {
            HttpContext.Application["is_logined"] = 0;
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string username = fc.Get("customer[email]").ToString();
            string pass = fc.Get("customer[password]").ToString();
            var u = new SqlParameter("@username", username);
            var p = new SqlParameter("@password", Data.MD5Hash(pass));
            var result = db.Database.SqlQuery<MEMBER>("exec getMEMBERfromusernameandpass @username, @password", u, p).ToList();
            int check = result.Count();
            if (check != 0)
            {
                HttpContext.Application["user_logined"] = username;
                HttpContext.Application["is_logined"] = 1;
                             
                if (result[0].role == 0)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                }
                return RedirectToAction("Home", "HomeSales", new { area = "Sales" });
            }
            else
            {
                return View("~/Areas/Sales/Views/Login/Login.cshtml");
            }
        }
        public ActionResult Logout()
        {
            HttpContext.Application["is_logined"] = 0;
            return View("~/Areas/Sales/Views/Login/Login.cshtml");
        }
    }
}