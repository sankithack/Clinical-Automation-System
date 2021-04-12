using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAS_DAL;
using CAS_DAL.Repositories;

namespace Clinical_Automation_System.Controllers
{

    public class PharmacyController : Controller
    {
 
        MedicineRepository medicineRepo;

        public PharmacyController()
        {
            medicineRepo = new MedicineRepository();
        }
        // GET: Pharmacy
        [HttpGet]
        public ActionResult Index()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 5))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Medicine> meds = medicineRepo.GetMedicinesAll();
            return View(meds);
        }
        [HttpPost]
        public ActionResult Index(string textMediName)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 5))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Medicine> meds = medicineRepo.GetMedicines(textMediName);
            return View(meds);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 5))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Medicine medicine)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 5))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            medicine.IsActive = true;
            int newid = medicineRepo.AddMedicine(medicine);
            if (newid > 0)
                return RedirectToAction("Index");
            else
                return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 5))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            Medicine m = medicineRepo.GetMedicinebyId(id);
            return View(m);
        }
        [HttpPost]
        public ActionResult Edit(Medicine medicine)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 5))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            bool res = medicineRepo.UpdateMedicine(medicine);
            Medicine m = medicineRepo.GetMedicinebyId(medicine.MedicineId);
            return View(m);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 5))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            bool res = medicineRepo.DeleteMedicine(id);           
            return RedirectToAction("Index");
        }

    }
}