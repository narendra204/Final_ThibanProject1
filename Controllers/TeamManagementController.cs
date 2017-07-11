using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models;
using PagedList;


namespace Final_ThibanProject.Controllers
{
    public class TeamManagementController : Controller
    {
        // GET: TeamManagement
        public ActionResult TeamMember(int? page, int? pageSizeValue)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                var objmember = new List<TeamManage>();
                objmember = CallMemberList();
                return View(objmember.ToPagedList(pageNumber, pageSize));
            }
        }

        private List<TeamManage> CallMemberList()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<TeamManage> objteammember = new List<TeamManage>();
                var query = (from data in db.teammanagements select data).ToList();
                ViewBag.menudata = (from data in db.roles select data.rolename).ToList();
                foreach (var item in query)
                {
                    objteammember.Add(new TeamManage()
                    {
                        FirstName=item.firstname,
                        Lastname=item.lastname,
                        Email=item.email,
                        Rolename=item.role.rolename,
                        Status=item.status
                    });
                }
               return objteammember;
            }

        }

        [HttpGet]
        public ActionResult CreateMember()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                ViewBag.menudata = (from data in db.roles select data.rolename).ToList();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateMember(TeamManage tem, int? page, int? pageSizeValue)
        {
            teammanagement temp = new teammanagement();
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                ViewBag.roledata = (from ro in db.roles where ro.rolename == tem.Rolename select ro.roleid).FirstOrDefault();
                int rid = Convert.ToInt32(ViewBag.roledata);
                var id = Convert.ToInt32(Session["Adminid"]);
                string name = Session["UserName"].ToString();
                var s = (from sd in db.admins where sd.username == name select sd.password).FirstOrDefault();
                if (s == tem.AdminPassword)
                {
                    temp.membername = tem.MemberName;
                    temp.firstname = tem.FirstName;
                    temp.lastname = tem.Lastname;
                    temp.email = tem.Email;
                    temp.password = tem.Password;
                    temp.status = tem.Status;
                    temp.roleid = rid;
                    temp.creadteby = id;
                    db.teammanagements.Add(temp);
                    db.SaveChanges();
                }
                var objmember = new List<TeamManage>();
                objmember = CallMemberList();

                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                return View(objmember.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}