using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinical_Automation_System.ViewModel
{
    public class MedicineViewModel
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
        public float Tax { get; set; }
    }
}