using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAS_DAL;
using CAS_DAL.Repositories;
using Clinical_Automation_System.ViewModel;

namespace Clinical_Automation_System.Controllers
{
    public class AdminController : Controller
    {
        DoctorRepository doctorRepo;
        UserRepository userRepo;
        public AdminController()
        {
            doctorRepo = new DoctorRepository();
            userRepo = new UserRepository();
        }
        [HttpGet]
        public ActionResult Index()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 1))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<User> usr = userRepo.GetAll();
            return View(usr);
        }
        [HttpPost]
        public ActionResult Index(string textUserName)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 1))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<User>  usr = userRepo.GetByNameAll(textUserName);
            return View(usr);
        }

        [HttpGet]
        public ActionResult AddDoctor()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 1))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            ViewBag.Specialization = doctorRepo.GetAllSpecialization().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddDoctor(UserDoctorModel ud)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 1))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            ud.users.RoleId = 2;
            ud.users.IsActive = true;
            int newid = userRepo.Add(ud.users);
            ud.doctors.UserId = newid;
            int newid2 = doctorRepo.Add(ud.doctors);
            if (newid > 0 && newid2 > 0)
                return RedirectToAction("Success");
            else
                return View();
        }

        public ActionResult Success()
        {
            return View();
        }

    }
}