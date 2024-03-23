using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public System.DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string BloodGroup { get; set; }
        public int UserID { get; set; }
        public int nid { get; set; }
    }
}