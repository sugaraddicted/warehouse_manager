using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;

namespace Warehouse_Manager.MVVM.Model
{
    public class ShoppingCartItem : IEntityBase, INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        public string ShoppingCartId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
