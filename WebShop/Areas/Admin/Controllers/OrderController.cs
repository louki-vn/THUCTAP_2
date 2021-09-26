using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        Shop db = new Shop();
        // GET: Admin/Order
        public ActionResult Index()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            var result = db.TRANSACTIONs.ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult EditOrder()
        {
            //  Chuyển trạng thái đơn hàng từ chờ xử lý -> đang xử lý
            var id = new SqlParameter("@id", TempData["Tran_ID"]);
            var status = new SqlParameter("@status", 1);
            db.Database.ExecuteSqlCommand("UpdateTransactionStatus @id, @status", id, status);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddReport(FormCollection fc)
        {
            //  Báo cáo hoàn thành đơn hàng đồng thời chuyển trạng thái đơn hàng thành Đã giao hàng
            var tran_id = new SqlParameter("@tran_id", System.Data.SqlDbType.Int) { Value = TempData["tran_id"] };
            var tran_id1 = new SqlParameter("@tran_id1", System.Data.SqlDbType.Int) { Value = TempData["tran_id"] };
            var employee_id = new SqlParameter("@employee_id", System.Data.SqlDbType.Int) { Value = fc["employee_id"] };
            var mem_id = new SqlParameter("@mem_id", System.Data.SqlDbType.Int) { Value = TempData["mem_id"] };
            var amount = new SqlParameter("@amount", System.Data.SqlDbType.Int) { Value = TempData["amount"] };
            var qty = new SqlParameter("@qty", System.Data.SqlDbType.Int) { Value = TempData["qty"] };
            var product_id = new SqlParameter("@product_id", System.Data.SqlDbType.Int) { Value = TempData["product_id"] };
            var date = new SqlParameter("@date", fc["date"]);
            var result = db.REPORTs.SqlQuery("CheckReport @tran_id1", tran_id1).ToList();
            if (result.Count != 0)
            {
                ViewBag.Message = "Giao dịch này đã được báo cáo!";
                var ret = db.TRANSACTIONs.ToList();
                return View("Index", ret);
            }
            
            db.Database.ExecuteSqlCommand("AddReport @tran_id, @employee_id, @mem_id,  @amount, @qty, @product_id, @date",
                                                    tran_id, employee_id, mem_id, amount, qty, product_id, date);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Filter(string filter)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            //  Lọc đơn hàng theo tình trạng đơn hàng
            var type = new SqlParameter("@type", filter);
            if(type.Value.ToString() == "3")
            {
                var result = db.TRANSACTIONs.ToList();
                return View("Index", result);
            }
            else
            {
                var result = db.TRANSACTIONs.SqlQuery("FilterTransaction @type", type).ToList();
                return View("Index", result);
            }           
        }
    }
}