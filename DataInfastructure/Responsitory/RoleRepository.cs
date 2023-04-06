using DataInfastructure.Model;
using DataInfastructure;
using Microsoft.EntityFrameworkCore;
using DataInfastructure.IResponsitory;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DataInfastructure.Responsitory
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SchoolDBContext _context;
        public RoleRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public Role Add(Role c)
        {

            _context.Roles.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.Roles.Remove(item);
            return true;
        }

        public Role GetById(Guid id)
        {
            var c = _context.Roles.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<Role> GetByIds(List<Guid> ids)
        {
            var cList = _context.Roles.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public Role Update(Role c)
        {
            _context.Roles.Update(c);
            return c;
        }
    }
}
