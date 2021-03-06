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
    
    public partial class driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public driver()
        {
            this.driveraddressproffs = new HashSet<driveraddressproff>();
            this.driverbankdetails = new HashSet<driverbankdetail>();
            this.driverdefaultaddresses = new HashSet<driverdefaultaddress>();
            this.driverlogs = new HashSet<driverlog>();
            this.driverpayoutrequesteds = new HashSet<driverpayoutrequested>();
            this.driverratings = new HashSet<driverrating>();
            this.driverworkinghours = new HashSet<driverworkinghour>();
            this.geofences = new HashSet<geofence>();
            this.payoutalls = new HashSet<payoutall>();
            this.shippinghistories = new HashSet<shippinghistory>();
            this.vechicles = new HashSet<vechicle>();
            this.deliveries = new HashSet<delivery>();
            this.odometerreadings = new HashSet<odometerreading>();
            this.onspotsales = new HashSet<onspotsale>();
            this.orderdeliveryweathers = new HashSet<orderdeliveryweather>();
        }
    
        public int driverid { get; set; }
        public string emailid { get; set; }
        public string name { get; set; }
        public string dusername { get; set; }
        public string password { get; set; }
        public string mobile_no { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public Nullable<int> vender_id { get; set; }
        public string driver_nationality { get; set; }
        public string gender { get; set; }
        public string driver_phone_type { get; set; }
        public string driver_divice_id { get; set; }
        public string driver_telicom_carrer { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public Nullable<int> verify_otp { get; set; }
        public string verify_email { get; set; }
        public Nullable<int> createdby { get; set; }
        public string status { get; set; }
        public Nullable<int> image { get; set; }
        public string Image_path { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
    
        public virtual ImageFile ImageFile { get; set; }
        public virtual vender vender { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driveraddressproff> driveraddressproffs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverbankdetail> driverbankdetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverdefaultaddress> driverdefaultaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverlog> driverlogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverpayoutrequested> driverpayoutrequesteds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverrating> driverratings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverworkinghour> driverworkinghours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<geofence> geofences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<payoutall> payoutalls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shippinghistory> shippinghistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vechicle> vechicles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<delivery> deliveries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<odometerreading> odometerreadings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<onspotsale> onspotsales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderdeliveryweather> orderdeliveryweathers { get; set; }
    }
}
