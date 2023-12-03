using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;

namespace Warehouse_Manager.MVVM.Model
{
    public class Product : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        // Binary content properties
        public byte[] BinaryContent { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }

        [MaxLength(255)]
        public string FileType { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime ModificationDate { get; set; }

        // Validation for Price
        public bool ValidatePrice()
        {
            if (Price < 0)
            {
                return false;
            }

            return true;
        }

        // Validation for StockQuantity
        public bool ValidateStockQuantity()
        {
            if (StockQuantity < 0)
            {
                return false;
            }

            return true;
        }

        // Validation for BinaryContent
        public bool ValidateBinaryContent()
        {
            // Validate file type
            if (!FileType.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) &&
                !FileType.Equals("image/png", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            // Validate content integrity
            if (BinaryContent.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
