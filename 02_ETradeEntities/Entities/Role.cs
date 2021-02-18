using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;

namespace ETradeEntities.Entities
{
    public class Role : RecordBase
    {
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        // Role ile User arasında 1 to Many ilişki olduğu için Role'de List of Users, User'da da Role tanımlanmalıdır.
        public List<User> Users { get; set; }
    }
}
