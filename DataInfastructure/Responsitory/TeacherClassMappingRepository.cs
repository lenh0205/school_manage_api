using DataInfastructure.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using DataInfastructure.IResponsitory;

namespace DataInfastructure.Responsitory
{
    public class TeacherClassMappingRepository : ITeacherClassMappingRepository
    {
        private readonly SchoolDBContext _context;
        public TeacherClassMappingRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public TeacherClassMapping Add(TeacherClassMapping c)
        {

            _context.TeacherClassMappings.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.TeacherClassMappings.Remove(item);
            return true;
        }

        public TeacherClassMapping GetById(Guid id)
        {
            var c = _context.TeacherClassMappings.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<TeacherClassMapping> GetByIds(List<Guid> ids)
        {
            var cList = _context.TeacherClassMappings.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public TeacherClassMapping Update(TeacherClassMapping c)
        {
            _context.TeacherClassMappings.Update(c);
            return c;
        }
    }
}
