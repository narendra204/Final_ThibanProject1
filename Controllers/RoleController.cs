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
                        Roleid = item.roleid,
                        RoleName = Convert.ToString(item.rolename),
                    });
                }
                return objrole;
            }

        }

        //GET: AddRole
        public ActionResult CreateRole(int? roleId)
        {
            RoleManagement roleMgt = new RoleManagement();
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                if (roleId != null && roleId == 0)
                {
                    List<MenuItem> menuItems = new List<MenuItem>();
                    menuItems = db.menuitems.Select(x => new MenuItem()
                    {
                        menuid = x.menuid,
                        menuname = x.menuname
                    }).ToList();

                    roleMgt.menuItem = menuItems;
                }
                else
                {

                    var roleName = db.roles.Where(x => x.roleid == roleId).Select(x => x.rolename).FirstOrDefault();

                    roleMgt.RoleName = roleName;
                    var menu = (from rm in db.rolemanagements
                                where rm.roleid == roleId
                                select new
                                {
                                    menuid = (int)rm.menuid
                                }).AsEnumerable();


                    var finalMenu = (from m in db.menuitems
                                     join sm in menu on m.menuid equals sm.menuid into smd
                                     from sm in smd.DefaultIfEmpty()
                                     group m by new
                                     {
                                         m.menuname,
                                         m.menuid
                                       , m1 = sm.menuid
                                     }
                                      into rc
                                     select new MenuItem
                                     {
                                         menuname = rc.Key.menuname,
                                         menuid = rc.Key.menuid//,
                                       //  selected=rc.Key.m1 !=null?1:0
                                     }).ToList();
                    List<MenuItem> Obj1 = new List<MenuItem>();
                    foreach (var item in finalMenu)
                    {
                        Obj1.Add(new MenuItem()
                        {
                          menuid=item.menuid,
                          menuname=item.menuname
                           // ,
                      //    ,selected = item.Or_stock == null ? 0 : item.Or_stock
                           
                        });
                    }
                    roleMgt.menuItem = Obj1;// finalMenu;
                }
            }
            return View(roleMgt);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateRole(RoleManagement rm, int[] menuid, int? page, int? pageSizeValue)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {

                int id = Convert.ToInt32(Session["Adminid"]);
                string name = Session["UserName"].ToString();
                var s = (from sd in db.admins where sd.username == name select sd.password).FirstOrDefault();
                if (s == rm.Password)
                {

                    //var result = (from d in db.menuitems.Where(a => a.menuid.Equals(rm.Management)) select d.menuid).ToList();//
                    if (menuid.Length > 0)
                    {
                        role ro = new role();
                        ro.rolename = rm.RoleName;
                        ro.createdby = id;
                        ro.status = true;
                        db.roles.Add(ro);
                        db.SaveChanges();
                        var memid = ro.roleid;
                        rolemanagement rmanag = new rolemanagement();
                        foreach (int it in menuid)
                        {
                            rmanag.roleid = memid;
                            rmanag.menuid = it;
                            rmanag.createdby = id;
                            rmanag.status = true;
                            db.rolemanagements.Add(rmanag);
                            db.SaveChanges();
                        }
                    }
                }

                int pageSize = (pageSizeValue ?? 10);
                int pageNumber = (page ?? 1);
                var objmember = new List<RoleManagement>();
                objmember = CallRoleList();
                return View("AddRole", objmember.ToPagedList(pageNumber, pageSize));


                //var objrole = new List<RoleManagement>();
                //objrole = CallRoleList();
                //int pageSize = (pageSizeValue ?? 10);
                //int pageNumber = (page ?? 1);
                //return View(objrole.ToPagedList(pageNumber, pageSize));
            }
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ThibanWaterDBEntities db = new ThibanWaterDBEntities())
            {
                var tid = (from s in db.rolemanagements where s.roleid == id select s);
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