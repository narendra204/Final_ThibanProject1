using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class VenderAddProff
    {
        public int vender_address_id { get; set; }
        public int? venderid { get; set; }

        public string image { get; set; }
        public string address_proof { get; set; }
        public string address_image { get; set; }
        public string CompanyTradeName { get; set; }
        public string CompanyType { get; set; }

        // public string CompanyType2 { get; set; }
        public string Nationality { get; set; }
        public string MainOffice { get; set; }
        public string PostalCode { get; set; }
        public string TlelphoneNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string POBox { get; set; }
        [Required(ErrorMessage = "bank name is required ", AllowEmptyStrings = false)]
        public string bank_name { get; set; }

        [Required(ErrorMessage = "Holder Name is Required.", AllowEmptyStrings = false)]
        public string HolderName { get; set; }

        [Required(ErrorMessage = "IBAN Code is Required.", AllowEmptyStrings = false)]
        public string IBANCode { get; set; }

        //profile page models
        //public int venderid { get; set; }
        public string emailid { get; set; }
        public string name { get; set; }

        public string mobile_no { get; set; }

        public string MerchantId { get; set; }
        public string AdditionalInfor { get; set; }

        public string BankNameOther { get; set; }

        //vender end

        //address proff

        //  public int vender_address_id { get; set; }

        public string PanNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        //end

        //bank details     

        public int vender_bank_id { get; set; }

        public int account_no { get; set; }
        public string benificary_name_in_bank { get; set; }
        public string bank_name_profile { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string VatIdentityFicationNumber { get; set; }


        //end


        //store details
        public int storeid { get; set; }

        public string display_name { get; set; }

        public List<Agreement> Agreementlist { get; set; }


        //end


        //end


    }

    public class Agreement
    {
        //public int AgreementId { get; set; }
        //public Nullable<int> VenderId { get; set; }
        public string AgreementVersion { get; set; }
        public Nullable<System.DateTime> AcceptedDate { get; set; }
        public string fileName { get; set; }
        public Nullable<int> fileSize { get; set; }
        public byte[] attachment { get; set; }
        public string FileExtension { get; set; }

        public int? FileId { get; set; }
    }
}