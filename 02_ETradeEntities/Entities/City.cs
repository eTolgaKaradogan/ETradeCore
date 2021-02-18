using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;

namespace ETradeEntities.Entities
{
    public class City : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<UserDetail> UserDetails { get; set; }
    }
}
