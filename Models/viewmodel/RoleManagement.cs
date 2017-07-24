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
        public int menuid { get; set; }
     
        public List<MenuItem> menuItem { get; set; }
        public int? IsAssigned { get; set; }
    }

    public class Rolepermission
    {
        public int Roleid { get; set; }
        public string RoleName { get; set; }
        public int menuid { get; set; }

        public string menuname { get; set; }
        public string status { get; set; }
        
    }
}