using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DAL.Repositories
{
    public class MedicineRepository : BaseRepository
    {

        public IEnumerable<Medicine> GetMedicinesAllForPatientAndDoctor()                 
        {
            IEnumerable<Medicine> result = myContext.Medicines
               .Where(meds => meds.IsActive == true)
               .OrderBy(meds => meds.Name);
            return result.ToList();
        }

        public IEnumerable<Medicine> GetMedicinesAllForPatientAndDoctorByName(string name)                  
        {
            IEnumerable<Medicine> result = myContext.Medicines
               .Where(meds => meds.Name.ToLower().Contains(name.ToLower()) && meds.IsActive == true)
               .OrderBy(meds => meds.Name);
            return result.ToList();
        }

        public IEnumerable<Medicine> GetMedicinesAll()                  //Get all medicines
        {
            IEnumerable<Medicine> result = myContext.Medicines
                .Where(meds=>meds.IsActive==true)
               .OrderBy(meds => meds.Name)
               .Select(meds => meds);
            return result.ToList();
        }

        public IEnumerable<Medicine> GetMedicines(string name)                  //Get all medicines
        {
            IEnumerable<Medicine> result = myContext.Medicines
               .Where(meds => meds.Name.ToLower().Contains(name.ToLower()) && meds.IsActive==true)
               .OrderBy(meds => meds.Name)
               .Select(meds => meds);
            return result.ToList();
        }

        public Medicine GetMedicinebyId(int id)                  //Get all medicines
        {
            Medicine result = myContext.Medicines
                .Where(meds => meds.MedicineId == id).FirstOrDefault();
            return result;
        }
        public int AddMedicine(Medicine meds)           //add new medicines
        {
            myContext.Medicines.Add(meds);
            int result = myContext.SaveChanges();
            if (result > 0)
                return 1;
            else
                return 0;
        }

        public bool UpdateMedicine(Medicine meds)
        {
            Medicine dbmed = GetMedicinebyId(meds.MedicineId);
            dbmed.Price = meds.Price;
            dbmed.Stock = meds.Stock;
            dbmed.IsAvailable = meds.IsAvailable;
            dbmed.Tax = meds.Tax;
            int result = myContext.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool DeleteMedicine(int id)
        {
            Medicine medicine = GetMedicinebyId(id);
            medicine.IsActive = false;
            int result = myContext.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
