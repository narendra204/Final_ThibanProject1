//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_ThibanProject.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class dgeofence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dgeofence()
        {
            this.geofences = new HashSet<geofence>();
        }
    
        public int geofenceid { get; set; }
        public System.Data.Entity.Spatial.DbGeography location { get; set; }
        public string zone_name { get; set; }
        public string city_name { get; set; }
        public string direction_name { get; set; }
        public Nullable<bool> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<geofence> geofences { get; set; }
    }
}
