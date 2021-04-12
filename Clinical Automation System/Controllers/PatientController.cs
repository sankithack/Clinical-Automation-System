using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAS_DAL;
using CAS_DAL.Repositories;

namespace Clinical_Automation_System.Controllers
{
    public class PatientController : Controller
    {
        DoctorRepository doctorRepository;
        AppointmentRepository appointmentRepository;
        MedicineRepository medicineRepository;
        MessageRepository messageRepository;
        UserRepository userRepository;

        public PatientController()
        {
            doctorRepository = new DoctorRepository();
            appointmentRepository = new AppointmentRepository();
            medicineRepository = new MedicineRepository();
            messageRepository = new MessageRepository();
            userRepository = new UserRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }

            IEnumerable<Doctor> doctorVM=doctorRepository.GetAll();
            return View(doctorVM);
        }

        [HttpPost]
        public ActionResult Index(string txtspname)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Doctor> doctorVM = doctorRepository.Search(txtspname.ToLower());
            return View(doctorVM);
        }

        [HttpGet]
        public ActionResult ViewAppointment()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment> appointments=appointmentRepository.SearchbyPateintId((int)Session["UserId"]);
            return View(appointments);
        }

        [HttpPost]
        public ActionResult ViewAppointment(DateTime txtappdate)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment>appointments= appointmentRepository.SearchbyPateintAppDate(txtappdate, (int)Session["UserId"]);
            return View(appointments);
        }

        [HttpPost]
        public ActionResult AddAppointment(int id)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            Appointment appointment = new Appointment();
            appointment.DoctorId = id;
            appointment.IsApprove = false;
            appointment.StartDateTime = DateTime.Now.AddDays(7);
            appointment.PatientId = (int)Session["UserId"];
            appointment.Status = "Queue";
            appointment.Details = "Will be Updated by FontOffice";
            int result = appointmentRepository.Add(appointment);
            if (result == 1)
            {
                return RedirectToAction("ViewAppointment");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ViewMedicines()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Medicine> medicines=medicineRepository.GetMedicinesAllForPatientAndDoctor();
            return View(medicines);
        }

        [HttpPost]
        public ActionResult ViewMedicines(string textMediName)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Medicine> medicines=medicineRepository.GetMedicinesAllForPatientAndDoctorByName(textMediName);
            return View(medicines);
        }

        [HttpGet]
        public ActionResult Message()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            var appointments = appointmentRepository.SearchbyPateintIdApproved((int)Session["UserId"]);              
            ViewBag.Appointments = appointments;
            return View();
        }
        [HttpPost]
        public ActionResult Message(int id)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            ViewBag.DoctorId = id;
            ViewBag.DoctorName = userRepository.GetByIdName(id);
            IEnumerable<Message> messages = messageRepository.GetBySenderIdAndRecieverId((int)Session["UserId"],id);
            var appointments = appointmentRepository.SearchbyPateintIdApproved((int)Session["UserId"]);
            ViewBag.Appointments = appointments;
            return View(messages);
        }

        [HttpPost]
        public ActionResult AddMessage(int id,string txtMessage)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 3))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            Message message = new Message();
            message.SenderId = (int)Session["UserId"];
            message.MessageTime = DateTime.Now;
            message.RecieverId = id;
            message.Status = "Sent";
            message.Message1 = txtMessage;
            int res = messageRepository.Add(message);
            return RedirectToAction("Message");
        }


    }
}