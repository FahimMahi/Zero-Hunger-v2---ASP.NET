using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class RestaurantDTO
    {
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ContactNumber { get; set; }
        public int UserID { get; set; }
        public int TINID { get; set; }
    }
}