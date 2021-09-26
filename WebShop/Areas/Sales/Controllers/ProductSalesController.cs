using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Sales.Controllers
{
    public class ProductSalesController : Controller
    {
        // GET: Sales/Product
        Shop db = new Shop();
        // GET: Product
        public ActionResult Product()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();

            var result_product = db.Database.SqlQuery<PRODUCT>("exec selectallfromPRODUCT").ToList();
            int qty = result_product.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result_product[i]);
            }

            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);
            ViewBag.qty = qty;

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }            

            return View(productpluslist);
        }

        public ActionResult Product_Detail(string product_id)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();

            var result_product = db.Database.SqlQuery<PRODUCT>("exec selectallfromPRODUCT").ToList();
            int qty = result_product.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result_product[i]);
            }
            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);
            ViewBag.qty = qty;
            ViewBag.product_id = product_id;

            if(HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }            

            return View("~/Areas/Sales/Views/ProductSales/Product_Detail.cshtml", productpluslist);
        }

        public ActionResult Add_To_Cart1(string product_id)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            List<PRODUCT> productlist = new List<PRODUCT>();
            string user = ViewBag.user_logined;
            string size = "S";
            int item_qty = 1;
         
            Add_To_Cart(user, Int32.Parse(product_id), item_qty, size, productlist);
            return View("~/Areas/Sales/Views/ProductSales/Product.cshtml", productlist);
        }

        [HttpPost]
        public ActionResult Add_To_Cart2(FormCollection form, string product_id)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();
            var result = db.Database.SqlQuery<PRODUCT>("exec selectallfromPRODUCT").ToList();
            int qty = result.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result[i]);
            }
            ViewBag.qty = qty;
            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            if (ViewBag.is_logined == 1)
            {
                string user = ViewBag.user_logined;
                string size = form.Get("option-1");
                if (size == null)
                {
                    size = "S";
                }
                int item_qty = Int32.Parse(form.Get("quantity").ToString());
                Add_To_Cart(user, Int32.Parse(product_id), item_qty, size, productlist);
            }
            else
            {

            }
            ViewBag.product_id = product_id;
            return View("~/Areas/Sales/Views/ProductSales/Product_Detail.cshtml", productpluslist);
        }

        public ActionResult Get_Product_Base_On_Product_Group(string group_id)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            int id_int = Int32.Parse(group_id);
            var id_var = new SqlParameter("@group_id", id_int);

            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();

            var result = db.Database.SqlQuery<PRODUCT>("exec get_product_from_PRODUCT_GROUP @group_id", id_var).ToList();
            int qty = result.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result[i]);
            }

            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);
            ViewBag.qty = qty;

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            return View("~/Areas/Sales/Views/ProductSales/Product.cshtml", productpluslist);
        }
        public ActionResult Get_Product_Base_On_Price(int along)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            var along_var = new SqlParameter("@along", along);

            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();

            var result = db.Database.SqlQuery<PRODUCT>("exec get_product_base_on_price @along", along_var).ToList();

            int qty = result.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result[i]);
            }

            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);
            ViewBag.qty = qty;

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            return View("~/Areas/Sales/Views/ProductSales/Product.cshtml", productpluslist);
        }

        public ActionResult Get_Product_Base_On_Brand(string brand)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            var brand_var = new SqlParameter("@brand", brand);
            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();

            var result = db.Database.SqlQuery<PRODUCT>("exec get_product_base_on_brand @brand", brand_var).ToList();


            int qty = result.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result[i]);
            }

            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);
            ViewBag.qty = qty;

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            return View("~/Areas/Sales/Views/ProductSales/Product.cshtml", productpluslist);
        }

        public ActionResult Get_Product_Base_On_Category(string name)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            var name_var = new SqlParameter("@name", name);

            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();

            var result = db.Database.SqlQuery<PRODUCT>("exec get_product_from_CATEGORY @name", name_var).ToList();

            int qty = result.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result[i]);
            }

            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);
            ViewBag.qty = qty;

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            return View("~/Areas/Sales/Views/ProductSales/Product.cshtml", productpluslist);
        }

        public void Add_To_Cart(string user, int product_id, int item_qty, string size, List<PRODUCT> productlist)
        {
            //          Lấy tất cả sản phẩm cho vào 1 list, để sau khi thêm product vào item, quay lại view vẫn hiển thị được full product
            var result = db.Database.SqlQuery<PRODUCT>("exec selectallfromPRODUCT").ToList();
            int qty = result.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result[i]);
            }
            ViewBag.qty = qty;
            ViewBag.product_id = product_id;

            //          Lấy thông tin member đang đăng nhập và product đã được chọn (để tạo item)
            var product_id_var = new SqlParameter("@product_id", product_id);
            var username = new SqlParameter("@username", System.Data.SqlDbType.NVarChar) { Value = user};
            var result_product = db.Database.SqlQuery<PRODUCT>("exec get_PRODUCT_from_product_id @product_id", product_id_var).ToList();
            var result_member = db.Database.SqlQuery<MEMBER>("exec get_MEMBER_from_username @username", username).ToList();
            string price = result_product[0].price.ToString();
            string cart_id = result_member[0].member_id.ToString();
         
            //          Kiểm tra xem đã tồn tại sản phẩm đó trong giỏ hàng chưa
            var product_id_var3 = new SqlParameter("@product_id", product_id);
            var cart_id_var2 = new SqlParameter("@cart_id", cart_id);
            var size_var1 = new SqlParameter("@size", size);
            var result_check = db.Database.SqlQuery<CART_ITEM>("exec CheckProductInCart @cart_id, @product_id, @size", cart_id_var2, product_id_var3, size_var1).ToList();
            if (result_check.Count() == 1)
            {
                var product_id_var2 = new SqlParameter("@product_id", product_id);          
                var cart_id_var = new SqlParameter("@cart_id", cart_id);    
                var size_var = new SqlParameter("@size", size);
                db.Database.ExecuteSqlCommand("UpdateNumberProductInCartItem @cart_id, @product_id, @size", cart_id_var, product_id_var2, size_var);
            }
            else
            {
                //          Tạo ITEM bằng thông tin của member(lấy cart_id) và product đó
                var product_id_var2 = new SqlParameter("@product_id", product_id);
                var price_var = new SqlParameter("@price", price);
                var cart_id_var = new SqlParameter("@cart_id", cart_id);
                var qty_var = new SqlParameter("@qty", item_qty);
                var size_var = new SqlParameter("@size", size);
                db.Database.ExecuteSqlCommand("exec create_CART_ITEM @cart_id, @product_id, @qty, @price, @size", 
                                                            cart_id_var, product_id_var2, qty_var, price_var, size_var);
            }
        }

        [HttpGet]
        public ActionResult Add_To_CartAjax(string id)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            List<PRODUCT> productlist = new List<PRODUCT>();
            string user = ViewBag.user_logined;
            string size = "S";
            int item_qty = 1;            

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Add_To_Cart(user, Int32.Parse(id), item_qty, size, productlist);
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }
            return View("~/Areas/Sales/Views/ProductSales/Product.cshtml", productlist);
        }

        public ActionResult Search(string key_word)
        {
            var key_word_var = new SqlParameter("@key_word", key_word);
            List<PRODUCT> productlist = new List<PRODUCT>();
            List<PRODUCT_Plus> productpluslist = new List<PRODUCT_Plus>();
            var result = db.Database.SqlQuery<PRODUCT>("exec get_PRODUCT_from_key_word @key_word", key_word_var).ToList();

            int qty = result.Count();
            for (int i = 0; i < qty; i++)
            {
                productlist.Add(result[i]);
            }

            Mix_PRODUCT_And_PRODUCT_Plus(productlist, productpluslist);
            ViewBag.qty = qty;

            if (HttpContext.Application["is_logined"].ToString() == "1")
            {
                Models.Data data = new Models.Data();
                List<ItemInCart> itemincartlist = new List<ItemInCart>();
                data.GetItemInCart(itemincartlist, HttpContext.Application["user_logined"].ToString());
                ViewBag.ItemInCart = itemincartlist;
                ViewBag.Number = itemincartlist.Count();
            }

            return View("~/Areas/Sales/Views/ProductSales/Product.cshtml", productpluslist);
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