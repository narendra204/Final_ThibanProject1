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
    
    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            this.customeraddressproofs = new HashSet<customeraddressproof>();
            this.customerappartmentaddresses = new HashSet<customerappartmentaddress>();
            this.customerbankdetails = new HashSet<customerbankdetail>();
            this.customerchaletaddresses = new HashSet<customerchaletaddress>();
            this.customerdefaultaddresses = new HashSet<customerdefaultaddress>();
            this.customerfavoriteproducts = new HashSet<customerfavoriteproduct>();
            this.customerofficeaddresses = new HashSet<customerofficeaddress>();
            this.customerotheraddresses = new HashSet<customerotheraddress>();
            this.customerpaymentcards = new HashSet<customerpaymentcard>();
            this.customervillaaddresses = new HashSet<customervillaaddress>();
            this.driverratings = new HashSet<driverrating>();
            this.orders = new HashSet<order>();
            this.oredercanclefeedbacks = new HashSet<oredercanclefeedback>();
            this.shippingaddresses = new HashSet<shippingaddress>();
            this.national_address = new HashSet<national_address>();
        }
    
        public int customerid { get; set; }
        public string emailid { get; set; }
        public string name { get; set; }
        public string mobileno { get; set; }
        public Nullable<int> verifyotp { get; set; }
        public string verifyemail { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public string customer_gender { get; set; }
        public string customer_phonetype { get; set; }
        public string customer_nationality { get; set; }
        public string customer_type { get; set; }
        public string telicon_carrier { get; set; }
        public Nullable<int> createdby { get; set; }
        public string custstatus { get; set; }
        public Nullable<int> Image { get; set; }
        public string DOB { get; set; }
        public string Image_path { get; set; }
    
        public virtual ImageFile ImageFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customeraddressproof> customeraddressproofs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerappartmentaddress> customerappartmentaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerbankdetail> customerbankdetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerchaletaddress> customerchaletaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerdefaultaddress> customerdefaultaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerfavoriteproduct> customerfavoriteproducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerofficeaddress> customerofficeaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerotheraddress> customerotheraddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customerpaymentcard> customerpaymentcards { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customervillaaddress> customervillaaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driverrating> driverratings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oredercanclefeedback> oredercanclefeedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shippingaddress> shippingaddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<national_address> national_address { get; set; }
    }
}