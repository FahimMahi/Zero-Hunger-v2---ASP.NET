using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}