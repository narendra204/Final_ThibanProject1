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
    
    public partial class vender
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vender()
        {
            this.advertisements = new HashSet<advertisement>();
            this.agreements = new HashSet<agreement>();
            this.coupons = new HashSet<coupon>();
            this.drivers = new HashSet<driver>();
            this.geofences = new HashSet<geofence>();
            this.minimumorderstatus = new HashSet<minimumorderstatu>();
            this.pandetails = new HashSet<pandetail>();
            this.products = new HashSet<product>();
            this.storedetails = new HashSet<storedetail>();
            this.teammanagements = new HashSet<teammanagement>();
            this.vattandetails = new HashSet<vattandetail>();
            this.venderaddressproofs = new HashSet<venderaddressproof>();
            this.venderbankdetails = new HashSet<venderbankdetail>();
            this.venderdefaultaddresses = new HashSet<venderdefaultaddress>();
            this.venderlogs = new HashSet<venderlog>();
            this.venderpayoutalls = new HashSet<venderpayoutall>();
            this.venderpayoutrequesteds = new HashSet<venderpayoutrequested>();
        }
    
        public int venderid { get; set; }
        public string emailid { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public string mobile_no { get; set; }
        public Nullable<int> verifyotp { get; set; }
        public string verifyemail { get; set; }
        public Nullable<int> createdby { get; set; }
        public string status { get; set; }
        public Nullable<System.Guid> Activationid { get; set; }
        public Nullable<bool> Accepted { get; set; }
        public string MerchantId { get; set; }
        public string AdditionalInfor { get; set; }
        public Nullable<System.Guid> ForgotActivationId { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> image { get; set; }
        public string Image_path { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<advertisement> advertisements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<agreement> agreements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<coupon> coupons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driver> drivers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<geofence> geofences { get; set; }
        public virtual ImageFile ImageFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<minimumorderstatu> minimumorderstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pandetail> pandetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<storedetail> storedetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<teammanagement> teammanagements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vattandetail> vattandetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venderaddressproof> venderaddressproofs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venderbankdetail> venderbankdetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venderdefaultaddress> venderdefaultaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venderlog> venderlogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venderpayoutall> venderpayoutalls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venderpayoutrequested> venderpayoutrequesteds { get; set; }
    }
}