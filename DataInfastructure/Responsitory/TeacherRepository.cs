using DataInfastructure.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using DataInfastructure.IResponsitory;

namespace DataInfastructure.Responsitory
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolDBContext _context;
        public TeacherRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public Teacher Add(Teacher c)
        {

            _context.Teachers.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.Teachers.Remove(item);
            return true;
        }

        public Teacher GetById(Guid id)
        {
            var c = _context.Teachers.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<Teacher> GetByIds(List<Guid> ids)
        {
            var cList = _context.Teachers.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public Teacher Update(Teacher c)
        {
            _context.Teachers.Update(c);
            return c;
        }
    }
}
