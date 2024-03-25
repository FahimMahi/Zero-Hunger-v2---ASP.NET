using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class FoodDistributerDTO
    {
        public int DID { get; set; }
        [Required]
        public System.DateTime Distributetime { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Distributednumber { get; set; }
        [Required]
        public string notes { get; set; }
        [Required]
        public int RID { get; set; }
        [Required]
        public int EmpID { get; set; }
    }
}