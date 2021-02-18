using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace ETradeBusiness.Models
{
    public class CategoryModel : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
