﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Sales.Controllers
{
    public class CartController : Controller
    {
        // GET: Sales/Cart
        Shop db = new Shop();
        // GET: Cart

        public List<ItemInCart> Get_Data(string username, List<ItemInCart> itemincartlist)
        {
            var user = new SqlParameter("@username", username);
            var result_member = db.Database.SqlQuery<MEMBER>("exec get_MEMBER_from_username @username", user).ToList();
            int cart_id = result_member[0].member_id;

            // Lấy cart của member đó và từ đó lấy ra những Item nằm trong cart đó.
            var cart_id_var = new SqlParameter("@cart_id", cart_id);
            var result_cart_item = db.Database.SqlQuery<CART_ITEM>("exec get_CART_ITEM_from_cart_id @cart_id", cart_id_var).ToList();

            // Thêm những item đó vào 1 list rồi gửi sang View
            List<CART_ITEM> cart_itemlist = new List<CART_ITEM>();
            for (int i = 0; i < result_cart_item.Count(); i++)
            {
                cart_itemlist.Add(result_cart_item[i]);
            }

            //          Tạo list product tương ứng với list cart_item
            List<PRODUCT> productlist = new List<PRODUCT>();
            foreach (var a in cart_itemlist)
            {
                var p = new SqlParameter("@product_id", a.product_id);
                var result_product = db.Database.SqlQuery<PRODUCT>("exec get_PRODUCT_from_product_id @product_id", p).ToList();
                productlist.Add(result_product[0]);
            }
            //          Tạo list ItemInCart để hiển thị trong cart     
            for (int i = 0; i < cart_itemlist.Count(); i++)
            {
                ItemInCart a = new ItemInCart();
                a.product_id = Int32.Parse(cart_itemlist[i].product_id.ToString());
                a.price = productlist[i].price;
                a.name = productlist[i].name;
                a.qty = Int32.Parse(cart_itemlist[i].qty.ToString());
                a.size = cart_itemlist[i].size;
                a.image_link = productlist[i].image_link;
                itemincartlist.Add(a);
            }
            ViewBag.ItemInCart = itemincartlist;
            ViewBag.Number = itemincartlist.Count();
            return itemincartlist;
        }
        public ActionResult Cart(string username)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            List<ItemInCart> itemincartlist = new List<ItemInCart>();
            Get_Data(HttpContext.Application["user_logined"].ToString(), itemincartlist);

            return View(itemincartlist);
        }

        public ActionResult Remove_Item(string product_id, string size)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            var product_id_var = new SqlParameter("@product_id", product_id);
            var size_var = new SqlParameter("@size", size);
            db.Database.ExecuteSqlCommand("exec remove_CART_ITEM_from_product_id_and_size @product_id, @size", product_id_var, size_var);

            var username = HttpContext.Application["user_logined"].ToString();
            List<ItemInCart> itemincartlist = new List<ItemInCart>();
            Get_Data(username, itemincartlist);
            return View("~/Areas/Sales/Views/Cart/Cart.cshtml", itemincartlist);
        }

        public ActionResult Remove_All_Item()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            var result = db.Database.ExecuteSqlCommand("exec remove_all_CART_ITEM");

            var username = HttpContext.Application["user_logined"].ToString();
            List<ItemInCart> itemincartlist = new List<ItemInCart>();
            Get_Data(username, itemincartlist);
            return View("~/Areas/Sales/Views/Cart/Cart.cshtml", itemincartlist);
        }

        public void CheckOut(FormCollection form)
        {

        }
    }
}