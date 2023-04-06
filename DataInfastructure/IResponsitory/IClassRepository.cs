using DataInfastructure.Model;
using System;
using System.Collections.Generic;

namespace DataInfastructure.IResponsitory
{
    public interface IClassRepository
    {

        public Class GetById(Guid id);
        public List<Class> GetByIds(List<Guid> ids);
        public Class Add(Class c);
        public Class Update(Class c);
        public bool Delete(Guid id);
    }
}
