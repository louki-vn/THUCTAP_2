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

        [HttpGet]
        public ActionResult EditOrder(string tran_id)
        {
            //  Chuyển trạng thái đơn hàng từ chờ xử lý -> đang xử lý
            var id = new SqlParameter("@id", tran_id);
            var status = new SqlParameter("@status", 1);
            db.Database.ExecuteSqlCommand("UpdateTransactionStatus @id, @status", id, status);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddReport(string  tran_id, string mem_id, string product_id, string amount, string qty, string employee_id, string date)
        {
            //  Báo cáo hoàn thành đơn hàng đồng thời chuyển trạng thái đơn hàng thành Đã giao hàng
            var tran_id_var = new SqlParameter("@tran_id", tran_id);
            var tran_id1 = new SqlParameter("@tran_id1", tran_id);
            var employee_id_var = new SqlParameter("@employee_id", employee_id);
            var mem_id_var = new SqlParameter("@mem_id", mem_id);
            var amount_var = new SqlParameter("@amount", amount);
            var qty_var = new SqlParameter("@qty", qty);
            var product_id_var = new SqlParameter("@product_id", product_id);
            var date_var = new SqlParameter("@date", date);
            var result = db.REPORTs.SqlQuery("CheckReport @tran_id1", tran_id1).ToList();
            if (result.Count != 0)
            {
                ViewBag.Message = "Giao dịch này đã được báo cáo!";                
                return RedirectToAction("Index");
            }
            
            db.Database.ExecuteSqlCommand("AddReport @tran_id, @employee_id, @mem_id,  @amount, @qty, @product_id, @date",
                                                    tran_id_var, employee_id_var, mem_id_var, amount_var, qty_var, product_id_var, date_var);
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