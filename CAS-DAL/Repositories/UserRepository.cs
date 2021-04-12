using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS_DAL.Repositories
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        public int Add(User item)
        {
            myContext.Users.Add(item);
            int result = myContext.SaveChanges();
            if (result > 0)
                return item.UserId;
            else
                return 0;
        }


        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> result = myContext.Users.Include("Role")
                .Where(usr => usr.IsActive == true)
                .OrderBy(usr => usr.Name)
                .Select(usr => usr);
            return result;
        }


        public IEnumerable<User> GetAllPateint()
        {
            IEnumerable<User> result = myContext.Users.Include("Role")
                .Where(usr => usr.IsActive == true && usr.RoleId==3)
                .OrderBy(usr => usr.Name)
                .Select(usr => usr);
            return result;
        }


        public IEnumerable<User> GetAllDoctor()
        {
            IEnumerable<User> result = myContext.Users.Include("Role")
                .Where(usr => usr.IsActive == true && usr.RoleId == 2)
                .OrderBy(usr => usr.Name)
                .Select(usr => usr);
            return result;
        }

        public User GetById(int id)
        {
            User result = myContext.Users.Include("Role")
                .Where(usr => usr.IsActive == true && usr.UserId==id)
                .OrderBy(usr => usr.Name)
                .Select(usr => usr).FirstOrDefault();
            return result;
        }
        public User GetByName(string name)
        {
            User result = myContext.Users.Include("Role")
                .Where(usr => usr.IsActive == true)
                .OrderBy(usr => usr.Name)
                .Select(usr => usr).FirstOrDefault();
            return result;
        }
        public IEnumerable<User> GetByNameAll(string name)
        {
            IEnumerable<User> result = myContext.Users.Include("Role")
                .Where(usr => usr.IsActive == true && usr.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(usr => usr.Name);
            return result;
        }
        public string GetByIdName(int id)
        {
            string name = myContext.Users.Where(u => u.UserId == id).Select(u => u.Name).FirstOrDefault();
            return name;
        }

        public bool Remove(int id)
        {
            User dbuser = GetById(id);
            myContext.Users.Remove(dbuser);
            int result = myContext.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<User> Search(string name)
        {
            IEnumerable<User> result = myContext.Users.Include("Role")
                .Where(usr => usr.Name.Equals(name))
                .OrderBy(usr => usr.Name)
                .Select(usr => usr);
            return result;
        }

        public bool Update(User item)
        {
            User dbuser = GetById(item.UserId);
            dbuser.Phone = item.Phone;
            dbuser.Email = item.Email;
            int result = myContext.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public User LoginUsingEmailAndPassword(string email,string password)
        {
            User user = myContext.Users
                .Include("Role")
                .OrderBy(u => u.UserId).
                Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            return user;
        }
    }
}
