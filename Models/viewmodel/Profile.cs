using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class Profile
    {
        //vender     
        public int venderid { get; set; }
        public string emailid { get; set; }
        public string name { get; set; }

        public string mobile_no { get; set; }

        public string MerchantId { get; set; }
        public string AdditionalInfor { get; set; }

        //vender end

        //address proff

        public int vender_address_id { get; set; }

        public string PanNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        //end

        //bank details     

        public int vender_bank_id { get; set; }

        public int account_no { get; set; }
        public string benificary_name_in_bank { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string VatIdentityFicationNumber { get; set; }


        //end


        //store details
        public int storeid { get; set; }

        public string display_name { get; set; }


        //end



    }
}