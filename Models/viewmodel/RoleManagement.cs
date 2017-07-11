using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models
{
    public class RoleManagement
    {
        public int Roleid { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public string Password { get; set; }
        public string Management { get; set; }
        public string Menuitem { get; set; }
    }
}