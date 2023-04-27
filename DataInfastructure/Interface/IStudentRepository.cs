using DataInfastructure.Model;
using System.Collections.Generic;
using System;
using DataInfastructure.Interface;

namespace DataInfastructure.IResponsitory
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        public void UpdateMoniter();
    }
}
