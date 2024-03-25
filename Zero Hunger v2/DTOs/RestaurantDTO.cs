using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class RestaurantDTO
    {
        public int RestaurantID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int ContactNumber { get; set; }
        public int UserID { get; set; }
        [Required]
        public int TINID { get; set; }
    }
}