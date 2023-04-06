using DataInfastructure.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using DataInfastructure.IResponsitory;

namespace DataInfastructure.Responsitory
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SchoolDBContext _context;
        public SchoolRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public School Add(School c)
        {
            _context.Schools.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.Schools.Remove(item);
            return true;
        }

        public School GetById(Guid id)
        {
            var c = _context.Schools.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<School> GetByIds(List<Guid> ids)
        {
            var cList = _context.Schools.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public School Update(School c)
        {
            _context.Schools.Update(c);
            return c;
        }
    }
}
