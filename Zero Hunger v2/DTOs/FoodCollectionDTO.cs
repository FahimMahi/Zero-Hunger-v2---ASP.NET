using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger_v2.DTOs
{
    public class FoodCollectionDTO
    {
        public int RID { get; set; }
        [Required]
        public string FoodName { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public string MaxPreserve { get; set; }
        [Required]
        public string Status { get; set; }
        public int? AssignedEmpID { get; set; }
        [Required]
        public string Note { get; set; }
        public int RestID { get; set; }
        public System.DateTime RequestTime { get; set; }
        public DateTime? CollectionTime { get; internal set; }
        public DateTime? CompletionTime { get; internal set; }
    }
}