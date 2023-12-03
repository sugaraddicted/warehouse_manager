using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manager.MVVM.Model
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public OrderDetails OrderDetails { get; set; }
    }
}
