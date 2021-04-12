using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using CAS_DAL;
using CAS_DAL.Repositories;

namespace Clinical_Automation_System.Controllers
{
    public class FontofficeController : Controller
    {
        DoctorRepository doctorRepository;
        AppointmentRepository appointmentRepository;
        UserRepository userRepository;

        public FontofficeController()
        {
            doctorRepository = new DoctorRepository();
            appointmentRepository = new AppointmentRepository();
            userRepository = new UserRepository();
        }
        // GET: Fontoffice
        public ActionResult Index()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            return RedirectToRoute(new
            {
                controller = "Fontoffice",
                action = "addpatient",
            });
        }

        [HttpGet]
        public ActionResult ViewAppointment()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment> appointments = appointmentRepository.GetAll();
            return View(appointments);
        }
        [HttpPost]
        public ActionResult ViewAppointment(DateTime txtappdate)
        {

            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment> appointments = appointmentRepository.SearchbyAppDate(txtappdate);
            return View(appointments);
        }


        [HttpPost]
        public ActionResult UpdateAppointment(int id,string txtDetails)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }

            Appointment appointment = new Appointment();
            appointment = appointmentRepository.GetById(id);
            appointment.Status = "Approved";
            appointment.Details = txtDetails;
            appointment.IsApprove = true;
            bool res = appointmentRepository.Update(appointment);
            return RedirectToAction("ViewAppointment");
        }

        [HttpGet]
        public ActionResult ViewAppointmentByPatient()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment> appointments = appointmentRepository.GetAll();
            return View(appointments);
        }

        [HttpPost]
        public ActionResult ViewAppointmentByPatient(string txtName)
        {

            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment> appointments = appointmentRepository.Search(txtName);
            return View(appointments);
        }

        [HttpGet]
        public ActionResult BookAppointment()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            ViewBag.Patient = userRepository.GetAllPateint().ToList();
            ViewBag.Doctor = userRepository.GetAllDoctor().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult BookAppointment(Appointment appointment)
        {

            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            int res = appointmentRepository.Add(appointment);
            return RedirectToAction("ViewAppointment");
        }

        [HttpGet]
        public ActionResult AddPatient()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
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
        public ActionResult AddPatient(User user)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 4))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            int res = userRepository.Add(user);
            return RedirectToAction("BookAppointment");
        }

    }
}