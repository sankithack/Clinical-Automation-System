using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAS_DAL;
namespace Clinical_Automation_System.ViewModel
{
    public class UserDoctorModel
    {
        public Doctor doctors { get; set; }
        public User users { get; set; }
    }
}