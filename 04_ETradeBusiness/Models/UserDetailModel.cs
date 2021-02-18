using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AppCore.Records.Bases;
using ETradeEntities.Entities;

namespace ETradeBusiness.Models
{
    public class UserDetailModel : RecordBase
    {
        [Required]
        [StringLength(200)]
        [EmailAddress]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }

        [DisplayName("City")]
        public int CityId { get; set; }

        [DisplayName("Address")]
        public string Adress { get; set; }
    }
}