using System.ComponentModel.DataAnnotations.Schema;
using Warehouse_Manager.Data.Base;

namespace Warehouse_Manager.MVVM.Model
{
    public class ShippingAddress : IEntityBase
    {
        public int Id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public int Building { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
