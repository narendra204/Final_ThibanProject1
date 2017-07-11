using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class DiverRating
    {
        public int drateid { get; set; }
        public int? rating { get; set; }

        public string comment { get; set; }
        // Avatar	User Name	Email ID	Driver Name	Rating	Comments

        public string emailid { get; set; }
        public string name { get; set; }
        public string dusername { get; set; }
        public byte[] image { get; set; }
    }
}