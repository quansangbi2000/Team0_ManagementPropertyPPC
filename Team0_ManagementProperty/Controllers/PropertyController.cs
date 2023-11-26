using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq;

using Team0_ManagementProperty.Models;
using System.Data.Entity.Infrastructure;

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
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Property entity)
        {
            try
            {
                List<Property_Type> propertyTypes = entities.Property_Type.ToList();
                List<District> districts = entities.Districts.ToList();

                // Lấy danh sách loại bất động sản từ cơ sở dữ liệu
                List<SelectListItem> propertyTypeItems = propertyTypes
                    .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Property_Type_Name })
                    .ToList();

                List<District> district = entities.Districts.ToList();
                List<SelectListItem> districtItems = districts
                    .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.District_Name })
                    .ToList();

                // Gán danh sách quận vào ViewBag
                ViewBag.Districts = districtItems;

                // Gán danh sách loại bất động sản vào ViewBag
                ViewBag.PropertyTypes = propertyTypeItems;

                // Thêm entity vào context (chưa lưu vào database)
                entities.Properties.Add(entity);

                // Lưu thay đổi vào database
                entities.SaveChanges();

                // Chuyển hướng đến action Index của PropertyController
                return RedirectToAction("Index", "Property");

            } catch (Exception ex)
            {
                return View(entity);
            }
            
            }
    
    }
}