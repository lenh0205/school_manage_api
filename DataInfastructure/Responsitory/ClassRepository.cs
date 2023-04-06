using DataInfastructure.IResponsitory;
using DataInfastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataInfastructure.Responsitory
{
    public class ClassRepository : IClassRepository
    {
        private readonly SchoolDBContext _context;
        public ClassRepository(SchoolDBContext context)
        {
            _context = context;
        }


        public Class Add(Class c)
        {

            _context.Classes.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.Classes.Remove(item);
            return true;
        }

        public Class GetById(Guid id)
        {
            var c = _context.Classes.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<Class> GetByIds(List<Guid> ids)
        {
            var cList = _context.Classes.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public Class Update(Class c)
        {
            _context.Classes.Update(c);
            return c;
        }
    }
}
