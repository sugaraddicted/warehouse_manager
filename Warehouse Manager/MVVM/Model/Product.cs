using System;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse_Manager.Data.Base;
using System.Windows.Media;
using Warehouse_Manager.Data;

namespace Warehouse_Manager.MVVM.Model
{
    public class Product : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }

        // Binary content properties
        public byte[] BinaryContent { get; set; }
        public string FileType { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime ModificationDate { get; set; }

        [NotMapped]
        public ImageSource Source { get; set; }

        public bool ValidatePrice()
        {
            if (Price < 0)
            {
                return false;
            }

            return true;
        }

        public bool ValidateStockQuantity()
        {
            if (StockQuantity < 0)
            {
                return false;
            }

            return true;
        }

        public bool ValidateBinaryContent()
        {
            if (!FileType.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) &&
                !FileType.Equals("image/png", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            if (BinaryContent.Length == 0)
            {
                return false;
            }

            return true;
        }
        public void SetImageSource()
        {
            Source =  Helper.ConvertByteArrayToImage(BinaryContent);
        }
    }
}
