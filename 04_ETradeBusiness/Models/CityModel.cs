using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;
using ETradeEntities.Entities;

namespace ETradeBusiness.Models
{
    public class CityModel : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public CountryModel Country { get; set; }
    }
}
