using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team0_ManagementProperty.Models;

namespace Team0_ManagementProperty.Controllers
{
    public class PropertyController : Controller
    {
        private QUANLYBDS_TEAMEntities entities = new QUANLYBDS_TEAMEntities();
        // GET: Property
        public ActionResult Index()
        {
            List<Property> properties = entities.Properties.ToList();
            return View(properties);
        }
      
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Property entity)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu được gửi từ form có hợp lệ không
            {
                // Thêm entity vào context (chưa lưu vào database)
                entities.Properties.Add(entity);

                // Lưu thay đổi vào database
                entities.SaveChanges();

                // Chuyển hướng đến action Index của PropertyController
                return RedirectToAction("Index", "Property");
            }

            // Nếu ModelState không hợp lệ, quay lại view Add để hiển thị lỗi
            return View(entity);
             
        }
    }
}