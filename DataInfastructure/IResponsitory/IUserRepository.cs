using DataInfastructure.Model;
using System.Collections.Generic;
using System;

namespace DataInfastructure.IResponsitory
{
    public interface IUserRepository
    {
        public User GetById(Guid id);
        public List<User> GetByIds(List<Guid> ids);
        public User Add(User c);
        public User Update(User c);
        public bool Delete(Guid id);
    }
}
