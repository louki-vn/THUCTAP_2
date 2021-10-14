using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Sales.Controllers
{
    public class CreateAccountController : Controller
    {
        Shop db = new Shop();
        // GET: CreateAccount
        
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(FormCollection form)
        {
            string username = form.Get("customer[username]").ToString();
            string pass = form.Get("customer[password]").ToString();
            string address = form.Get("customer[address]").ToString();
            string phone = form.Get("customer[phone]").ToString();
            string name = form.Get("customer[name]").ToString();
            var u = new SqlParameter("@username", username);

            var result = db.Database.SqlQuery<MEMBER>("exec get_MEMBER_from_username @username", u).ToList();
            int check = result.Count();
            if (check != 0)
            {
                return View();
            }
            else
            {
                var username2var = new SqlParameter("@username", username);
                var passvar = new SqlParameter("@password", Data.MD5Hash(pass));
                var namevar = new SqlParameter("@name", name);
                var phonevar = new SqlParameter("@phone", phone);
                var addressvar = new SqlParameter("@address", address);
                db.Database.ExecuteSqlCommand("exec createaccount @username, @name, @password, @phone, @address",
                                                                            username2var, namevar, passvar, phonevar, addressvar);
                return RedirectToAction("Home", "HomeSales", new { area = "Sales" });
            }
        }
    }
}