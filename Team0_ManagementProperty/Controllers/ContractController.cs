using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team0_ManagementProperty.Models;

namespace Team0_ManagementProperty.Controllers
{
    public class ContractController : Controller
    {
        private QUANLYBDS_TEAMEntities entities = new QUANLYBDS_TEAMEntities();
        // GET: Contract
        public ActionResult Index()
        {
            List<Full_Contract> contracts = entities.Full_Contract.ToList();
            return View(contracts);
        }
        public ActionResult AddContract() {
            return View();
        }
        [HttpPost]
        public ActionResult AddContract(Full_Contract contract)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra logic hợp lệ ở đây

                // Thêm contract vào database
                entities.Full_Contract.Add(contract);
                entities.SaveChanges();

                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, quay lại view với dữ liệu đã nhập và thông báo lỗi
            return View(contract);

        }
    }
}