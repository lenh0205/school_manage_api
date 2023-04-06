using DataInfastructure.Model;
using System.Collections.Generic;
using System;

namespace DataInfastructure.IResponsitory
{
    public interface ITeacherRepository
    {
        public Teacher GetById(Guid id);
        public List<Teacher> GetByIds(List<Guid> ids);
        public Teacher Add(Teacher c);
        public Teacher Update(Teacher c);
        public bool Delete(Guid id);
    }
}
