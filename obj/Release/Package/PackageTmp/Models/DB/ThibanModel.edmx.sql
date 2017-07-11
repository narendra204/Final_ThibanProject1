
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/26/2017 16:07:08
-- Generated from EDMX file: D:\Project\Final_ThibanProject\Models\DB\ThibanModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ThibanWateDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__admin__Image__5E8A0973]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[admin] DROP CONSTRAINT [FK__admin__Image__5E8A0973];
GO
IF OBJECT_ID(N'[dbo].[FK__advertise__vende__6A30C649]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[advertisement] DROP CONSTRAINT [FK__advertise__vende__6A30C649];
GO
IF OBJECT_ID(N'[dbo].[FK__agreement__FileI__37703C52]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[agreement] DROP CONSTRAINT [FK__agreement__FileI__37703C52];
GO
IF OBJECT_ID(N'[dbo].[FK__agreement__Vende__367C1819]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[agreement] DROP CONSTRAINT [FK__agreement__Vende__367C1819];
GO
IF OBJECT_ID(N'[dbo].[FK__category__Image__5F7E2DAC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[category] DROP CONSTRAINT [FK__category__Image__5F7E2DAC];
GO
IF OBJECT_ID(N'[dbo].[FK__coupon__vender_i__6B24EA82]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[coupon] DROP CONSTRAINT [FK__coupon__vender_i__6B24EA82];
GO
IF OBJECT_ID(N'[dbo].[FK__customer__Image__5D95E53A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customer] DROP CONSTRAINT [FK__customer__Image__5D95E53A];
GO
IF OBJECT_ID(N'[dbo].[FK__customera__custo__6C190EBB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customeraddressproof] DROP CONSTRAINT [FK__customera__custo__6C190EBB];
GO
IF OBJECT_ID(N'[dbo].[FK__customera__custo__6D0D32F4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerappartmentaddress] DROP CONSTRAINT [FK__customera__custo__6D0D32F4];
GO
IF OBJECT_ID(N'[dbo].[FK__customera__Image__607251E5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customeraddressproof] DROP CONSTRAINT [FK__customera__Image__607251E5];
GO
IF OBJECT_ID(N'[dbo].[FK__customerb__custo__6E01572D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerbankdetail] DROP CONSTRAINT [FK__customerb__custo__6E01572D];
GO
IF OBJECT_ID(N'[dbo].[FK__customerc__custo__6EF57B66]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerchaletaddress] DROP CONSTRAINT [FK__customerc__custo__6EF57B66];
GO
IF OBJECT_ID(N'[dbo].[FK__customerd__custi__6FE99F9F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerdefaultaddress] DROP CONSTRAINT [FK__customerd__custi__6FE99F9F];
GO
IF OBJECT_ID(N'[dbo].[FK__customerf__custo__70DDC3D8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerfavoriteproduct] DROP CONSTRAINT [FK__customerf__custo__70DDC3D8];
GO
IF OBJECT_ID(N'[dbo].[FK__customerf__produ__71D1E811]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerfavoriteproduct] DROP CONSTRAINT [FK__customerf__produ__71D1E811];
GO
IF OBJECT_ID(N'[dbo].[FK__customero__custo__72C60C4A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerofficeaddress] DROP CONSTRAINT [FK__customero__custo__72C60C4A];
GO
IF OBJECT_ID(N'[dbo].[FK__customero__custo__73BA3083]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerotheraddress] DROP CONSTRAINT [FK__customero__custo__73BA3083];
GO
IF OBJECT_ID(N'[dbo].[FK__customerp__custo__74AE54BC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customerpaymentcard] DROP CONSTRAINT [FK__customerp__custo__74AE54BC];
GO
IF OBJECT_ID(N'[dbo].[FK__customerv__custo__75A278F5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customervillaaddress] DROP CONSTRAINT [FK__customerv__custo__75A278F5];
GO
IF OBJECT_ID(N'[dbo].[FK__driver__image__662B2B3B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driver] DROP CONSTRAINT [FK__driver__image__662B2B3B];
GO
IF OBJECT_ID(N'[dbo].[FK__driver__vender_i__76969D2E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driver] DROP CONSTRAINT [FK__driver__vender_i__76969D2E];
GO
IF OBJECT_ID(N'[dbo].[FK__driveradd__drive__778AC167]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driveraddressproff] DROP CONSTRAINT [FK__driveradd__drive__778AC167];
GO
IF OBJECT_ID(N'[dbo].[FK__driveradd__Image__6166761E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driveraddressproff] DROP CONSTRAINT [FK__driveradd__Image__6166761E];
GO
IF OBJECT_ID(N'[dbo].[FK__driverban__drive__787EE5A0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverbankdetail] DROP CONSTRAINT [FK__driverban__drive__787EE5A0];
GO
IF OBJECT_ID(N'[dbo].[FK__driverdef__drive__797309D9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverdefaultaddress] DROP CONSTRAINT [FK__driverdef__drive__797309D9];
GO
IF OBJECT_ID(N'[dbo].[FK__driverlog__drive__7A672E12]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverlog] DROP CONSTRAINT [FK__driverlog__drive__7A672E12];
GO
IF OBJECT_ID(N'[dbo].[FK__driverpay__drive__7B5B524B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverpayoutrequested] DROP CONSTRAINT [FK__driverpay__drive__7B5B524B];
GO
IF OBJECT_ID(N'[dbo].[FK__driverrat__custo__7C4F7684]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverrating] DROP CONSTRAINT [FK__driverrat__custo__7C4F7684];
GO
IF OBJECT_ID(N'[dbo].[FK__driverrat__drive__7D439ABD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverrating] DROP CONSTRAINT [FK__driverrat__drive__7D439ABD];
GO
IF OBJECT_ID(N'[dbo].[FK__driverrat__order__7E37BEF6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverrating] DROP CONSTRAINT [FK__driverrat__order__7E37BEF6];
GO
IF OBJECT_ID(N'[dbo].[FK__driverwor__drive__7F2BE32F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[driverworkinghours] DROP CONSTRAINT [FK__driverwor__drive__7F2BE32F];
GO
IF OBJECT_ID(N'[dbo].[FK__geofence__driver__00200768]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[geofence] DROP CONSTRAINT [FK__geofence__driver__00200768];
GO
IF OBJECT_ID(N'[dbo].[FK__geofence__geofen__01142BA1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[geofence] DROP CONSTRAINT [FK__geofence__geofen__01142BA1];
GO
IF OBJECT_ID(N'[dbo].[FK__geofence__vender__02084FDA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[geofence] DROP CONSTRAINT [FK__geofence__vender__02084FDA];
GO
IF OBJECT_ID(N'[dbo].[FK__minimumor__vende__02FC7413]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[minimumorderstatus] DROP CONSTRAINT [FK__minimumor__vende__02FC7413];
GO
IF OBJECT_ID(N'[dbo].[FK__orders__customer__03F0984C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[orders] DROP CONSTRAINT [FK__orders__customer__03F0984C];
GO
IF OBJECT_ID(N'[dbo].[FK__orders__product___04E4BC85]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[orders] DROP CONSTRAINT [FK__orders__product___04E4BC85];
GO
IF OBJECT_ID(N'[dbo].[FK__oredercan__custo__05D8E0BE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[oredercanclefeedback] DROP CONSTRAINT [FK__oredercan__custo__05D8E0BE];
GO
IF OBJECT_ID(N'[dbo].[FK__oredercan__order__06CD04F7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[oredercanclefeedback] DROP CONSTRAINT [FK__oredercan__order__06CD04F7];
GO
IF OBJECT_ID(N'[dbo].[FK__pandetail__Image__625A9A57]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pandetail] DROP CONSTRAINT [FK__pandetail__Image__625A9A57];
GO
IF OBJECT_ID(N'[dbo].[FK__pandetail__vende__07C12930]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[pandetail] DROP CONSTRAINT [FK__pandetail__vende__07C12930];
GO
IF OBJECT_ID(N'[dbo].[FK__payoutall__drive__08B54D69]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[payoutall] DROP CONSTRAINT [FK__payoutall__drive__08B54D69];
GO
IF OBJECT_ID(N'[dbo].[FK__product__categor__09A971A2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[product] DROP CONSTRAINT [FK__product__categor__09A971A2];
GO
IF OBJECT_ID(N'[dbo].[FK__product__image__671F4F74]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[product] DROP CONSTRAINT [FK__product__image__671F4F74];
GO
IF OBJECT_ID(N'[dbo].[FK__product__vender___0A9D95DB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[product] DROP CONSTRAINT [FK__product__vender___0A9D95DB];
GO
IF OBJECT_ID(N'[dbo].[FK__productra__order__0B91BA14]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[productratting] DROP CONSTRAINT [FK__productra__order__0B91BA14];
GO
IF OBJECT_ID(N'[dbo].[FK__productra__produ__0C85DE4D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[productratting] DROP CONSTRAINT [FK__productra__produ__0C85DE4D];
GO
IF OBJECT_ID(N'[dbo].[FK__rolemanag__rolei__5AB9788F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[rolemanagement] DROP CONSTRAINT [FK__rolemanag__rolei__5AB9788F];
GO
IF OBJECT_ID(N'[dbo].[FK__shippinga__custo__0E6E26BF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shippingaddress] DROP CONSTRAINT [FK__shippinga__custo__0E6E26BF];
GO
IF OBJECT_ID(N'[dbo].[FK__shippinga__order__0F624AF8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shippingaddress] DROP CONSTRAINT [FK__shippinga__order__0F624AF8];
GO
IF OBJECT_ID(N'[dbo].[FK__shippingh__drive__10566F31]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shippinghistory] DROP CONSTRAINT [FK__shippingh__drive__10566F31];
GO
IF OBJECT_ID(N'[dbo].[FK__shippingh__order__114A936A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shippinghistory] DROP CONSTRAINT [FK__shippingh__order__114A936A];
GO
IF OBJECT_ID(N'[dbo].[FK__storedeta__vende__123EB7A3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[storedetail] DROP CONSTRAINT [FK__storedeta__vende__123EB7A3];
GO
IF OBJECT_ID(N'[dbo].[FK__teammanag__rolei__57DD0BE4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[teammanagement] DROP CONSTRAINT [FK__teammanag__rolei__57DD0BE4];
GO
IF OBJECT_ID(N'[dbo].[FK__teammanag__vende__14270015]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[teammanagement] DROP CONSTRAINT [FK__teammanag__vende__14270015];
GO
IF OBJECT_ID(N'[dbo].[FK__vattandet__Image__634EBE90]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[vattandetail] DROP CONSTRAINT [FK__vattandet__Image__634EBE90];
GO
IF OBJECT_ID(N'[dbo].[FK__vattandet__vende__151B244E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[vattandetail] DROP CONSTRAINT [FK__vattandet__vende__151B244E];
GO
IF OBJECT_ID(N'[dbo].[FK__vechicle__driver__160F4887]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[vechicle] DROP CONSTRAINT [FK__vechicle__driver__160F4887];
GO
IF OBJECT_ID(N'[dbo].[FK__vender__image__65370702]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[vender] DROP CONSTRAINT [FK__vender__image__65370702];
GO
IF OBJECT_ID(N'[dbo].[FK__venderadd__Image__6442E2C9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[venderaddressproof] DROP CONSTRAINT [FK__venderadd__Image__6442E2C9];
GO
IF OBJECT_ID(N'[dbo].[FK__venderadd__vende__17036CC0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[venderaddressproof] DROP CONSTRAINT [FK__venderadd__vende__17036CC0];
GO
IF OBJECT_ID(N'[dbo].[FK__venderban__vende__17F790F9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[venderbankdetail] DROP CONSTRAINT [FK__venderban__vende__17F790F9];
GO
IF OBJECT_ID(N'[dbo].[FK__venderdef__vende__18EBB532]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[venderdefaultaddress] DROP CONSTRAINT [FK__venderdef__vende__18EBB532];
GO
IF OBJECT_ID(N'[dbo].[FK__venderlog__vende__19DFD96B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[venderlog] DROP CONSTRAINT [FK__venderlog__vende__19DFD96B];
GO
IF OBJECT_ID(N'[dbo].[FK__venderpay__vende__1AD3FDA4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[venderpayoutall] DROP CONSTRAINT [FK__venderpay__vende__1AD3FDA4];
GO
IF OBJECT_ID(N'[dbo].[FK__venderpay__vende__1BC821DD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[venderpayoutrequested] DROP CONSTRAINT [FK__venderpay__vende__1BC821DD];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[admin];
GO
IF OBJECT_ID(N'[dbo].[advertisement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[advertisement];
GO
IF OBJECT_ID(N'[dbo].[agreement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[agreement];
GO
IF OBJECT_ID(N'[dbo].[AverageCompositionPPM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AverageCompositionPPM];
GO
IF OBJECT_ID(N'[dbo].[bank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[bank];
GO
IF OBJECT_ID(N'[dbo].[BottleMaterial]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BottleMaterial];
GO
IF OBJECT_ID(N'[dbo].[BottlePerBox]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BottlePerBox];
GO
IF OBJECT_ID(N'[dbo].[BrandName]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BrandName];
GO
IF OBJECT_ID(N'[dbo].[bussinessofficehours]', 'U') IS NOT NULL
    DROP TABLE [dbo].[bussinessofficehours];
GO
IF OBJECT_ID(N'[dbo].[bussinesstype]', 'U') IS NOT NULL
    DROP TABLE [dbo].[bussinesstype];
GO
IF OBJECT_ID(N'[dbo].[category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[category];
GO
IF OBJECT_ID(N'[dbo].[CompanyType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyType];
GO
IF OBJECT_ID(N'[dbo].[ConventionalstoreMinimumOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConventionalstoreMinimumOrder];
GO
IF OBJECT_ID(N'[dbo].[ConventionalstoreMiximumOrderQuality]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConventionalstoreMiximumOrderQuality];
GO
IF OBJECT_ID(N'[dbo].[coupon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[coupon];
GO
IF OBJECT_ID(N'[dbo].[CREATETABLEBottlePerBox]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CREATETABLEBottlePerBox];
GO
IF OBJECT_ID(N'[dbo].[customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customer];
GO
IF OBJECT_ID(N'[dbo].[customeraddressproof]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customeraddressproof];
GO
IF OBJECT_ID(N'[dbo].[customerappartmentaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerappartmentaddress];
GO
IF OBJECT_ID(N'[dbo].[customerbankdetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerbankdetail];
GO
IF OBJECT_ID(N'[dbo].[customerchaletaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerchaletaddress];
GO
IF OBJECT_ID(N'[dbo].[customerdefaultaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerdefaultaddress];
GO
IF OBJECT_ID(N'[dbo].[customerfavoriteproduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerfavoriteproduct];
GO
IF OBJECT_ID(N'[dbo].[CustomerMaximumOrderQuality]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerMaximumOrderQuality];
GO
IF OBJECT_ID(N'[dbo].[CustomerMinimumOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerMinimumOrder];
GO
IF OBJECT_ID(N'[dbo].[customerofficeaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerofficeaddress];
GO
IF OBJECT_ID(N'[dbo].[customerotheraddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerotheraddress];
GO
IF OBJECT_ID(N'[dbo].[customerpaymentcard]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customerpaymentcard];
GO
IF OBJECT_ID(N'[dbo].[customervillaaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customervillaaddress];
GO
IF OBJECT_ID(N'[dbo].[dgeofence]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dgeofence];
GO
IF OBJECT_ID(N'[dbo].[driver]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driver];
GO
IF OBJECT_ID(N'[dbo].[driveraddressproff]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driveraddressproff];
GO
IF OBJECT_ID(N'[dbo].[driverbankdetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driverbankdetail];
GO
IF OBJECT_ID(N'[dbo].[driverdefaultaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driverdefaultaddress];
GO
IF OBJECT_ID(N'[dbo].[driverlog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driverlog];
GO
IF OBJECT_ID(N'[dbo].[driverpayoutrequested]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driverpayoutrequested];
GO
IF OBJECT_ID(N'[dbo].[driverrating]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driverrating];
GO
IF OBJECT_ID(N'[dbo].[driverworkinghours]', 'U') IS NOT NULL
    DROP TABLE [dbo].[driverworkinghours];
GO
IF OBJECT_ID(N'[dbo].[filestore]', 'U') IS NOT NULL
    DROP TABLE [dbo].[filestore];
GO
IF OBJECT_ID(N'[dbo].[geofence]', 'U') IS NOT NULL
    DROP TABLE [dbo].[geofence];
GO
IF OBJECT_ID(N'[dbo].[ImageFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImageFile];
GO
IF OBJECT_ID(N'[dbo].[Language]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Language];
GO
IF OBJECT_ID(N'[dbo].[menuitem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[menuitem];
GO
IF OBJECT_ID(N'[dbo].[minimumorderstatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[minimumorderstatus];
GO
IF OBJECT_ID(N'[dbo].[orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[orders];
GO
IF OBJECT_ID(N'[dbo].[oredercanclefeedback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[oredercanclefeedback];
GO
IF OBJECT_ID(N'[dbo].[pandetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pandetail];
GO
IF OBJECT_ID(N'[dbo].[payoutall]', 'U') IS NOT NULL
    DROP TABLE [dbo].[payoutall];
GO
IF OBJECT_ID(N'[dbo].[PHNumber]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PHNumber];
GO
IF OBJECT_ID(N'[dbo].[product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[product];
GO
IF OBJECT_ID(N'[dbo].[ProductAvailability]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductAvailability];
GO
IF OBJECT_ID(N'[dbo].[productratting]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productratting];
GO
IF OBJECT_ID(N'[dbo].[Province]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Province];
GO
IF OBJECT_ID(N'[dbo].[role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[role];
GO
IF OBJECT_ID(N'[dbo].[rolemanagement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[rolemanagement];
GO
IF OBJECT_ID(N'[dbo].[shippingaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shippingaddress];
GO
IF OBJECT_ID(N'[dbo].[shippinghistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shippinghistory];
GO
IF OBJECT_ID(N'[dbo].[storedetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[storedetail];
GO
IF OBJECT_ID(N'[dbo].[teammanagement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[teammanagement];
GO
IF OBJECT_ID(N'[dbo].[vattandetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[vattandetail];
GO
IF OBJECT_ID(N'[dbo].[vechicle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[vechicle];
GO
IF OBJECT_ID(N'[dbo].[vender]', 'U') IS NOT NULL
    DROP TABLE [dbo].[vender];
GO
IF OBJECT_ID(N'[dbo].[venderaddressproof]', 'U') IS NOT NULL
    DROP TABLE [dbo].[venderaddressproof];
GO
IF OBJECT_ID(N'[dbo].[venderbankdetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[venderbankdetail];
GO
IF OBJECT_ID(N'[dbo].[venderdefaultaddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[venderdefaultaddress];
GO
IF OBJECT_ID(N'[dbo].[venderlog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[venderlog];
GO
IF OBJECT_ID(N'[dbo].[venderpayoutall]', 'U') IS NOT NULL
    DROP TABLE [dbo].[venderpayoutall];
GO
IF OBJECT_ID(N'[dbo].[venderpayoutrequested]', 'U') IS NOT NULL
    DROP TABLE [dbo].[venderpayoutrequested];
GO
IF OBJECT_ID(N'[dbo].[Volume]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Volume];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'advertisements'
CREATE TABLE [dbo].[advertisements] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NULL,
    [description] nvarchar(1000)  NULL,
    [vender_id] int  NULL,
    [video_image] varbinary(max)  NULL,
    [status] bit  NULL,
    [type] nvarchar(100)  NULL
);
GO

-- Creating table 'categories'
CREATE TABLE [dbo].[categories] (
    [categoryid] int IDENTITY(1,1) NOT NULL,
    [category_name] nvarchar(100)  NOT NULL,
    [description] nvarchar(1000)  NULL,
    [image] int  NULL,
    [createdby] int  NULL,
    [status] bit  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'customeraddressproofs'
CREATE TABLE [dbo].[customeraddressproofs] (
    [addressid] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [addressproof] nvarchar(100)  NULL,
    [image] int  NULL,
    [addressimage] nvarchar(200)  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'customerappartmentaddresses'
CREATE TABLE [dbo].[customerappartmentaddresses] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [appartment_street] nvarchar(100)  NULL,
    [appartment_number] nvarchar(100)  NULL,
    [floor_number] nvarchar(100)  NULL,
    [building_name] nvarchar(100)  NULL,
    [building_address_status] bit  NULL
);
GO

-- Creating table 'customerbankdetails'
CREATE TABLE [dbo].[customerbankdetails] (
    [customer_bank_id] int IDENTITY(1,1) NOT NULL,
    [account_no] int  NOT NULL,
    [customerid] int  NULL,
    [benificary_name_in_bank] nvarchar(100)  NULL,
    [bank_name] nvarchar(100)  NULL,
    [branch_name] nvarchar(100)  NULL,
    [ifsc_code] nvarchar(100)  NULL,
    [benificary_name_in_bank_image] nvarchar(200)  NULL
);
GO

-- Creating table 'customerchaletaddresses'
CREATE TABLE [dbo].[customerchaletaddresses] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [chalet_street] nvarchar(100)  NULL,
    [chalet_number] nvarchar(100)  NULL,
    [chalet_address_status] bit  NULL
);
GO

-- Creating table 'customerfavoriteproducts'
CREATE TABLE [dbo].[customerfavoriteproducts] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customer_id] int  NULL,
    [product_id] int  NULL
);
GO

-- Creating table 'customerofficeaddresses'
CREATE TABLE [dbo].[customerofficeaddresses] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [office_street] nvarchar(100)  NULL,
    [office_building_name] nvarchar(100)  NULL,
    [offie_floor_number] nvarchar(100)  NULL,
    [office_number] nvarchar(100)  NULL,
    [office_address_status] bit  NULL
);
GO

-- Creating table 'customerotheraddresses'
CREATE TABLE [dbo].[customerotheraddresses] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [other_street] nvarchar(100)  NULL,
    [other_specificaqtion] nvarchar(100)  NULL,
    [other_address_status] bit  NULL
);
GO

-- Creating table 'customerpaymentcards'
CREATE TABLE [dbo].[customerpaymentcards] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [card_number] int  NULL,
    [card_expiry] datetime  NULL,
    [card_cvv] nvarchar(50)  NULL,
    [card_type] nvarchar(100)  NULL,
    [card_status] bit  NULL
);
GO

-- Creating table 'customervillaaddresses'
CREATE TABLE [dbo].[customervillaaddresses] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [villa_street] nvarchar(100)  NULL,
    [villa_name] nvarchar(100)  NULL,
    [villa_address_status] bit  NULL
);
GO

-- Creating table 'dgeofences'
CREATE TABLE [dbo].[dgeofences] (
    [geofenceid] int IDENTITY(1,1) NOT NULL,
    [location] geography  NULL,
    [zone_name] nvarchar(100)  NULL,
    [city_name] nvarchar(100)  NULL,
    [direction_name] nvarchar(100)  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'drivers'
CREATE TABLE [dbo].[drivers] (
    [driverid] int IDENTITY(1,1) NOT NULL,
    [emailid] nvarchar(100)  NOT NULL,
    [name] nvarchar(100)  NOT NULL,
    [dusername] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [mobile_no] nvarchar(15)  NOT NULL,
    [registration_date] datetime  NULL,
    [vender_id] int  NULL,
    [driver_nationality] nvarchar(50)  NULL,
    [gender] nvarchar(50)  NULL,
    [driver_phone_type] nvarchar(100)  NULL,
    [driver_divice_id] nvarchar(100)  NULL,
    [driver_telicom_carrer] nvarchar(100)  NULL,
    [creation_date] datetime  NULL,
    [verify_otp] int  NULL,
    [verify_email] nvarchar(150)  NULL,
    [createdby] int  NULL,
    [status] nvarchar(100)  NULL,
    [image] int  NULL
);
GO

-- Creating table 'driveraddressproffs'
CREATE TABLE [dbo].[driveraddressproffs] (
    [daddressid] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [address_proff] nvarchar(100)  NULL,
    [image] int  NULL,
    [address_image] nvarchar(150)  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'driverbankdetails'
CREATE TABLE [dbo].[driverbankdetails] (
    [driver_bank_id] int IDENTITY(1,1) NOT NULL,
    [accountno] int  NOT NULL,
    [driver_id] int  NULL,
    [benificary_name_in_bank] nvarchar(100)  NULL,
    [bank_name] nvarchar(50)  NULL,
    [branch_name] nvarchar(50)  NULL,
    [ifsc_code] nvarchar(50)  NULL,
    [benificary_name_in_bank_image] nvarchar(150)  NULL
);
GO

-- Creating table 'driverlogs'
CREATE TABLE [dbo].[driverlogs] (
    [dlogid] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [login_date] datetime  NULL,
    [logout] datetime  NULL,
    [online_time] int  NULL
);
GO

-- Creating table 'driverpayoutrequesteds'
CREATE TABLE [dbo].[driverpayoutrequesteds] (
    [dpayoutid] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [request_date] datetime  NULL,
    [tran_id] int  NULL,
    [amount] decimal(19,4)  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'driverratings'
CREATE TABLE [dbo].[driverratings] (
    [drateid] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [customer_id] int  NULL,
    [rating] int  NULL,
    [orderid] int  NULL,
    [rating_date] datetime  NULL,
    [comment] nvarchar(500)  NULL
);
GO

-- Creating table 'driverworkinghours'
CREATE TABLE [dbo].[driverworkinghours] (
    [id] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [working_hours_mondey] int  NULL,
    [working_hours_tuesday] int  NULL,
    [working_hours_wednesday] int  NULL,
    [working_hours_thursday] int  NULL,
    [working_hour_friday] int  NULL,
    [working_hour_saturday] int  NULL,
    [working_hour_sunday] int  NULL
);
GO

-- Creating table 'geofences'
CREATE TABLE [dbo].[geofences] (
    [id] int IDENTITY(1,1) NOT NULL,
    [geofence_id] int  NULL,
    [vender_id] int  NULL,
    [driver_id] int  NULL,
    [description] nvarchar(1000)  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'minimumorderstatus'
CREATE TABLE [dbo].[minimumorderstatus] (
    [min_order_id] int IDENTITY(1,1) NOT NULL,
    [vender_id] int  NULL,
    [order_type] nvarchar(200)  NULL,
    [order_quantity] int  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'orders'
CREATE TABLE [dbo].[orders] (
    [orderid] int IDENTITY(1,1) NOT NULL,
    [product_id] int  NULL,
    [customer_id] int  NULL,
    [ship_address] nvarchar(1000)  NULL,
    [price] decimal(19,4)  NULL,
    [quantity] int  NULL,
    [ship_date] datetime  NULL,
    [order_type] nvarchar(50)  NULL,
    [expected_delivery_time] datetime  NULL,
    [couponid] int  NULL,
    [discount] decimal(19,4)  NULL,
    [total] decimal(19,4)  NULL,
    [bill_address] nvarchar(1000)  NULL,
    [paid] bit  NULL,
    [orderdate] datetime  NULL,
    [payment_type] nvarchar(100)  NULL,
    [payment_date] datetime  NULL,
    [createdby] int  NULL,
    [status] nvarchar(100)  NULL
);
GO

-- Creating table 'oredercanclefeedbacks'
CREATE TABLE [dbo].[oredercanclefeedbacks] (
    [id] int IDENTITY(1,1) NOT NULL,
    [customer_id] int  NULL,
    [order_id] int  NULL,
    [feedback] nvarchar(500)  NULL
);
GO

-- Creating table 'pandetails'
CREATE TABLE [dbo].[pandetails] (
    [panid] int IDENTITY(1,1) NOT NULL,
    [pancardno] nvarchar(50)  NULL,
    [venderid] int  NULL,
    [image] int  NULL,
    [pan_image] nvarchar(150)  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'payoutalls'
CREATE TABLE [dbo].[payoutalls] (
    [dpayallid] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [transaction_date] datetime  NULL,
    [transaction_id] int  NULL,
    [payment_mode] nvarchar(50)  NULL,
    [amount] decimal(19,4)  NULL,
    [adjustment_amount] decimal(19,4)  NULL,
    [net_amount] decimal(19,4)  NULL
);
GO

-- Creating table 'products'
CREATE TABLE [dbo].[products] (
    [productid] int IDENTITY(1,1) NOT NULL,
    [vender_id] int  NULL,
    [product_title] nvarchar(100)  NOT NULL,
    [brand] nvarchar(100)  NULL,
    [description] nvarchar(1000)  NULL,
    [image] int  NULL,
    [category_id] int  NULL,
    [customer_price] decimal(19,4)  NULL,
    [customer_min_order_quantity] int  NULL,
    [customer_max_order_quantity] int  NULL,
    [store_price] decimal(19,4)  NULL,
    [store_min_order_quantity] int  NULL,
    [store_max_order_quantity] int  NULL,
    [phno] nvarchar(50)  NULL,
    [bottle_per_box] int  NULL,
    [stock] int  NULL,
    [sku] int  NULL,
    [createdby] int  NULL,
    [status] nvarchar(100)  NULL,
    [ProductAvaibility] nvarchar(250)  NULL,
    [volume] nvarchar(250)  NULL
);
GO

-- Creating table 'productrattings'
CREATE TABLE [dbo].[productrattings] (
    [rateid] int IDENTITY(1,1) NOT NULL,
    [product_id] int  NULL,
    [ratting] int  NULL,
    [comment] nvarchar(500)  NULL,
    [orderid] int  NULL,
    [ratting_date] datetime  NULL
);
GO

-- Creating table 'shippinghistories'
CREATE TABLE [dbo].[shippinghistories] (
    [shiphistoryid] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [order_id] int  NULL,
    [reached_time] datetime  NULL,
    [delivery_time] datetime  NULL,
    [wating_time] int  NULL,
    [delay_time] int  NULL
);
GO

-- Creating table 'storedetails'
CREATE TABLE [dbo].[storedetails] (
    [storeid] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [business_type] nvarchar(100)  NULL,
    [language] nvarchar(50)  NULL,
    [display_name] nvarchar(100)  NULL,
    [reg_business_name] nvarchar(100)  NULL,
    [business_address1] nvarchar(1000)  NULL,
    [business_address2] nvarchar(1000)  NULL,
    [city] nvarchar(100)  NULL,
    [state] nvarchar(100)  NULL,
    [warehouse_address1] nvarchar(500)  NULL,
    [warehouse_address2] nvarchar(500)  NULL,
    [Province] nvarchar(100)  NULL,
    [RegistrationName] nvarchar(100)  NULL,
    [WarehouseCity] nvarchar(100)  NULL,
    [Bussinessofficehours] nvarchar(100)  NULL,
    [WarehouseProvince] nvarchar(100)  NULL
);
GO

-- Creating table 'vattandetails'
CREATE TABLE [dbo].[vattandetails] (
    [vatid] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [image] int  NULL,
    [vat_image] nvarchar(150)  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'vechicles'
CREATE TABLE [dbo].[vechicles] (
    [id] int IDENTITY(1,1) NOT NULL,
    [driver_id] int  NULL,
    [vechicle_model] nvarchar(100)  NULL,
    [vechicle_company_name] nvarchar(100)  NULL,
    [vechicle_year] nvarchar(100)  NULL,
    [vechicle_energy_type] nvarchar(100)  NULL,
    [vechicle_cylender_type] int  NULL,
    [vechicle_fule_consumption_short] int  NULL,
    [vechicle_fule_consumption_long] int  NULL,
    [vechicle_assistant] int  NULL,
    [vechicle_max_capacity] int  NULL,
    [vechicle_status] bit  NULL
);
GO

-- Creating table 'venders'
CREATE TABLE [dbo].[venders] (
    [venderid] int IDENTITY(1,1) NOT NULL,
    [emailid] nvarchar(100)  NOT NULL,
    [name] nvarchar(100)  NOT NULL,
    [username] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [registration_date] datetime  NULL,
    [mobile_no] nvarchar(15)  NOT NULL,
    [verifyotp] int  NULL,
    [verifyemail] nvarchar(100)  NULL,
    [image] int  NULL,
    [createdby] int  NULL,
    [status] nvarchar(100)  NULL,
    [Activationid] uniqueidentifier  NULL,
    [Accepted] bit  NULL,
    [MerchantId] nvarchar(150)  NULL,
    [AdditionalInfor] nvarchar(200)  NULL,
    [ForgotActivationId] uniqueidentifier  NULL,
    [CompanyName] nvarchar(150)  NULL
);
GO

-- Creating table 'venderaddressproofs'
CREATE TABLE [dbo].[venderaddressproofs] (
    [vender_address_id] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [address_proof] nvarchar(100)  NULL,
    [CompanyTradeName] nvarchar(150)  NULL,
    [CompanyType] nvarchar(150)  NULL,
    [Nationality] nvarchar(150)  NULL,
    [MainOffice] nvarchar(150)  NULL,
    [PostalCode] nvarchar(150)  NULL,
    [TlelphoneNumber] nvarchar(150)  NULL,
    [RegistrationDate] nvarchar(150)  NULL,
    [POBox] nvarchar(150)  NULL,
    [CompanyType2] nvarchar(150)  NULL,
    [PanNumber] nvarchar(150)  NULL,
    [Address] nvarchar(150)  NULL,
    [City] nvarchar(150)  NULL,
    [State] nvarchar(150)  NULL,
    [image] int  NULL,
    [address_image] nvarchar(150)  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'venderbankdetails'
CREATE TABLE [dbo].[venderbankdetails] (
    [vender_bank_id] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [account_no] int  NOT NULL,
    [benificary_name_in_bank] nvarchar(100)  NULL,
    [bank_name] nvarchar(100)  NULL,
    [branch_name] nvarchar(100)  NULL,
    [ifsc_code] nvarchar(50)  NULL,
    [benificary_name_in_bank_image] nvarchar(150)  NULL,
    [HolderName] nvarchar(150)  NULL,
    [IBANCode] nvarchar(150)  NULL,
    [VatIdentityFicationNumber] nvarchar(150)  NULL,
    [BankNameCaseOther] nvarchar(150)  NULL
);
GO

-- Creating table 'venderlogs'
CREATE TABLE [dbo].[venderlogs] (
    [id] int IDENTITY(1,1) NOT NULL,
    [vender_id] int  NULL,
    [login_date] datetime  NULL,
    [logout] datetime  NULL,
    [online_time] int  NULL
);
GO

-- Creating table 'venderpayoutalls'
CREATE TABLE [dbo].[venderpayoutalls] (
    [vpayallid] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [transactiondate] datetime  NULL,
    [transactionid] int  NULL,
    [paymentmode] nvarchar(100)  NULL,
    [amount] decimal(19,4)  NULL,
    [adjustment_amount] decimal(19,4)  NULL,
    [net_amount] decimal(19,4)  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'venderpayoutrequesteds'
CREATE TABLE [dbo].[venderpayoutrequesteds] (
    [vpayoutid] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [requestdate] datetime  NULL,
    [transactionid] int  NULL,
    [amount] decimal(19,4)  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'shippingaddresses'
CREATE TABLE [dbo].[shippingaddresses] (
    [addressid] int IDENTITY(1,1) NOT NULL,
    [customerid] int  NULL,
    [streetaddress] nvarchar(500)  NULL,
    [city] nvarchar(500)  NULL,
    [zip] int  NULL,
    [state] nvarchar(100)  NULL,
    [country] nvarchar(100)  NULL,
    [orderid] int  NULL
);
GO

-- Creating table 'customerdefaultaddresses'
CREATE TABLE [dbo].[customerdefaultaddresses] (
    [addressid] int IDENTITY(1,1) NOT NULL,
    [custid] int  NULL,
    [custnote1] nvarchar(500)  NULL,
    [customernote2] nvarchar(500)  NULL,
    [streetaddress] nvarchar(500)  NULL,
    [city] nvarchar(100)  NULL,
    [zip] int  NULL,
    [state] nvarchar(100)  NULL,
    [country] nvarchar(150)  NULL
);
GO

-- Creating table 'driverdefaultaddresses'
CREATE TABLE [dbo].[driverdefaultaddresses] (
    [addressid] int IDENTITY(1,1) NOT NULL,
    [driverid] int  NULL,
    [drivernote1] nvarchar(500)  NULL,
    [drivernote2] nvarchar(500)  NULL,
    [streetaddress] nvarchar(500)  NULL,
    [city] nvarchar(100)  NULL,
    [zip] int  NULL,
    [state] nvarchar(100)  NULL,
    [country] nvarchar(150)  NULL
);
GO

-- Creating table 'venderdefaultaddresses'
CREATE TABLE [dbo].[venderdefaultaddresses] (
    [addressid] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [vendernote1] nvarchar(500)  NULL,
    [vendernote2] nvarchar(500)  NULL,
    [streetaddress] nvarchar(500)  NULL,
    [city] nvarchar(100)  NULL,
    [zip] int  NULL,
    [state] nvarchar(100)  NULL,
    [country] nvarchar(150)  NULL
);
GO

-- Creating table 'menuitems'
CREATE TABLE [dbo].[menuitems] (
    [menuid] int IDENTITY(1,1) NOT NULL,
    [menuname] nvarchar(100)  NULL
);
GO

-- Creating table 'roles'
CREATE TABLE [dbo].[roles] (
    [roleid] int IDENTITY(1,1) NOT NULL,
    [rolename] nvarchar(100)  NOT NULL,
    [createdby] int  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'teammanagements'
CREATE TABLE [dbo].[teammanagements] (
    [memberid] int IDENTITY(1,1) NOT NULL,
    [membername] nvarchar(100)  NULL,
    [firstname] nvarchar(100)  NULL,
    [lastname] nvarchar(100)  NULL,
    [email] nvarchar(100)  NULL,
    [password] nvarchar(100)  NULL,
    [roleid] int  NULL,
    [creadteby] int  NULL,
    [status] bit  NULL,
    [vender_id] int  NULL
);
GO

-- Creating table 'rolemanagements'
CREATE TABLE [dbo].[rolemanagements] (
    [id] int IDENTITY(1,1) NOT NULL,
    [roleid] int  NULL,
    [menuitem] nvarchar(100)  NULL,
    [createdby] int  NULL,
    [status] bit  NULL
);
GO

-- Creating table 'prodtransactions'
CREATE TABLE [dbo].[prodtransactions] (
    [transactionid] int IDENTITY(1,1) NOT NULL,
    [orderid] int  NULL,
    [transactiondate] datetime  NULL,
    [transactionmode] nvarchar(100)  NULL,
    [transactiontype] nvarchar(100)  NULL,
    [amount] decimal(19,4)  NULL,
    [adjamount] decimal(19,4)  NULL,
    [netamount] decimal(19,4)  NULL,
    [notes] nvarchar(500)  NULL
);
GO

-- Creating table 'coupons'
CREATE TABLE [dbo].[coupons] (
    [couponid] int IDENTITY(1,1) NOT NULL,
    [venderid] int  NULL,
    [coupon_code] nvarchar(100)  NULL,
    [coupon_valid_start_date] datetime  NULL,
    [coupon_valid_end_date] datetime  NULL,
    [coupon_status] bit  NULL,
    [discount] float  NULL,
    [coupon_name] nvarchar(100)  NULL,
    [coupon_type] int  NULL,
    [coupon_description] nvarchar(1000)  NULL
);
GO

-- Creating table 'agreements'
CREATE TABLE [dbo].[agreements] (
    [AgreementId] int IDENTITY(1,1) NOT NULL,
    [VenderId] int  NULL,
    [AgreementVersion] nvarchar(150)  NULL,
    [AcceptedDate] datetime  NULL,
    [FileId] int  NULL
);
GO

-- Creating table 'AverageCompositionPPMs'
CREATE TABLE [dbo].[AverageCompositionPPMs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'banks'
CREATE TABLE [dbo].[banks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BankName] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'BottleMaterials'
CREATE TABLE [dbo].[BottleMaterials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'BottlePerBoxes'
CREATE TABLE [dbo].[BottlePerBoxes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'BrandNames'
CREATE TABLE [dbo].[BrandNames] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'bussinessofficehours'
CREATE TABLE [dbo].[bussinessofficehours] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [bussinessoffice] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'bussinesstypes'
CREATE TABLE [dbo].[bussinesstypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [bussinesstypetext] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'CompanyTypes'
CREATE TABLE [dbo].[CompanyTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyTypeText] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'ConventionalstoreMinimumOrders'
CREATE TABLE [dbo].[ConventionalstoreMinimumOrders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'ConventionalstoreMiximumOrderQualities'
CREATE TABLE [dbo].[ConventionalstoreMiximumOrderQualities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'CREATETABLEBottlePerBoxes'
CREATE TABLE [dbo].[CREATETABLEBottlePerBoxes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'CustomerMaximumOrderQualities'
CREATE TABLE [dbo].[CustomerMaximumOrderQualities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'CustomerMinimumOrders'
CREATE TABLE [dbo].[CustomerMinimumOrders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'filestores'
CREATE TABLE [dbo].[filestores] (
    [FileId] int IDENTITY(1,1) NOT NULL,
    [fileName] nvarchar(250)  NULL,
    [fileSize] int  NULL,
    [attachment] varbinary(max)  NULL,
    [FileExtension] nvarchar(150)  NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageText] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'PHNumbers'
CREATE TABLE [dbo].[PHNumbers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'ProductAvailabilities'
CREATE TABLE [dbo].[ProductAvailabilities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'Provinces'
CREATE TABLE [dbo].[Provinces] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProvinceText] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Volumes'
CREATE TABLE [dbo].[Volumes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(250)  NULL
);
GO

-- Creating table 'ImageFiles'
CREATE TABLE [dbo].[ImageFiles] (
    [ImageId] int IDENTITY(1,1) NOT NULL,
    [ImageName] nvarchar(250)  NULL,
    [ImageSize] int  NULL,
    [Imageattachment] varbinary(max)  NULL
);
GO

-- Creating table 'customers'
CREATE TABLE [dbo].[customers] (
    [customerid] int IDENTITY(1,1) NOT NULL,
    [emailid] nvarchar(100)  NOT NULL,
    [name] nvarchar(100)  NOT NULL,
    [mobileno] nvarchar(15)  NULL,
    [verifyotp] int  NULL,
    [verifyemail] nvarchar(100)  NULL,
    [username] nvarchar(50)  NULL,
    [password] nvarchar(100)  NOT NULL,
    [regdate] datetime  NULL,
    [customer_gender] nvarchar(50)  NULL,
    [customer_phonetype] nvarchar(50)  NULL,
    [customer_nationality] nvarchar(50)  NULL,
    [customer_type] nvarchar(50)  NULL,
    [telicon_carrier] nvarchar(50)  NULL,
    [createdby] int  NULL,
    [custstatus] nvarchar(50)  NULL,
    [image] int  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'admins'
CREATE TABLE [dbo].[admins] (
    [adminid] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [email] nvarchar(100)  NOT NULL,
    [firstname] nvarchar(100)  NULL,
    [lastname] nvarchar(100)  NULL,
    [gender] nvarchar(50)  NULL,
    [website] nvarchar(100)  NULL,
    [regdate] datetime  NULL,
    [mobile] nvarchar(15)  NULL,
    [status] bit  NULL,
    [Image] int  NULL
);
GO

-- Creating table 'admin1'
CREATE TABLE [dbo].[admin1] (
    [adminid] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [email] nvarchar(100)  NOT NULL,
    [firstname] nvarchar(100)  NULL,
    [lastname] nvarchar(100)  NULL,
    [gender] nvarchar(50)  NULL,
    [website] nvarchar(100)  NULL,
    [regdate] datetime  NULL,
    [mobile] nvarchar(15)  NULL,
    [status] bit  NULL,
    [Image] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'advertisements'
ALTER TABLE [dbo].[advertisements]
ADD CONSTRAINT [PK_advertisements]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [categoryid] in table 'categories'
ALTER TABLE [dbo].[categories]
ADD CONSTRAINT [PK_categories]
    PRIMARY KEY CLUSTERED ([categoryid] ASC);
GO

-- Creating primary key on [addressid] in table 'customeraddressproofs'
ALTER TABLE [dbo].[customeraddressproofs]
ADD CONSTRAINT [PK_customeraddressproofs]
    PRIMARY KEY CLUSTERED ([addressid] ASC);
GO

-- Creating primary key on [id] in table 'customerappartmentaddresses'
ALTER TABLE [dbo].[customerappartmentaddresses]
ADD CONSTRAINT [PK_customerappartmentaddresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [customer_bank_id] in table 'customerbankdetails'
ALTER TABLE [dbo].[customerbankdetails]
ADD CONSTRAINT [PK_customerbankdetails]
    PRIMARY KEY CLUSTERED ([customer_bank_id] ASC);
GO

-- Creating primary key on [id] in table 'customerchaletaddresses'
ALTER TABLE [dbo].[customerchaletaddresses]
ADD CONSTRAINT [PK_customerchaletaddresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'customerfavoriteproducts'
ALTER TABLE [dbo].[customerfavoriteproducts]
ADD CONSTRAINT [PK_customerfavoriteproducts]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'customerofficeaddresses'
ALTER TABLE [dbo].[customerofficeaddresses]
ADD CONSTRAINT [PK_customerofficeaddresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'customerotheraddresses'
ALTER TABLE [dbo].[customerotheraddresses]
ADD CONSTRAINT [PK_customerotheraddresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'customerpaymentcards'
ALTER TABLE [dbo].[customerpaymentcards]
ADD CONSTRAINT [PK_customerpaymentcards]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'customervillaaddresses'
ALTER TABLE [dbo].[customervillaaddresses]
ADD CONSTRAINT [PK_customervillaaddresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [geofenceid] in table 'dgeofences'
ALTER TABLE [dbo].[dgeofences]
ADD CONSTRAINT [PK_dgeofences]
    PRIMARY KEY CLUSTERED ([geofenceid] ASC);
GO

-- Creating primary key on [driverid] in table 'drivers'
ALTER TABLE [dbo].[drivers]
ADD CONSTRAINT [PK_drivers]
    PRIMARY KEY CLUSTERED ([driverid] ASC);
GO

-- Creating primary key on [daddressid] in table 'driveraddressproffs'
ALTER TABLE [dbo].[driveraddressproffs]
ADD CONSTRAINT [PK_driveraddressproffs]
    PRIMARY KEY CLUSTERED ([daddressid] ASC);
GO

-- Creating primary key on [driver_bank_id] in table 'driverbankdetails'
ALTER TABLE [dbo].[driverbankdetails]
ADD CONSTRAINT [PK_driverbankdetails]
    PRIMARY KEY CLUSTERED ([driver_bank_id] ASC);
GO

-- Creating primary key on [dlogid] in table 'driverlogs'
ALTER TABLE [dbo].[driverlogs]
ADD CONSTRAINT [PK_driverlogs]
    PRIMARY KEY CLUSTERED ([dlogid] ASC);
GO

-- Creating primary key on [dpayoutid] in table 'driverpayoutrequesteds'
ALTER TABLE [dbo].[driverpayoutrequesteds]
ADD CONSTRAINT [PK_driverpayoutrequesteds]
    PRIMARY KEY CLUSTERED ([dpayoutid] ASC);
GO

-- Creating primary key on [drateid] in table 'driverratings'
ALTER TABLE [dbo].[driverratings]
ADD CONSTRAINT [PK_driverratings]
    PRIMARY KEY CLUSTERED ([drateid] ASC);
GO

-- Creating primary key on [id] in table 'driverworkinghours'
ALTER TABLE [dbo].[driverworkinghours]
ADD CONSTRAINT [PK_driverworkinghours]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'geofences'
ALTER TABLE [dbo].[geofences]
ADD CONSTRAINT [PK_geofences]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [min_order_id] in table 'minimumorderstatus'
ALTER TABLE [dbo].[minimumorderstatus]
ADD CONSTRAINT [PK_minimumorderstatus]
    PRIMARY KEY CLUSTERED ([min_order_id] ASC);
GO

-- Creating primary key on [orderid] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [PK_orders]
    PRIMARY KEY CLUSTERED ([orderid] ASC);
GO

-- Creating primary key on [id] in table 'oredercanclefeedbacks'
ALTER TABLE [dbo].[oredercanclefeedbacks]
ADD CONSTRAINT [PK_oredercanclefeedbacks]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [panid] in table 'pandetails'
ALTER TABLE [dbo].[pandetails]
ADD CONSTRAINT [PK_pandetails]
    PRIMARY KEY CLUSTERED ([panid] ASC);
GO

-- Creating primary key on [dpayallid] in table 'payoutalls'
ALTER TABLE [dbo].[payoutalls]
ADD CONSTRAINT [PK_payoutalls]
    PRIMARY KEY CLUSTERED ([dpayallid] ASC);
GO

-- Creating primary key on [productid] in table 'products'
ALTER TABLE [dbo].[products]
ADD CONSTRAINT [PK_products]
    PRIMARY KEY CLUSTERED ([productid] ASC);
GO

-- Creating primary key on [rateid] in table 'productrattings'
ALTER TABLE [dbo].[productrattings]
ADD CONSTRAINT [PK_productrattings]
    PRIMARY KEY CLUSTERED ([rateid] ASC);
GO

-- Creating primary key on [shiphistoryid] in table 'shippinghistories'
ALTER TABLE [dbo].[shippinghistories]
ADD CONSTRAINT [PK_shippinghistories]
    PRIMARY KEY CLUSTERED ([shiphistoryid] ASC);
GO

-- Creating primary key on [storeid] in table 'storedetails'
ALTER TABLE [dbo].[storedetails]
ADD CONSTRAINT [PK_storedetails]
    PRIMARY KEY CLUSTERED ([storeid] ASC);
GO

-- Creating primary key on [vatid] in table 'vattandetails'
ALTER TABLE [dbo].[vattandetails]
ADD CONSTRAINT [PK_vattandetails]
    PRIMARY KEY CLUSTERED ([vatid] ASC);
GO

-- Creating primary key on [id] in table 'vechicles'
ALTER TABLE [dbo].[vechicles]
ADD CONSTRAINT [PK_vechicles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [venderid] in table 'venders'
ALTER TABLE [dbo].[venders]
ADD CONSTRAINT [PK_venders]
    PRIMARY KEY CLUSTERED ([venderid] ASC);
GO

-- Creating primary key on [vender_address_id] in table 'venderaddressproofs'
ALTER TABLE [dbo].[venderaddressproofs]
ADD CONSTRAINT [PK_venderaddressproofs]
    PRIMARY KEY CLUSTERED ([vender_address_id] ASC);
GO

-- Creating primary key on [vender_bank_id] in table 'venderbankdetails'
ALTER TABLE [dbo].[venderbankdetails]
ADD CONSTRAINT [PK_venderbankdetails]
    PRIMARY KEY CLUSTERED ([vender_bank_id] ASC);
GO

-- Creating primary key on [id] in table 'venderlogs'
ALTER TABLE [dbo].[venderlogs]
ADD CONSTRAINT [PK_venderlogs]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [vpayallid] in table 'venderpayoutalls'
ALTER TABLE [dbo].[venderpayoutalls]
ADD CONSTRAINT [PK_venderpayoutalls]
    PRIMARY KEY CLUSTERED ([vpayallid] ASC);
GO

-- Creating primary key on [vpayoutid] in table 'venderpayoutrequesteds'
ALTER TABLE [dbo].[venderpayoutrequesteds]
ADD CONSTRAINT [PK_venderpayoutrequesteds]
    PRIMARY KEY CLUSTERED ([vpayoutid] ASC);
GO

-- Creating primary key on [addressid] in table 'shippingaddresses'
ALTER TABLE [dbo].[shippingaddresses]
ADD CONSTRAINT [PK_shippingaddresses]
    PRIMARY KEY CLUSTERED ([addressid] ASC);
GO

-- Creating primary key on [addressid] in table 'customerdefaultaddresses'
ALTER TABLE [dbo].[customerdefaultaddresses]
ADD CONSTRAINT [PK_customerdefaultaddresses]
    PRIMARY KEY CLUSTERED ([addressid] ASC);
GO

-- Creating primary key on [addressid] in table 'driverdefaultaddresses'
ALTER TABLE [dbo].[driverdefaultaddresses]
ADD CONSTRAINT [PK_driverdefaultaddresses]
    PRIMARY KEY CLUSTERED ([addressid] ASC);
GO

-- Creating primary key on [addressid] in table 'venderdefaultaddresses'
ALTER TABLE [dbo].[venderdefaultaddresses]
ADD CONSTRAINT [PK_venderdefaultaddresses]
    PRIMARY KEY CLUSTERED ([addressid] ASC);
GO

-- Creating primary key on [menuid] in table 'menuitems'
ALTER TABLE [dbo].[menuitems]
ADD CONSTRAINT [PK_menuitems]
    PRIMARY KEY CLUSTERED ([menuid] ASC);
GO

-- Creating primary key on [roleid] in table 'roles'
ALTER TABLE [dbo].[roles]
ADD CONSTRAINT [PK_roles]
    PRIMARY KEY CLUSTERED ([roleid] ASC);
GO

-- Creating primary key on [memberid] in table 'teammanagements'
ALTER TABLE [dbo].[teammanagements]
ADD CONSTRAINT [PK_teammanagements]
    PRIMARY KEY CLUSTERED ([memberid] ASC);
GO

-- Creating primary key on [id] in table 'rolemanagements'
ALTER TABLE [dbo].[rolemanagements]
ADD CONSTRAINT [PK_rolemanagements]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [transactionid] in table 'prodtransactions'
ALTER TABLE [dbo].[prodtransactions]
ADD CONSTRAINT [PK_prodtransactions]
    PRIMARY KEY CLUSTERED ([transactionid] ASC);
GO

-- Creating primary key on [couponid] in table 'coupons'
ALTER TABLE [dbo].[coupons]
ADD CONSTRAINT [PK_coupons]
    PRIMARY KEY CLUSTERED ([couponid] ASC);
GO

-- Creating primary key on [AgreementId] in table 'agreements'
ALTER TABLE [dbo].[agreements]
ADD CONSTRAINT [PK_agreements]
    PRIMARY KEY CLUSTERED ([AgreementId] ASC);
GO

-- Creating primary key on [Id] in table 'AverageCompositionPPMs'
ALTER TABLE [dbo].[AverageCompositionPPMs]
ADD CONSTRAINT [PK_AverageCompositionPPMs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'banks'
ALTER TABLE [dbo].[banks]
ADD CONSTRAINT [PK_banks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BottleMaterials'
ALTER TABLE [dbo].[BottleMaterials]
ADD CONSTRAINT [PK_BottleMaterials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BottlePerBoxes'
ALTER TABLE [dbo].[BottlePerBoxes]
ADD CONSTRAINT [PK_BottlePerBoxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BrandNames'
ALTER TABLE [dbo].[BrandNames]
ADD CONSTRAINT [PK_BrandNames]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'bussinessofficehours'
ALTER TABLE [dbo].[bussinessofficehours]
ADD CONSTRAINT [PK_bussinessofficehours]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'bussinesstypes'
ALTER TABLE [dbo].[bussinesstypes]
ADD CONSTRAINT [PK_bussinesstypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompanyTypes'
ALTER TABLE [dbo].[CompanyTypes]
ADD CONSTRAINT [PK_CompanyTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConventionalstoreMinimumOrders'
ALTER TABLE [dbo].[ConventionalstoreMinimumOrders]
ADD CONSTRAINT [PK_ConventionalstoreMinimumOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConventionalstoreMiximumOrderQualities'
ALTER TABLE [dbo].[ConventionalstoreMiximumOrderQualities]
ADD CONSTRAINT [PK_ConventionalstoreMiximumOrderQualities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CREATETABLEBottlePerBoxes'
ALTER TABLE [dbo].[CREATETABLEBottlePerBoxes]
ADD CONSTRAINT [PK_CREATETABLEBottlePerBoxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerMaximumOrderQualities'
ALTER TABLE [dbo].[CustomerMaximumOrderQualities]
ADD CONSTRAINT [PK_CustomerMaximumOrderQualities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerMinimumOrders'
ALTER TABLE [dbo].[CustomerMinimumOrders]
ADD CONSTRAINT [PK_CustomerMinimumOrders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [FileId] in table 'filestores'
ALTER TABLE [dbo].[filestores]
ADD CONSTRAINT [PK_filestores]
    PRIMARY KEY CLUSTERED ([FileId] ASC);
GO

-- Creating primary key on [Id] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PHNumbers'
ALTER TABLE [dbo].[PHNumbers]
ADD CONSTRAINT [PK_PHNumbers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductAvailabilities'
ALTER TABLE [dbo].[ProductAvailabilities]
ADD CONSTRAINT [PK_ProductAvailabilities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Provinces'
ALTER TABLE [dbo].[Provinces]
ADD CONSTRAINT [PK_Provinces]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Volumes'
ALTER TABLE [dbo].[Volumes]
ADD CONSTRAINT [PK_Volumes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ImageId] in table 'ImageFiles'
ALTER TABLE [dbo].[ImageFiles]
ADD CONSTRAINT [PK_ImageFiles]
    PRIMARY KEY CLUSTERED ([ImageId] ASC);
GO

-- Creating primary key on [customerid] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [PK_customers]
    PRIMARY KEY CLUSTERED ([customerid] ASC);
GO

-- Creating primary key on [adminid] in table 'admins'
ALTER TABLE [dbo].[admins]
ADD CONSTRAINT [PK_admins]
    PRIMARY KEY CLUSTERED ([adminid] ASC);
GO

-- Creating primary key on [adminid] in table 'admin1'
ALTER TABLE [dbo].[admin1]
ADD CONSTRAINT [PK_admin1]
    PRIMARY KEY CLUSTERED ([adminid] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [vender_id] in table 'advertisements'
ALTER TABLE [dbo].[advertisements]
ADD CONSTRAINT [FK__advertise__vende__7D439ABD]
    FOREIGN KEY ([vender_id])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__advertise__vende__7D439ABD'
CREATE INDEX [IX_FK__advertise__vende__7D439ABD]
ON [dbo].[advertisements]
    ([vender_id]);
GO

-- Creating foreign key on [category_id] in table 'products'
ALTER TABLE [dbo].[products]
ADD CONSTRAINT [FK__product__categor__6383C8BA]
    FOREIGN KEY ([category_id])
    REFERENCES [dbo].[categories]
        ([categoryid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__product__categor__6383C8BA'
CREATE INDEX [IX_FK__product__categor__6383C8BA]
ON [dbo].[products]
    ([category_id]);
GO

-- Creating foreign key on [product_id] in table 'customerfavoriteproducts'
ALTER TABLE [dbo].[customerfavoriteproducts]
ADD CONSTRAINT [FK__customerf__produ__0F624AF8]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[products]
        ([productid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customerf__produ__0F624AF8'
CREATE INDEX [IX_FK__customerf__produ__0F624AF8]
ON [dbo].[customerfavoriteproducts]
    ([product_id]);
GO

-- Creating foreign key on [geofence_id] in table 'geofences'
ALTER TABLE [dbo].[geofences]
ADD CONSTRAINT [FK__geofence__geofen__04E4BC85]
    FOREIGN KEY ([geofence_id])
    REFERENCES [dbo].[dgeofences]
        ([geofenceid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__geofence__geofen__04E4BC85'
CREATE INDEX [IX_FK__geofence__geofen__04E4BC85]
ON [dbo].[geofences]
    ([geofence_id]);
GO

-- Creating foreign key on [vender_id] in table 'drivers'
ALTER TABLE [dbo].[drivers]
ADD CONSTRAINT [FK__driver__vender_i__49C3F6B7]
    FOREIGN KEY ([vender_id])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driver__vender_i__49C3F6B7'
CREATE INDEX [IX_FK__driver__vender_i__49C3F6B7]
ON [dbo].[drivers]
    ([vender_id]);
GO

-- Creating foreign key on [driver_id] in table 'driveraddressproffs'
ALTER TABLE [dbo].[driveraddressproffs]
ADD CONSTRAINT [FK__driveradd__drive__52593CB8]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driveradd__drive__52593CB8'
CREATE INDEX [IX_FK__driveradd__drive__52593CB8]
ON [dbo].[driveraddressproffs]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'driverbankdetails'
ALTER TABLE [dbo].[driverbankdetails]
ADD CONSTRAINT [FK__driverban__drive__4CA06362]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverban__drive__4CA06362'
CREATE INDEX [IX_FK__driverban__drive__4CA06362]
ON [dbo].[driverbankdetails]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'driverlogs'
ALTER TABLE [dbo].[driverlogs]
ADD CONSTRAINT [FK__driverlog__drive__5535A963]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverlog__drive__5535A963'
CREATE INDEX [IX_FK__driverlog__drive__5535A963]
ON [dbo].[driverlogs]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'driverpayoutrequesteds'
ALTER TABLE [dbo].[driverpayoutrequesteds]
ADD CONSTRAINT [FK__driverpay__drive__5AEE82B9]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverpay__drive__5AEE82B9'
CREATE INDEX [IX_FK__driverpay__drive__5AEE82B9]
ON [dbo].[driverpayoutrequesteds]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'driverratings'
ALTER TABLE [dbo].[driverratings]
ADD CONSTRAINT [FK__driverrat__drive__6A30C649]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverrat__drive__6A30C649'
CREATE INDEX [IX_FK__driverrat__drive__6A30C649]
ON [dbo].[driverratings]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'driverworkinghours'
ALTER TABLE [dbo].[driverworkinghours]
ADD CONSTRAINT [FK__driverwor__drive__5812160E]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverwor__drive__5812160E'
CREATE INDEX [IX_FK__driverwor__drive__5812160E]
ON [dbo].[driverworkinghours]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'geofences'
ALTER TABLE [dbo].[geofences]
ADD CONSTRAINT [FK__geofence__driver__06CD04F7]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__geofence__driver__06CD04F7'
CREATE INDEX [IX_FK__geofence__driver__06CD04F7]
ON [dbo].[geofences]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'payoutalls'
ALTER TABLE [dbo].[payoutalls]
ADD CONSTRAINT [FK__payoutall__drive__4F7CD00D]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__payoutall__drive__4F7CD00D'
CREATE INDEX [IX_FK__payoutall__drive__4F7CD00D]
ON [dbo].[payoutalls]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'shippinghistories'
ALTER TABLE [dbo].[shippinghistories]
ADD CONSTRAINT [FK__shippingh__drive__0A9D95DB]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__shippingh__drive__0A9D95DB'
CREATE INDEX [IX_FK__shippingh__drive__0A9D95DB]
ON [dbo].[shippinghistories]
    ([driver_id]);
GO

-- Creating foreign key on [driver_id] in table 'vechicles'
ALTER TABLE [dbo].[vechicles]
ADD CONSTRAINT [FK__vechicle__driver__5DCAEF64]
    FOREIGN KEY ([driver_id])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__vechicle__driver__5DCAEF64'
CREATE INDEX [IX_FK__vechicle__driver__5DCAEF64]
ON [dbo].[vechicles]
    ([driver_id]);
GO

-- Creating foreign key on [orderid] in table 'driverratings'
ALTER TABLE [dbo].[driverratings]
ADD CONSTRAINT [FK__driverrat__order__6C190EBB]
    FOREIGN KEY ([orderid])
    REFERENCES [dbo].[orders]
        ([orderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverrat__order__6C190EBB'
CREATE INDEX [IX_FK__driverrat__order__6C190EBB]
ON [dbo].[driverratings]
    ([orderid]);
GO

-- Creating foreign key on [vender_id] in table 'geofences'
ALTER TABLE [dbo].[geofences]
ADD CONSTRAINT [FK__geofence__vender__05D8E0BE]
    FOREIGN KEY ([vender_id])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__geofence__vender__05D8E0BE'
CREATE INDEX [IX_FK__geofence__vender__05D8E0BE]
ON [dbo].[geofences]
    ([vender_id]);
GO

-- Creating foreign key on [vender_id] in table 'minimumorderstatus'
ALTER TABLE [dbo].[minimumorderstatus]
ADD CONSTRAINT [FK__minimumor__vende__46E78A0C]
    FOREIGN KEY ([vender_id])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__minimumor__vende__46E78A0C'
CREATE INDEX [IX_FK__minimumor__vende__46E78A0C]
ON [dbo].[minimumorderstatus]
    ([vender_id]);
GO

-- Creating foreign key on [product_id] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [FK__orders__product___66603565]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[products]
        ([productid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__orders__product___66603565'
CREATE INDEX [IX_FK__orders__product___66603565]
ON [dbo].[orders]
    ([product_id]);
GO

-- Creating foreign key on [order_id] in table 'oredercanclefeedbacks'
ALTER TABLE [dbo].[oredercanclefeedbacks]
ADD CONSTRAINT [FK__oredercan__order__6FE99F9F]
    FOREIGN KEY ([order_id])
    REFERENCES [dbo].[orders]
        ([orderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__oredercan__order__6FE99F9F'
CREATE INDEX [IX_FK__oredercan__order__6FE99F9F]
ON [dbo].[oredercanclefeedbacks]
    ([order_id]);
GO

-- Creating foreign key on [orderid] in table 'productrattings'
ALTER TABLE [dbo].[productrattings]
ADD CONSTRAINT [FK__productra__order__73BA3083]
    FOREIGN KEY ([orderid])
    REFERENCES [dbo].[orders]
        ([orderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__productra__order__73BA3083'
CREATE INDEX [IX_FK__productra__order__73BA3083]
ON [dbo].[productrattings]
    ([orderid]);
GO

-- Creating foreign key on [order_id] in table 'shippinghistories'
ALTER TABLE [dbo].[shippinghistories]
ADD CONSTRAINT [FK__shippingh__order__0B91BA14]
    FOREIGN KEY ([order_id])
    REFERENCES [dbo].[orders]
        ([orderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__shippingh__order__0B91BA14'
CREATE INDEX [IX_FK__shippingh__order__0B91BA14]
ON [dbo].[shippinghistories]
    ([order_id]);
GO

-- Creating foreign key on [venderid] in table 'pandetails'
ALTER TABLE [dbo].[pandetails]
ADD CONSTRAINT [FK__pandetail__vende__300424B4]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__pandetail__vende__300424B4'
CREATE INDEX [IX_FK__pandetail__vende__300424B4]
ON [dbo].[pandetails]
    ([venderid]);
GO

-- Creating foreign key on [vender_id] in table 'products'
ALTER TABLE [dbo].[products]
ADD CONSTRAINT [FK__product__vender___628FA481]
    FOREIGN KEY ([vender_id])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__product__vender___628FA481'
CREATE INDEX [IX_FK__product__vender___628FA481]
ON [dbo].[products]
    ([vender_id]);
GO

-- Creating foreign key on [product_id] in table 'productrattings'
ALTER TABLE [dbo].[productrattings]
ADD CONSTRAINT [FK__productra__produ__72C60C4A]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[products]
        ([productid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__productra__produ__72C60C4A'
CREATE INDEX [IX_FK__productra__produ__72C60C4A]
ON [dbo].[productrattings]
    ([product_id]);
GO

-- Creating foreign key on [venderid] in table 'storedetails'
ALTER TABLE [dbo].[storedetails]
ADD CONSTRAINT [FK__storedeta__vende__3B75D760]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__storedeta__vende__3B75D760'
CREATE INDEX [IX_FK__storedeta__vende__3B75D760]
ON [dbo].[storedetails]
    ([venderid]);
GO

-- Creating foreign key on [venderid] in table 'vattandetails'
ALTER TABLE [dbo].[vattandetails]
ADD CONSTRAINT [FK__vattandet__vende__32E0915F]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__vattandet__vende__32E0915F'
CREATE INDEX [IX_FK__vattandet__vende__32E0915F]
ON [dbo].[vattandetails]
    ([venderid]);
GO

-- Creating foreign key on [venderid] in table 'venderaddressproofs'
ALTER TABLE [dbo].[venderaddressproofs]
ADD CONSTRAINT [FK__venderadd__vende__412EB0B6]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderadd__vende__412EB0B6'
CREATE INDEX [IX_FK__venderadd__vende__412EB0B6]
ON [dbo].[venderaddressproofs]
    ([venderid]);
GO

-- Creating foreign key on [venderid] in table 'venderbankdetails'
ALTER TABLE [dbo].[venderbankdetails]
ADD CONSTRAINT [FK__venderban__vende__3E52440B]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderban__vende__3E52440B'
CREATE INDEX [IX_FK__venderban__vende__3E52440B]
ON [dbo].[venderbankdetails]
    ([venderid]);
GO

-- Creating foreign key on [vender_id] in table 'venderlogs'
ALTER TABLE [dbo].[venderlogs]
ADD CONSTRAINT [FK__venderlog__vende__440B1D61]
    FOREIGN KEY ([vender_id])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderlog__vende__440B1D61'
CREATE INDEX [IX_FK__venderlog__vende__440B1D61]
ON [dbo].[venderlogs]
    ([vender_id]);
GO

-- Creating foreign key on [venderid] in table 'venderpayoutalls'
ALTER TABLE [dbo].[venderpayoutalls]
ADD CONSTRAINT [FK__venderpay__vende__35BCFE0A]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderpay__vende__35BCFE0A'
CREATE INDEX [IX_FK__venderpay__vende__35BCFE0A]
ON [dbo].[venderpayoutalls]
    ([venderid]);
GO

-- Creating foreign key on [venderid] in table 'venderpayoutrequesteds'
ALTER TABLE [dbo].[venderpayoutrequesteds]
ADD CONSTRAINT [FK__venderpay__vende__38996AB5]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderpay__vende__38996AB5'
CREATE INDEX [IX_FK__venderpay__vende__38996AB5]
ON [dbo].[venderpayoutrequesteds]
    ([venderid]);
GO

-- Creating foreign key on [orderid] in table 'shippingaddresses'
ALTER TABLE [dbo].[shippingaddresses]
ADD CONSTRAINT [FK__shippinga__order__2A164134]
    FOREIGN KEY ([orderid])
    REFERENCES [dbo].[orders]
        ([orderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__shippinga__order__2A164134'
CREATE INDEX [IX_FK__shippinga__order__2A164134]
ON [dbo].[shippingaddresses]
    ([orderid]);
GO

-- Creating foreign key on [driverid] in table 'driverdefaultaddresses'
ALTER TABLE [dbo].[driverdefaultaddresses]
ADD CONSTRAINT [FK__driverdef__drive__41EDCAC5]
    FOREIGN KEY ([driverid])
    REFERENCES [dbo].[drivers]
        ([driverid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverdef__drive__41EDCAC5'
CREATE INDEX [IX_FK__driverdef__drive__41EDCAC5]
ON [dbo].[driverdefaultaddresses]
    ([driverid]);
GO

-- Creating foreign key on [venderid] in table 'venderdefaultaddresses'
ALTER TABLE [dbo].[venderdefaultaddresses]
ADD CONSTRAINT [FK__venderdef__vende__3F115E1A]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderdef__vende__3F115E1A'
CREATE INDEX [IX_FK__venderdef__vende__3F115E1A]
ON [dbo].[venderdefaultaddresses]
    ([venderid]);
GO

-- Creating foreign key on [roleid] in table 'teammanagements'
ALTER TABLE [dbo].[teammanagements]
ADD CONSTRAINT [FK__teammanag__rolei__4A8310C6]
    FOREIGN KEY ([roleid])
    REFERENCES [dbo].[roles]
        ([roleid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__teammanag__rolei__4A8310C6'
CREATE INDEX [IX_FK__teammanag__rolei__4A8310C6]
ON [dbo].[teammanagements]
    ([roleid]);
GO

-- Creating foreign key on [roleid] in table 'rolemanagements'
ALTER TABLE [dbo].[rolemanagements]
ADD CONSTRAINT [FK__rolemanag__rolei__4D5F7D71]
    FOREIGN KEY ([roleid])
    REFERENCES [dbo].[roles]
        ([roleid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__rolemanag__rolei__4D5F7D71'
CREATE INDEX [IX_FK__rolemanag__rolei__4D5F7D71]
ON [dbo].[rolemanagements]
    ([roleid]);
GO

-- Creating foreign key on [orderid] in table 'prodtransactions'
ALTER TABLE [dbo].[prodtransactions]
ADD CONSTRAINT [FK__prodtrans__order__503BEA1C]
    FOREIGN KEY ([orderid])
    REFERENCES [dbo].[orders]
        ([orderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__prodtrans__order__503BEA1C'
CREATE INDEX [IX_FK__prodtrans__order__503BEA1C]
ON [dbo].[prodtransactions]
    ([orderid]);
GO

-- Creating foreign key on [venderid] in table 'coupons'
ALTER TABLE [dbo].[coupons]
ADD CONSTRAINT [FK__coupon__venderid__73852659]
    FOREIGN KEY ([venderid])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__coupon__venderid__73852659'
CREATE INDEX [IX_FK__coupon__venderid__73852659]
ON [dbo].[coupons]
    ([venderid]);
GO

-- Creating foreign key on [FileId] in table 'agreements'
ALTER TABLE [dbo].[agreements]
ADD CONSTRAINT [FK__agreement__FileI__04AFB25B]
    FOREIGN KEY ([FileId])
    REFERENCES [dbo].[filestores]
        ([FileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__agreement__FileI__04AFB25B'
CREATE INDEX [IX_FK__agreement__FileI__04AFB25B]
ON [dbo].[agreements]
    ([FileId]);
GO

-- Creating foreign key on [VenderId] in table 'agreements'
ALTER TABLE [dbo].[agreements]
ADD CONSTRAINT [FK__agreement__Vende__03BB8E22]
    FOREIGN KEY ([VenderId])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__agreement__Vende__03BB8E22'
CREATE INDEX [IX_FK__agreement__Vende__03BB8E22]
ON [dbo].[agreements]
    ([VenderId]);
GO

-- Creating foreign key on [image] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [FK__customer__image__1E6F845E]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customer__image__1E6F845E'
CREATE INDEX [IX_FK__customer__image__1E6F845E]
ON [dbo].[customers]
    ([image]);
GO

-- Creating foreign key on [customerid] in table 'customeraddressproofs'
ALTER TABLE [dbo].[customeraddressproofs]
ADD CONSTRAINT [FK__customera__custo__1CF15040]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customera__custo__1CF15040'
CREATE INDEX [IX_FK__customera__custo__1CF15040]
ON [dbo].[customeraddressproofs]
    ([customerid]);
GO

-- Creating foreign key on [customerid] in table 'customerappartmentaddresses'
ALTER TABLE [dbo].[customerappartmentaddresses]
ADD CONSTRAINT [FK__customera__custo__22AA2996]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customera__custo__22AA2996'
CREATE INDEX [IX_FK__customera__custo__22AA2996]
ON [dbo].[customerappartmentaddresses]
    ([customerid]);
GO

-- Creating foreign key on [customerid] in table 'customerbankdetails'
ALTER TABLE [dbo].[customerbankdetails]
ADD CONSTRAINT [FK__customerb__custo__173876EA]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customerb__custo__173876EA'
CREATE INDEX [IX_FK__customerb__custo__173876EA]
ON [dbo].[customerbankdetails]
    ([customerid]);
GO

-- Creating foreign key on [customerid] in table 'customerchaletaddresses'
ALTER TABLE [dbo].[customerchaletaddresses]
ADD CONSTRAINT [FK__customerc__custo__286302EC]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customerc__custo__286302EC'
CREATE INDEX [IX_FK__customerc__custo__286302EC]
ON [dbo].[customerchaletaddresses]
    ([customerid]);
GO

-- Creating foreign key on [custid] in table 'customerdefaultaddresses'
ALTER TABLE [dbo].[customerdefaultaddresses]
ADD CONSTRAINT [FK__customerd__custi__3C34F16F]
    FOREIGN KEY ([custid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customerd__custi__3C34F16F'
CREATE INDEX [IX_FK__customerd__custi__3C34F16F]
ON [dbo].[customerdefaultaddresses]
    ([custid]);
GO

-- Creating foreign key on [customer_id] in table 'customerfavoriteproducts'
ALTER TABLE [dbo].[customerfavoriteproducts]
ADD CONSTRAINT [FK__customerf__custo__0E6E26BF]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customerf__custo__0E6E26BF'
CREATE INDEX [IX_FK__customerf__custo__0E6E26BF]
ON [dbo].[customerfavoriteproducts]
    ([customer_id]);
GO

-- Creating foreign key on [customerid] in table 'customerofficeaddresses'
ALTER TABLE [dbo].[customerofficeaddresses]
ADD CONSTRAINT [FK__customero__custo__25869641]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customero__custo__25869641'
CREATE INDEX [IX_FK__customero__custo__25869641]
ON [dbo].[customerofficeaddresses]
    ([customerid]);
GO

-- Creating foreign key on [customerid] in table 'customerotheraddresses'
ALTER TABLE [dbo].[customerotheraddresses]
ADD CONSTRAINT [FK__customero__custo__2B3F6F97]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customero__custo__2B3F6F97'
CREATE INDEX [IX_FK__customero__custo__2B3F6F97]
ON [dbo].[customerotheraddresses]
    ([customerid]);
GO

-- Creating foreign key on [customerid] in table 'customerpaymentcards'
ALTER TABLE [dbo].[customerpaymentcards]
ADD CONSTRAINT [FK__customerp__custo__1A14E395]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customerp__custo__1A14E395'
CREATE INDEX [IX_FK__customerp__custo__1A14E395]
ON [dbo].[customerpaymentcards]
    ([customerid]);
GO

-- Creating foreign key on [customerid] in table 'customervillaaddresses'
ALTER TABLE [dbo].[customervillaaddresses]
ADD CONSTRAINT [FK__customerv__custo__1FCDBCEB]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customerv__custo__1FCDBCEB'
CREATE INDEX [IX_FK__customerv__custo__1FCDBCEB]
ON [dbo].[customervillaaddresses]
    ([customerid]);
GO

-- Creating foreign key on [customer_id] in table 'driverratings'
ALTER TABLE [dbo].[driverratings]
ADD CONSTRAINT [FK__driverrat__custo__6B24EA82]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driverrat__custo__6B24EA82'
CREATE INDEX [IX_FK__driverrat__custo__6B24EA82]
ON [dbo].[driverratings]
    ([customer_id]);
GO

-- Creating foreign key on [customer_id] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [FK__orders__customer__6754599E]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__orders__customer__6754599E'
CREATE INDEX [IX_FK__orders__customer__6754599E]
ON [dbo].[orders]
    ([customer_id]);
GO

-- Creating foreign key on [customer_id] in table 'oredercanclefeedbacks'
ALTER TABLE [dbo].[oredercanclefeedbacks]
ADD CONSTRAINT [FK__oredercan__custo__6EF57B66]
    FOREIGN KEY ([customer_id])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__oredercan__custo__6EF57B66'
CREATE INDEX [IX_FK__oredercan__custo__6EF57B66]
ON [dbo].[oredercanclefeedbacks]
    ([customer_id]);
GO

-- Creating foreign key on [customerid] in table 'shippingaddresses'
ALTER TABLE [dbo].[shippingaddresses]
ADD CONSTRAINT [FK__shippinga__custo__29221CFB]
    FOREIGN KEY ([customerid])
    REFERENCES [dbo].[customers]
        ([customerid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__shippinga__custo__29221CFB'
CREATE INDEX [IX_FK__shippinga__custo__29221CFB]
ON [dbo].[shippingaddresses]
    ([customerid]);
GO

-- Creating foreign key on [image] in table 'categories'
ALTER TABLE [dbo].[categories]
ADD CONSTRAINT [FK__category__image__2057CCD0]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__category__image__2057CCD0'
CREATE INDEX [IX_FK__category__image__2057CCD0]
ON [dbo].[categories]
    ([image]);
GO

-- Creating foreign key on [image] in table 'customeraddressproofs'
ALTER TABLE [dbo].[customeraddressproofs]
ADD CONSTRAINT [FK__customera__image__214BF109]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customera__image__214BF109'
CREATE INDEX [IX_FK__customera__image__214BF109]
ON [dbo].[customeraddressproofs]
    ([image]);
GO

-- Creating foreign key on [image] in table 'drivers'
ALTER TABLE [dbo].[drivers]
ADD CONSTRAINT [FK__driver__image__22401542]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driver__image__22401542'
CREATE INDEX [IX_FK__driver__image__22401542]
ON [dbo].[drivers]
    ([image]);
GO

-- Creating foreign key on [image] in table 'driveraddressproffs'
ALTER TABLE [dbo].[driveraddressproffs]
ADD CONSTRAINT [FK__driveradd__image__2334397B]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driveradd__image__2334397B'
CREATE INDEX [IX_FK__driveradd__image__2334397B]
ON [dbo].[driveraddressproffs]
    ([image]);
GO

-- Creating foreign key on [image] in table 'pandetails'
ALTER TABLE [dbo].[pandetails]
ADD CONSTRAINT [FK__pandetail__image__24285DB4]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__pandetail__image__24285DB4'
CREATE INDEX [IX_FK__pandetail__image__24285DB4]
ON [dbo].[pandetails]
    ([image]);
GO

-- Creating foreign key on [image] in table 'products'
ALTER TABLE [dbo].[products]
ADD CONSTRAINT [FK__product__image__251C81ED]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__product__image__251C81ED'
CREATE INDEX [IX_FK__product__image__251C81ED]
ON [dbo].[products]
    ([image]);
GO

-- Creating foreign key on [image] in table 'vattandetails'
ALTER TABLE [dbo].[vattandetails]
ADD CONSTRAINT [FK__vattandet__image__2610A626]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__vattandet__image__2610A626'
CREATE INDEX [IX_FK__vattandet__image__2610A626]
ON [dbo].[vattandetails]
    ([image]);
GO

-- Creating foreign key on [image] in table 'venders'
ALTER TABLE [dbo].[venders]
ADD CONSTRAINT [FK__vender__image__2704CA5F]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__vender__image__2704CA5F'
CREATE INDEX [IX_FK__vender__image__2704CA5F]
ON [dbo].[venders]
    ([image]);
GO

-- Creating foreign key on [image] in table 'venderaddressproofs'
ALTER TABLE [dbo].[venderaddressproofs]
ADD CONSTRAINT [FK__venderadd__image__27F8EE98]
    FOREIGN KEY ([image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderadd__image__27F8EE98'
CREATE INDEX [IX_FK__venderadd__image__27F8EE98]
ON [dbo].[venderaddressproofs]
    ([image]);
GO

-- Creating foreign key on [Image] in table 'categories'
ALTER TABLE [dbo].[categories]
ADD CONSTRAINT [FK__category__Image__5F7E2DAC]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__category__Image__5F7E2DAC'
CREATE INDEX [IX_FK__category__Image__5F7E2DAC]
ON [dbo].[categories]
    ([Image]);
GO

-- Creating foreign key on [Image] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [FK__customer__Image__5D95E53A]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customer__Image__5D95E53A'
CREATE INDEX [IX_FK__customer__Image__5D95E53A]
ON [dbo].[customers]
    ([Image]);
GO

-- Creating foreign key on [Image] in table 'customeraddressproofs'
ALTER TABLE [dbo].[customeraddressproofs]
ADD CONSTRAINT [FK__customera__Image__607251E5]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__customera__Image__607251E5'
CREATE INDEX [IX_FK__customera__Image__607251E5]
ON [dbo].[customeraddressproofs]
    ([Image]);
GO

-- Creating foreign key on [Image] in table 'driveraddressproffs'
ALTER TABLE [dbo].[driveraddressproffs]
ADD CONSTRAINT [FK__driveradd__Image__6166761E]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__driveradd__Image__6166761E'
CREATE INDEX [IX_FK__driveradd__Image__6166761E]
ON [dbo].[driveraddressproffs]
    ([Image]);
GO

-- Creating foreign key on [Image] in table 'pandetails'
ALTER TABLE [dbo].[pandetails]
ADD CONSTRAINT [FK__pandetail__Image__625A9A57]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__pandetail__Image__625A9A57'
CREATE INDEX [IX_FK__pandetail__Image__625A9A57]
ON [dbo].[pandetails]
    ([Image]);
GO

-- Creating foreign key on [Image] in table 'vattandetails'
ALTER TABLE [dbo].[vattandetails]
ADD CONSTRAINT [FK__vattandet__Image__634EBE90]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__vattandet__Image__634EBE90'
CREATE INDEX [IX_FK__vattandet__Image__634EBE90]
ON [dbo].[vattandetails]
    ([Image]);
GO

-- Creating foreign key on [Image] in table 'venderaddressproofs'
ALTER TABLE [dbo].[venderaddressproofs]
ADD CONSTRAINT [FK__venderadd__Image__6442E2C9]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__venderadd__Image__6442E2C9'
CREATE INDEX [IX_FK__venderadd__Image__6442E2C9]
ON [dbo].[venderaddressproofs]
    ([Image]);
GO

-- Creating foreign key on [vender_id] in table 'teammanagements'
ALTER TABLE [dbo].[teammanagements]
ADD CONSTRAINT [FK__teammanag__vende__14270015]
    FOREIGN KEY ([vender_id])
    REFERENCES [dbo].[venders]
        ([venderid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__teammanag__vende__14270015'
CREATE INDEX [IX_FK__teammanag__vende__14270015]
ON [dbo].[teammanagements]
    ([vender_id]);
GO

-- Creating foreign key on [Image] in table 'admins'
ALTER TABLE [dbo].[admins]
ADD CONSTRAINT [FK__admin__Image__5E8A0973]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__admin__Image__5E8A0973'
CREATE INDEX [IX_FK__admin__Image__5E8A0973]
ON [dbo].[admins]
    ([Image]);
GO

-- Creating foreign key on [Image] in table 'admin1'
ALTER TABLE [dbo].[admin1]
ADD CONSTRAINT [FK__admin__Image__5E8A09731]
    FOREIGN KEY ([Image])
    REFERENCES [dbo].[ImageFiles]
        ([ImageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__admin__Image__5E8A09731'
CREATE INDEX [IX_FK__admin__Image__5E8A09731]
ON [dbo].[admin1]
    ([Image]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------