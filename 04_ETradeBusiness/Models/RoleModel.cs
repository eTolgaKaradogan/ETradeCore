using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;

namespace ETradeBusiness.Models
{
    public class RoleModel : RecordBase
    {
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
