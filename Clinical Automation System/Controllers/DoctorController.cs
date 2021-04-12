using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAS_DAL;
using CAS_DAL.Repositories;

namespace Clinical_Automation_System.Controllers
{
    public class DoctorController : Controller
    {
        AppointmentRepository appointmentRepository;
        MedicineRepository medicineRepository;
        MessageRepository messageRepository;
        UserRepository userRepository;


        public DoctorController()
        {
            appointmentRepository = new AppointmentRepository();
            medicineRepository = new MedicineRepository();
            messageRepository = new MessageRepository();
            userRepository = new UserRepository();
        }
        // GET: Doctor
        public ActionResult Index()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            return RedirectToAction("ViewAppointment");
        }


        [HttpGet]
        public ActionResult ViewMedicines()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Medicine> medicines = medicineRepository.GetMedicinesAllForPatientAndDoctor();
            return View(medicines);
        }

        [HttpPost]
        public ActionResult ViewMedicines(string textMediName)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Medicine> medicines = medicineRepository.GetMedicinesAllForPatientAndDoctorByName(textMediName);
            return View(medicines);
        }

        [HttpGet]
        public ActionResult ViewAppointment()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment> appointments = appointmentRepository.SearchbyDoctorId((int)Session["UserId"]);
            return View(appointments);
        }

        [HttpPost]
        public ActionResult ViewAppointment(DateTime txtAppDate)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            IEnumerable<Appointment> appointments = appointmentRepository.SearchbyDoctorAppDate(txtAppDate, (int)Session["UserId"]);
            return View(appointments);
        }

        [HttpGet]
        public ActionResult Message()
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            var appointments = appointmentRepository.SearchbyDoctorId((int)Session["UserId"]);
            ViewBag.Appointments = appointments;
            return View();
        }
        [HttpPost]
        public ActionResult Message(int id)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
            {
                return RedirectToRoute(new
                {
                    controller = "account",
                    action = "login",
                });
            }
            ViewBag.PatientId = id;
            ViewBag.DoctorName = userRepository.GetByIdName(id);
            IEnumerable<Message> messages = messageRepository.GetBySenderIdAndRecieverId((int)Session["UserId"], id);
            var appointments = appointmentRepository.SearchbyDoctorId((int)Session["UserId"]);
            ViewBag.Appointments = appointments;
            return View(messages);
        }

        [HttpPost]
        public ActionResult AddMessage(int id, string txtMessage)
        {
            if ((int)Session["userid"] == 0 || !((int)Session["RoleId"] == 2))
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