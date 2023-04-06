using DataInfastructure.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using DataInfastructure.IResponsitory;

namespace DataInfastructure.Responsitory
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDBContext _context;
        public StudentRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public Student Add(Student c)
        {

            _context.Students.Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.Students.Remove(item);
            return true;
        }

        public Student GetById(Guid id)
        {
            var c = _context.Students.FirstOrDefault(c => c.Id == id);
            return c;
        }

        public List<Student> GetByIds(List<Guid> ids)
        {
            var cList = _context.Students.Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public Student Update(Student c)
        {
            _context.Students.Update(c);
            return c;
        }
    }
}
