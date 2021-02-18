using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;

namespace ETradeEntities.Entities
{
    public class UserDetail : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string Adress { get; set; }

        // UserDetail ile User arasında 1 to 1 ilişki olduğu için User'da UserDetail, UserDetail'da da User tanımlanmalıdır.
        public User User { get; set; }
    }
}
