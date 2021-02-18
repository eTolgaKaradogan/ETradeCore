using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;
using ETradeEntities.Entities;

namespace ETradeBusiness.Models
{
    public class CountryModel : RecordBase
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public List<CityModel> Cities { get; set; }
    }
}
