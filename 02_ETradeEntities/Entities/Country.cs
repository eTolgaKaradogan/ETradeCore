using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;

namespace ETradeEntities.Entities
{
    public class Country : RecordBase
    {
        [Required]
        [StringLength(150)] 
        public string Name { get; set; }
        public List<City> Cities { get; set; }

        // Country ile UserDetails arasında 1 to Many ilişki olduğu için Country'de List of UserDetails, UserDetails'de de Country tanımlanmalıdır.
        public List<UserDetail> UserDetails { get; set; }
    }
}
