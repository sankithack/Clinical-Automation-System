using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DAL.Repositories
{
    public class DoctorRepository : BaseRepository, IRepository<Doctor>
    {
        public int Add(Doctor item)
        {
            myContext.Doctors.Add(item);
            int result = myContext.SaveChanges();
            if (result > 0)
                return 1;
            else
                return 0;
        }
        public IEnumerable<Specialization> GetAllSpecialization()
        {
            return myContext.Specializations.OrderBy(sp => sp.SpecializationId);

        }
        public IEnumerable<Doctor> GetAll()
        {
            IEnumerable<Doctor> doctors = myContext.Doctors.Include("Specialization")
                .Include("User").OrderBy(dr => dr.DoctorId).Where(dr=>dr.IsAavailable== true && dr.User.IsActive==true);
            return doctors;
        }

        public Doctor GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> Search(string name)
        {
            IEnumerable<Doctor> doctors = myContext.Doctors.Include("Specialization")
               .Include("User").Where(dr=>dr.Specialization.SpecializationName.ToLower().Contains(name.ToLower())).OrderBy(dr => dr.DoctorId).
               Where(dr => dr.IsAavailable == true && dr.User.IsActive==true);
            return doctors;
        }

        public bool Update(Doctor item)
        {
            throw new NotImplementedException();
        }
    }
}
