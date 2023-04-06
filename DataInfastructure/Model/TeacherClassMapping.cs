using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataInfastructure.Model
{
    public class TeacherClassMapping : BaseEntity
    {
        [ForeignKey("Class")]
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        [ForeignKey("Teacher")]
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }


        public bool IsHeadTeacher { get; set; }
    }
}
