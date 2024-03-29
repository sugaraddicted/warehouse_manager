﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse_Manager.Data.Base;

namespace Warehouse_Manager.MVVM.Model
{
    public class Order : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShippingAddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerNotes { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
