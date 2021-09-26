using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Sales.Controllers
{
    public class UserInformationController : Controller
    {
        Shop db = new Shop();
        // GET: UserInfomation
        public ActionResult UserInformation()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            var user_var = new SqlParameter("@username", ViewBag.user_logined);
            var result = db.Database.SqlQuery<MEMBER>("exec get_MEMBER_from_username @username", user_var).ToList();
            MEMBER user = new MEMBER();
            user = result[0];

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            return View(user);
        }

        public ActionResult update_information(string member_id, string name, string phone, string address)
        {
            var member_id_var = new SqlParameter("@member_id", member_id);
            var name_var = new SqlParameter("@name", name);
            var phone_number_var = new SqlParameter("@phone_number", phone);
            var address_var = new SqlParameter("@address", address);
            var result = db.Database.ExecuteSqlCommand("exec update_MEMBER_information @member_id, @name, @phone_number, @address", member_id_var, name_var, phone_number_var, address_var);

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            return Content("1");
        }
    }
}