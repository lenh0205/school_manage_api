using DataInfastructure.IResponsitory;
using DataInfastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataInfastructure.Responsitory
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolDBContext _context;
        public UserRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public User Add(User c)
        {

            _context.Users.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.Users.Remove(item);
            return true;
        }

        public User GetById(Guid id)
        {
            var c = _context.Users.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<User> GetByIds(List<Guid> ids)
        {
            var cList = _context.Users.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public User Update(User c)
        {
            _context.Users.Update(c);
            return c;
        }
    }
}
