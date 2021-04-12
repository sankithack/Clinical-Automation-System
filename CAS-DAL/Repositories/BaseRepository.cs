using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DAL.Repositories
{
   public class BaseRepository
    {
        protected ClinicalDBEntities myContext;
        public BaseRepository()
        {
            myContext = new ClinicalDBEntities();
        }
    }
}
