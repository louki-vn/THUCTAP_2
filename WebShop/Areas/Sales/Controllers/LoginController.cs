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
        [HttpGet]
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
                ViewBag.user_logined = HttpContext.Application["user_logined"];
                ViewBag.is_logined = HttpContext.Application["is_logined"];

                //int doda = 2;
                //var id_var = new SqlParameter("@group_id", doda);
                //var result_product = db.Database.SqlQuery<PRODUCT>("exec get_product_from_PRODUCT_GROUP @group_id", id_var).ToList();
                //int qty = result_product.Count();
                //List<PRODUCT> product1list = new List<PRODUCT>();
                //List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();

                //for (int i = 0; i < qty; i++)
                //{
                //    product1list.Add(result_product[i]);
                //}
                //ViewBag.qty = qty;
                //Mix_PRODUCT_And_PRODUCT_Plus(product1list, productpluslist);

                //if (HttpContext.Application["is_logined"].ToString() == "1")
                //{
                //    Models.Data data = new Models.Data();
                //    List<ItemInCart> itemincartlist = new List<ItemInCart>();
                //    data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                //    ViewBag.ItemInCart = itemincartlist;
                //    ViewBag.Number = itemincartlist.Count();
                //}

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

        public void Mix_PRODUCT_And_PRODUCT_Plus(List<PRODUCT> productlist, List<PRODUCT_Plus> productpluslist)
        {
            var result_sale = db.Database.SqlQuery<SALE>("exec get_all_from_SALES").ToList();
            foreach (var a in productlist)
            {
                PRODUCT_Plus c = new PRODUCT_Plus();
                c.product_id = a.product_id;
                c.category_id = a.category_id;
                c.sale_id = a.sale_id;
                c.name = a.name;
                c.price = a.price;
                c.brand = a.brand;
                c.sold = a.sold;
                c.size = a.size;
                c.content = a.content;
                c.image_link = a.image_link;
                foreach (var b in result_sale)
                {
                    if (b.sale_id == a.sale_id)
                    {
                        c.sale_name = b.sale_name;
                        c.percent = b.percent;
                    }
                }
                productpluslist.Add(c);
            }
        }
    }
}