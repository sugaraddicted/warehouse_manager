using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manager.Dto
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation  { get; set; }
    }
}
