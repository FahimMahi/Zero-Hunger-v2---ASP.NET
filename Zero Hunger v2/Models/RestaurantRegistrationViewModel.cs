using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zero_Hunger_v2.DTOs;

namespace Zero_Hunger_v2.Models
{
    public class RestaurantRegistrationViewModel
    {
        public UserDTO User { get; set; }
        public RestaurantDTO Restaurant { get; set; }
    }
}