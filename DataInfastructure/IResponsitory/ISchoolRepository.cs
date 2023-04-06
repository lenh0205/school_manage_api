using DataInfastructure.Model;
using System.Collections.Generic;
using System;

namespace DataInfastructure.IResponsitory
{
    public interface ISchoolRepository
    {
        public School GetById(Guid id);
        public List<School> GetByIds(List<Guid> ids);
        public School Add(School c);
        public School Update(School c);
        public bool Delete(Guid id);
    }
}
