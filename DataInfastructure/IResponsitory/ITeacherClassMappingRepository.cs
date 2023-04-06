using DataInfastructure.Model;
using System.Collections.Generic;
using System;

namespace DataInfastructure.IResponsitory
{
    public interface ITeacherClassMappingRepository
    {
        public TeacherClassMapping GetById(Guid id);
        public List<TeacherClassMapping> GetByIds(List<Guid> ids);
        public TeacherClassMapping Add(TeacherClassMapping c);
        public TeacherClassMapping Update(TeacherClassMapping c);
        public bool Delete(Guid id);
    }
}
