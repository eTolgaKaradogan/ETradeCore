using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;

namespace ETradeEntities.Entities
{
    public class User : RecordBase
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Password { get; set; }
        public bool Active { get; set; }

        public int RoleId { get; set; }

        // Role ile User arasında 1 to Many ilişki olduğu için Role'de List of Users, User'da da Role tanımlanmalıdır.
        public Role Role { get; set; }

        public int UserDetailId { get; set; }
        // UserDetail ile User arasında 1 to 1 ilişki olduğu için User'da UserDetail, UserDetail'da da User tanımlanmalıdır.
        public UserDetail UserDetail { get; set; }
    }
}
