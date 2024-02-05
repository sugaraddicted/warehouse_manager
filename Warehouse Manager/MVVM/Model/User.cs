using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.Data.Base;

namespace Warehouse_Manager.MVVM.Model
{
    public class User: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Firstname { get; set; }

        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string UserRole { get; set; }
    }
}
