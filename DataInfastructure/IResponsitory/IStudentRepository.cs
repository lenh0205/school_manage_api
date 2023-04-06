using DataInfastructure.Model;
using System.Collections.Generic;
using System;

namespace DataInfastructure.IResponsitory
{
    public interface IStudentRepository
    {
        public Student GetById(Guid id);
        public List<Student> GetByIds(List<Guid> ids);
        public Student Add(Student c);
        public Student Update(Student c);
        public bool Delete(Guid id);
    }
}
