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
            var order = db.TRANSACTIONs.ToList();
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

            return View();
        }

    }
}