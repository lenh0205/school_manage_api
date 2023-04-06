using DataInfastructure.Model;
using System.Collections.Generic;
using System;

namespace DataInfastructure.IResponsitory
{
    public interface IUserRoleMappingRepository
    {
        public UserRoleMapping GetById(Guid id);
        public List<UserRoleMapping> GetByIds(List<Guid> ids);
        public UserRoleMapping Add(UserRoleMapping c);
        public UserRoleMapping Update(UserRoleMapping c);
        public bool Delete(Guid id);
    }
}
