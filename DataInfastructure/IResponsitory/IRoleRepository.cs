using DataInfastructure.Model;
using System.Collections.Generic;
using System;

namespace DataInfastructure.IResponsitory
{
    public interface IRoleRepository
    {
        public Role GetById(Guid id);
        public List<Role> GetByIds(List<Guid> ids);
        public Role Add(Role c);
        public Role Update(Role c);
        public bool Delete(Guid id);
    }
}
