using System.Collections.Generic;

namespace DataInfastructure.Model
{
    public class Class : BaseEntity
    {
        public ICollection<Student> Students { get; set; }
    }
}
