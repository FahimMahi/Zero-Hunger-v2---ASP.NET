using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class FoodCollectionDTO
    {
        public int RID { get; set; }
        public string FoodName { get; set; }
        public int amount { get; set; }
        public string MaxPreserve { get; set; }
        public string Status { get; set; }
        public int AssignedEmpID { get; set; }
        public string Note { get; set; }
        public int RestID { get; set; }
        public System.DateTime RequestTime { get; set; }
    }
}