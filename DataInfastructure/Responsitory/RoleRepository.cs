using DataInfastructure.Model;
using DataInfastructure;
using Microsoft.EntityFrameworkCore;
using DataInfastructure.IResponsitory;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DataInfastructure.Responsitory
{
    public class RoleRepository : RepositoryBase<Role> ,IRoleRepository
    {
        private readonly SchoolDBContext _context;
        public RoleRepository(SchoolDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
