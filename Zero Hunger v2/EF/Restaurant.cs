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
    
    public partial class Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurant()
        {
            this.FoodCollections = new HashSet<FoodCollection>();
        }
    
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ContactNumber { get; set; }
        public int UserID { get; set; }
        public int TINID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodCollection> FoodCollections { get; set; }
        public virtual User User { get; set; }
    }
}
