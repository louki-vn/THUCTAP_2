using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    public class UserDataController : Controller
    {
        Shop db = new Shop();
        // GET: Admin/UserData
        public ActionResult Index()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            var result = db.MEMBERs.ToList();
            return View(result);
        }
        
        public ActionResult UserInfor()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            return View();
        }

        [HttpPost]
        public ActionResult DeleteMember()
        {
            var id = new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = TempData["delete_id"] };
            db.Database.ExecuteSqlCommand("DeleteMember @id", id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateMember(FormCollection fc)
        {
            var id = new SqlParameter("@id", fc["id"]);
            var password = new SqlParameter("@password", fc["password"]);
            var phone = new SqlParameter("@phone", fc["phone"]);
            var address = new SqlParameter("@address", fc["address"]);

            db.Database.ExecuteSqlCommand("UpdateMember @id, @password, @phone, @address", id, password, phone, address);
            return View("UserInfor");
        }

        [HttpPost]
        public ActionResult Filter(string filter)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            //  Lọc danh sách thành viên theo vai trò
            var type = new SqlParameter("@type", filter);
            if (type.Value.ToString() == "3")
            {
                var result = db.MEMBERs.ToList();
                return View("Index", result);
            }
            else
            {
                var result = db.MEMBERs.SqlQuery("FilterMember @type", type).ToList();
                return View("Index", result);
            }
        }
    }
}