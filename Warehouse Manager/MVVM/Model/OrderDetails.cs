using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manager.MVVM.Model
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public string OrderStatus { get; set; }

        public string ShippingAddress { get; set; }

        public string PaymentDetails { get; set; }

        public string CustomerNotes { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

    }
}
