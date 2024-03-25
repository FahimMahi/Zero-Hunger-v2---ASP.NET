//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zero_Hunger_v2.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodCollection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FoodCollection()
        {
            this.FoodDistributers = new HashSet<FoodDistributer>();
        }
    
        public int RID { get; set; }
        public string FoodName { get; set; }
        public int amount { get; set; }
        public string MaxPreserve { get; set; }
        public string Status { get; set; }
        public Nullable<int> AssignedEmpID { get; set; }
        public Nullable<System.DateTime> CollectionTime { get; set; }
        public Nullable<System.DateTime> CompletionTime { get; set; }
        public string Note { get; set; }
        public int RestID { get; set; }
        public System.DateTime RequestTime { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodDistributer> FoodDistributers { get; set; }
    }
}
