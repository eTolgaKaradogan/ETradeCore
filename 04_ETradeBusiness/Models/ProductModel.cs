using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace ETradeBusiness.Models
{
    public class ProductModel : RecordBase
    {
        // FluentValidation 
        
        #region Entity Kısmı
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        [DisplayName("Category Name")]
        public int CategoryId { get; set; }

        [DisplayName("Create Date")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Update Date")]
        public DateTime? UpdateDate { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; }
        #endregion

        // kendi ihtiyacımız olan şeyleri set etmemize gerek yok

        [DisplayName("Category Name")]
        public string DisplayCategoryName { get; set; }

        private string _displayCreateDate;
        [DisplayName("Create Date")]
        public string DisplayCreateDate
        {
            get
            {
                return CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                _displayCreateDate = value;
            }
        }

        [DisplayName("Update Date")]
        public string DisplayUpdateDate { get; set; }
    }
}
