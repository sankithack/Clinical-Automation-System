using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAS_DAL;

namespace CAS_DAL.Repositories
{
    public class MessageRepository : BaseRepository
    {
        public int Add(Message item)
        {
            myContext.Messages.Add(item);
            int res = myContext.SaveChanges();
            if (res > 0)
                return 1;
            else
            return 0;
        }

        public IEnumerable<Message> GetById(int id)
        {
            IEnumerable<Message> messages = myContext.Messages.Include("User").
                OrderByDescending(mess => mess.MessageTime).
                Where(mess => mess.RecieverId == id || mess.SenderId == id);
            return messages;
        }

        public IEnumerable<Message> GetBySenderIdAndRecieverId(int SenderId, int RecieverId)
        {
            IEnumerable<Message> messages = myContext.Messages.Include("User").
                OrderBy(mess => mess.MessageTime).
                Where(mess => (mess.RecieverId == RecieverId && mess.SenderId==SenderId)|| (mess.RecieverId == SenderId && mess.SenderId == RecieverId));
            return messages;
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> Search(string name)
        {
            throw new NotImplementedException();
        }

        public bool Update(Message item)
        {
            throw new NotImplementedException();
        }
    }
}
