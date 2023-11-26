using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            LoadDropdowns(); // Tải danh sách loại bất động sản và quận
            return View(new Property());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Property entity)
        {
                try
                {
                entities.Properties.Add(entity);

                    // Save changes to the database
                    entities.SaveChanges();
                    return RedirectToAction("Index", "Property");
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    Console.WriteLine($"An error occurred: {ex.Message}");

                    // Rollback the transaction on error
                    

                    // Add an error message to ModelState
                    ModelState.AddModelError("", "An error occurred while processing your request.");

                    // Reload dropdowns and return the view with errors
                    LoadDropdowns();
                    return View(entity);
                }
            }
        


        private void LoadDropdowns()
        {
            List<Property_Type> propertyTypes = entities.Property_Type.ToList();
            List<SelectListItem> propertyTypeItems = propertyTypes
                .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Property_Type_Name })
                .ToList();

            List<District> districts = entities.Districts.ToList();
            List<SelectListItem> districtItems = districts
                .Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.District_Name })
                .ToList();
            var statusList = entities.Property_Status
     .Select(s => new SelectListItem
     {
         Value = s.ID.ToString(),
         Text = s.Property_Status_Name
     })
     .ToList();

            ViewBag.Property_Status = statusList;
            ViewBag.Districts = districtItems;
            ViewBag.PropertyTypes = propertyTypeItems;
        }
    }
}
