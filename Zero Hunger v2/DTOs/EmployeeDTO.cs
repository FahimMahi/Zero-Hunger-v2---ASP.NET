using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public System.DateTime DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Religion { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        public int UserID { get; set; }
        [Required]
        public int nid { get; set; }
    }
}