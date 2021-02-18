using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;
using ETradeEntities.Entities;

namespace ETradeBusiness.Models
{
    public class UserModel : RecordBase
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Password { get; set; }

        [MinLength(3)]
        [MaxLength(10)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public int UserDetailId { get; set; }

        public RoleModel Role { get; set; }

        public UserDetailModel UserDetail { get; set; }
    }
}
