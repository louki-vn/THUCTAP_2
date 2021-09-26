using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace WebShop.Models
{
    public class Data
    {
        Shop db = new Shop();

        public static string user_logined
        {
            get
            {
                return (string)HttpContext.Current.Application["user_logined"];
            }
            set
            {
                HttpContext.Current.Application["user_logined"] = value;
            }
        }

        public static int is_logined
        {
            get
            {
                return (int)HttpContext.Current.Application["is_logined"];
            }
            set
            {
                HttpContext.Current.Application["is_logined"] = value;
            }
        }        

        public int Login(string username, string password)
        {
            object[] para =
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            var ret = db.MEMBERs.SqlQuery("CheckMember @username, @password", para).ToList();
            if (ret.Count > 0)
            {
                if (ret[0].role == 0)
                {
                    user_logined = ret[0].name;
                    is_logined = 1;
                    return 2;
                }
            }
            return 0;
        }

        public int Register(string name, string username, string password, string address, string phone)
        {
            object[] para =
            {
                new SqlParameter("@name", name),
                new SqlParameter("@username", username),
                new SqlParameter("@password", password),
                new SqlParameter("@address", address),
                new SqlParameter("@phone", phone)
            };
            db.Database.ExecuteSqlCommand("AddMember @name, @username, @password, @address, @phone", para);

            return 0;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();           

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public int GetItemInCart(List<ItemInCart> itemincartlist, string username)
        {          
            var user = new SqlParameter("@username", username);
            var result_member = db.Database.SqlQuery<MEMBER>("exec get_MEMBER_from_username @username", user).ToList();


            // Lấy cart của member đó và từ đó lấy ra những Item nằm trong cart đó.
            var cart_id_var = new SqlParameter("@cart_id", result_member[0].member_id);
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
                var p1 = new SqlParameter("@product_id", a.product_id);
                var result_product1 = db.Database.SqlQuery<PRODUCT>("exec get_PRODUCT_from_product_id @product_id", p1).ToList();
                productlist.Add(result_product1[0]);
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
            return 1;
        }
    }
}