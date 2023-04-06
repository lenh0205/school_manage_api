using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataInfastructure.Model
{
    public class School : BaseEntity
    {
        [ForeignKey("Class")]
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
    }
}
