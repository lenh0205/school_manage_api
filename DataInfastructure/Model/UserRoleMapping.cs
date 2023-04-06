using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataInfastructure.Model
{
    public class UserRoleMapping : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
