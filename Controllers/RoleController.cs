using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models;
using System.Web.Security;
using PagedList;


namespace Final_ThibanProject.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult AddRole(int? page, int? pageSizeValue)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                var objmember = new List<RoleManagement>();
                objmember = CallRoleList();
                return View(objmember.ToPagedList(pageNumber, pageSize));
            }
        }

        private List<RoleManagement> CallRoleList()
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                List<RoleManagement> objrole = new List<RoleManagement>();
                var query = (from data in db.roles select data).ToList();
                foreach (var item in query)
                {
                    objrole.Add(new RoleManagement()
                    {
                        Roleid=item.roleid,
                        RoleName =Convert.ToString(item.rolename),
                    });
                }
                return objrole;
            }

        }

        //GET: AddRole
        public ActionResult CreateRole()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateRole(RoleManagement rm, string[] menuitem, int? page, int? pageSizeValue)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {

                int id = Convert.ToInt32(Session["Adminid"]);
                string name = Session["UserName"].ToString();
                var s = (from sd in db.admins where sd.username == name select sd.password).FirstOrDefault();
                if (s == rm.Password)
                {

                    var result = (from d in db.menuitems.Where(a => a.menuname.Equals(rm.Management)) select d.menuname).ToList();
                    role ro = new role();
                    ro.rolename = rm.RoleName;
                    ro.createdby = id;
                    ro.status = true;
                    db.roles.Add(ro);
                    db.SaveChanges();
                    var memid = ro.roleid;
                    rolemanagement rmanag = new rolemanagement();
                    foreach (var it in menuitem)
                    {
                        rmanag.roleid = memid;
                        rmanag.menuitem = it;
                        rmanag.createdby = id;
                        rmanag.status = true;
                        db.rolemanagements.Add(rmanag);
                        db.SaveChanges();
                    }
                    
                }
                var objrole = new List<RoleManagement>();
                objrole = CallRoleList();
                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                return View(objrole.ToPagedList(pageNumber, pageSize));
            }
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var tid = (from s in db.rolemanagements where s.roleid==id select s);
                foreach (var item in tid)
                {
                    db.rolemanagements.Remove(item);
                }
                var main = db.roles.Find(id);
                db.roles.Remove(main);
                db.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

       

    }
}