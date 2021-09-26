using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        Shop db = new Shop();
        // GET: Admin/Category
        public ActionResult Index()
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];
            var result = db.CATEGORies.ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult AddCategory(FormCollection fc)
        {
            var name = new SqlParameter("@name", fc["category"]);
            var group_id = new SqlParameter("@group_id", fc["group_id"]);
            db.Database.ExecuteSqlCommand("AddCategory @name, @group_id", name, group_id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditCategory(FormCollection fc)
        {
            var id = new SqlParameter("@id", TempData["category_id"]);
            var name = new SqlParameter("@name", fc["name"]);
            var group_id = new SqlParameter("@group_id", fc["group_id"]);

            db.Database.ExecuteSqlCommand("EditCategory @id, @name, @group_id", id, name, group_id); 
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCategory()
        {
            var id = new SqlParameter("@id", TempData["category_id"]);
            db.Database.ExecuteSqlCommand("DeleteCategory @id", id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Filter(string filter)
        {
            ViewBag.user_logined = HttpContext.Application["user_logined"];
            ViewBag.is_logined = HttpContext.Application["is_logined"];

            //  Lọc danh mục theo nhóm sản phẩm
            var type = new SqlParameter("@type", filter);
            if (type.Value.ToString() == "3")
            {                
                var result = db.CATEGORies.ToList();
                return View("Index", result);
            }
            else
            {               
                var result = db.CATEGORies.SqlQuery("FilterCategory @type", type).ToList();
                return View("Index", result);
            }
        }
    }
}