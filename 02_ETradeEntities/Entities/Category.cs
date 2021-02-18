using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AppCore.Records.Bases;

namespace ETradeEntities.Entities
{
    public class Category : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
