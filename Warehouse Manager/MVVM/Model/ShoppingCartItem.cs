using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manager.MVVM.Model
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public OrderItem OrderItem { get; set; }

        public string ShoppingCartId { get; set; }

    }
}
