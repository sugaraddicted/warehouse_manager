using System.Windows.Media;
using Warehouse_Manager.Data;

namespace Warehouse_Manager.Dto
{
    public class ProductDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; }

        public byte[] BinaryContent { get; set; }

        public string FileType { get; set; } = ".jpg";

        public ImageSource? Image { get; set; }

        public void SetImageSource()
        {
            Image = Helper.ConvertByteArrayToImage(BinaryContent);
        }
    }
}
