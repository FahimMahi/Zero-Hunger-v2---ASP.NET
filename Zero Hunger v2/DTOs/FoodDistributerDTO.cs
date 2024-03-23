using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class FoodDistributerDTO
    {
        public int DID { get; set; }
        public System.DateTime Distributetime { get; set; }
        public string Location { get; set; }
        public int Distributednumber { get; set; }
        public string notes { get; set; }
        public int RID { get; set; }
        public int EmpID { get; set; }
    }
}