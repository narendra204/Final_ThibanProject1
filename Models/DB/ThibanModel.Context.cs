﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ThibanWaterDBEntities : DbContext
    {
        public ThibanWaterDBEntities()
            : base("name=ThibanWaterDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<advertisement> advertisements { get; set; }
        public virtual DbSet<agreement> agreements { get; set; }
        public virtual DbSet<AverageCompositionPPM> AverageCompositionPPMs { get; set; }
        public virtual DbSet<bank> banks { get; set; }
        public virtual DbSet<BottleMaterial> BottleMaterials { get; set; }
        public virtual DbSet<BottlePerBox> BottlePerBoxes { get; set; }
        public virtual DbSet<BrandName> BrandNames { get; set; }
        public virtual DbSet<bussinessofficehour> bussinessofficehours { get; set; }
        public virtual DbSet<bussinesstype> bussinesstypes { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<CompanyType> CompanyTypes { get; set; }
        public virtual DbSet<ConventionalstoreMinimumOrder> ConventionalstoreMinimumOrders { get; set; }
        public virtual DbSet<ConventionalstoreMiximumOrderQuality> ConventionalstoreMiximumOrderQualities { get; set; }
        public virtual DbSet<coupon> coupons { get; set; }
        public virtual DbSet<CREATETABLEBottlePerBox> CREATETABLEBottlePerBoxes { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<customeraddressproof> customeraddressproofs { get; set; }
        public virtual DbSet<customerappartmentaddress> customerappartmentaddresses { get; set; }
        public virtual DbSet<customerbankdetail> customerbankdetails { get; set; }
        public virtual DbSet<customerchaletaddress> customerchaletaddresses { get; set; }
        public virtual DbSet<customerdefaultaddress> customerdefaultaddresses { get; set; }
        public virtual DbSet<customerfavoriteproduct> customerfavoriteproducts { get; set; }
        public virtual DbSet<CustomerMaximumOrderQuality> CustomerMaximumOrderQualities { get; set; }
        public virtual DbSet<CustomerMinimumOrder> CustomerMinimumOrders { get; set; }
        public virtual DbSet<customerofficeaddress> customerofficeaddresses { get; set; }
        public virtual DbSet<customerotheraddress> customerotheraddresses { get; set; }
        public virtual DbSet<customerpaymentcard> customerpaymentcards { get; set; }
        public virtual DbSet<customervillaaddress> customervillaaddresses { get; set; }
        public virtual DbSet<dgeofence> dgeofences { get; set; }
        public virtual DbSet<driver> drivers { get; set; }
        public virtual DbSet<driveraddressproff> driveraddressproffs { get; set; }
        public virtual DbSet<driverbankdetail> driverbankdetails { get; set; }
        public virtual DbSet<driverdefaultaddress> driverdefaultaddresses { get; set; }
        public virtual DbSet<driverlog> driverlogs { get; set; }
        public virtual DbSet<driverpayoutrequested> driverpayoutrequesteds { get; set; }
        public virtual DbSet<driverrating> driverratings { get; set; }
        public virtual DbSet<driverworkinghour> driverworkinghours { get; set; }
        public virtual DbSet<filestore> filestores { get; set; }
        public virtual DbSet<geofence> geofences { get; set; }
        public virtual DbSet<ImageFile> ImageFiles { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<menuitem> menuitems { get; set; }
        public virtual DbSet<minimumorderstatu> minimumorderstatus { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<oredercanclefeedback> oredercanclefeedbacks { get; set; }
        public virtual DbSet<pandetail> pandetails { get; set; }
        public virtual DbSet<payoutall> payoutalls { get; set; }
        public virtual DbSet<PHNumber> PHNumbers { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<ProductAvailability> ProductAvailabilities { get; set; }
        public virtual DbSet<productratting> productrattings { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<rolemanagement> rolemanagements { get; set; }
        public virtual DbSet<shippingaddress> shippingaddresses { get; set; }
        public virtual DbSet<shippinghistory> shippinghistories { get; set; }
        public virtual DbSet<storedetail> storedetails { get; set; }
        public virtual DbSet<teammanagement> teammanagements { get; set; }
        public virtual DbSet<vattandetail> vattandetails { get; set; }
        public virtual DbSet<vechicle> vechicles { get; set; }
        public virtual DbSet<vender> venders { get; set; }
        public virtual DbSet<venderaddressproof> venderaddressproofs { get; set; }
        public virtual DbSet<venderbankdetail> venderbankdetails { get; set; }
        public virtual DbSet<venderdefaultaddress> venderdefaultaddresses { get; set; }
        public virtual DbSet<venderlog> venderlogs { get; set; }
        public virtual DbSet<venderpayoutall> venderpayoutalls { get; set; }
        public virtual DbSet<venderpayoutrequested> venderpayoutrequesteds { get; set; }
        public virtual DbSet<Volume> Volumes { get; set; }
        public virtual DbSet<CmsPage> CmsPages { get; set; }
        public virtual DbSet<mobile_otp> mobile_otp { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<delivery> deliveries { get; set; }
        public virtual DbSet<national_address> national_address { get; set; }
        public virtual DbSet<odometerreading> odometerreadings { get; set; }
        public virtual DbSet<onspotsale> onspotsales { get; set; }
        public virtual DbSet<orderdeliveryweather> orderdeliveryweathers { get; set; }
        public virtual DbSet<warehouseaddress> warehouseaddresses { get; set; }
        public virtual DbSet<cust_notification> cust_notification { get; set; }
        public virtual DbSet<General_feeback> General_feeback { get; set; }
        public virtual DbSet<product_feedback> product_feedback { get; set; }
    }
}