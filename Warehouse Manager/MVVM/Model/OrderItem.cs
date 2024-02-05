using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace Warehouse_Manager.MVVM.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
