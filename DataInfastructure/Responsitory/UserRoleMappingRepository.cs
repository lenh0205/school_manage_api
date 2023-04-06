using DataInfastructure.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using DataInfastructure.IResponsitory;

namespace DataInfastructure.Responsitory
{
    public class UserRoleMappingRepository : IUserRoleMappingRepository
    {
        private readonly SchoolDBContext _context;
        public UserRoleMappingRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public UserRoleMapping Add(UserRoleMapping c)
        {

            _context.UserRoleMappings.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.UserRoleMappings.Remove(item);
            return true;
        }

        public UserRoleMapping GetById(Guid id)
        {
            var c = _context.UserRoleMappings.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<UserRoleMapping> GetByIds(List<Guid> ids)
        {
            var cList = _context.UserRoleMappings.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public UserRoleMapping Update(UserRoleMapping c)
        {
            _context.UserRoleMappings.Update(c);
            return c;
        }

    }
}
