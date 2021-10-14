using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        Shop db = new Shop();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            var mem = db.MEMBERs.ToList();
            var order = db.TRANSACTIONs.SqlQuery("exec SelectCompleteOrder").ToList();
            //  Số lượng thành viên
            ViewBag.Member_count = mem.Count();
            //  Tổng số đơn hàng
            ViewBag.Order_count = order.Count();
            int amount = 0, total = 0;
            foreach(var item in order)
            {
                amount += item.amount;
                total += item.qty;
            }
            //  Số sản phẩm đã bán và tổng doanh thu
            ViewBag.Amount = amount;
            ViewBag.Total = total;

            var topproduct = db.PRODUCTs.SqlQuery("exec SelectTopProduct").ToList();
            ViewBag.TopProduct = topproduct;

            var category = db.CATEGORies.ToArray();    
            Dictionary<int, string> p = new Dictionary<int, string>();
            foreach (var item in category)
            {
                p.Add(item.category_id, item.name);
            }            
            ViewBag.Category = p;

            var topmem = db.Database.SqlQuery<Mem_Cart>("exec SelectTopMember").ToList();
            ViewBag.TopMem = topmem;

            return View();
        }
    }
}