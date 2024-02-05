using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manager.Exeptions
{
    public class UserNotFoundExeption : Exception
    {
        public string Username { get; set; }
        public UserNotFoundExeption(string username)
        {
            Username = username;
        }

        public UserNotFoundExeption(string? message, string username) : base(message)
        {
            Username = username;
        }

        public UserNotFoundExeption(string? message, Exception? innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
