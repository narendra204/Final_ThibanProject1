using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models
{
    public class TeamManage
    {
        public string MemberName { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AdminPassword { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Rolename { get; set; }
        public int RoleId { get; set; }
    }
}