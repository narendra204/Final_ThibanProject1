using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_ThibanProject.Models.DB;
using Final_ThibanProject.Models;
using System.IO;
using Final_ThibanProject.Models.LogError;
using Final_ThibanProject.Models.viewmodel;
using PagedList;
using System.Data.Entity.Validation;
using Newtonsoft.Json;
using System.Collections;
using System.Text;
using System.IO;
namespace Final_ThibanProject.Controllers
{
    public class geofenceController : Controller
    {
        ThibanWaterDBEntities db = new ThibanWaterDBEntities();

        // GET: geofence
        public ActionResult Index(string regionname)
        {
            List<geofences> lstRegions = new List<geofences>();
            ViewBag.regionname = regionname;
            if (!string.IsNullOrEmpty(regionname))
            {
                var obj = (from dg in db.dgeofences
                           where  dg.zone_name.Contains(regionname)
                           select new
                           {
                               dg.geofenceid,
                               dg.zone_name,
                               dg.city_name,
                               dg.direction_name,
                               dg.area,
                               dg.population
                           }).ToList();

                foreach (var item in obj)
                {
                    lstRegions.Add(new geofences()
                    {
                        area = item.area,
                        city_name = item.city_name,
                        cordinates = null,
                        direction_name = item.direction_name,
                        geofenceid = item.geofenceid,
                        population = item.population,
                        zone_name = item.zone_name
                    });
                }
            }
            else
            {

                var obj = (from dg in db.dgeofences
                           
                           select new
                           {
                               dg.geofenceid,
                               dg.zone_name,
                               dg.city_name,
                               dg.direction_name,
                               dg.area,
                               dg.population
                           }).ToList();

                foreach (var item in obj)
                {
                    lstRegions.Add(new geofences()
                    {
                        area = item.area,
                        city_name = item.city_name,
                        cordinates = null,
                        direction_name = item.direction_name,
                        geofenceid = item.geofenceid,
                        population = item.population,
                        zone_name = item.zone_name
                    });
                }

            }
            return View(lstRegions);
        }

        public String getregionfilepath(int regionid)
        {
            
            string strXML= "<?xml version=\"1.0\" encoding=\"utf-8\" ?><kml xmlns=\"http://earth.google.com/kml/2.1\"><Document><Folder><name>regions</name><Schema name=\"regions\" id=\"regions\"><SimpleField name=\"pkEmirateID\" type=\"int\"></SimpleField><SimpleField name=\"EnglishName\" type=\"string\"></SimpleField><SimpleField name=\"UTMArea\" type=\"float\"></SimpleField><SimpleField name=\"Population\" type=\"int\"></SimpleField></Schema><Placemark><name>$#$name$#$</name>	<description>1</description><Style><LineStyle><color>ff0000ff</color></LineStyle><PolyStyle><fill>0</fill></PolyStyle></Style><ExtendedData><SchemaData schemaUrl=\"#regions\"><SimpleData name=\"pkEmirateID\">$#$rateid$#$</SimpleData><SimpleData name=\"EnglishName\">$#$name$#$</SimpleData><SimpleData name=\"UTMArea\">$#$area$#$</SimpleData><SimpleData name=\"Population\">$#$population$#$</SimpleData></SchemaData></ExtendedData><MultiGeometry>$#$cordinates$#$</MultiGeometry>	</Placemark>  </Folder></Document></kml>";
            var objList = (from dgeo in db.dgeofences where dgeo.geofenceid == regionid select dgeo).ToList();
            StringBuilder strb = new StringBuilder();

            if (objList != null)
            {
                strXML = strXML.Replace("$#$rateid$#$", objList[0].geofenceid.ToString());
                strXML = strXML.Replace("$#$name$#$", objList[0].zone_name);
                strXML = strXML.Replace("$#$area$#$", objList[0].area);
                strXML = strXML.Replace("$#$population$#$", objList[0].population.ToString());
                
            }

            var coridinatesList = (from dgcordinates in db.dgeofencelocations where dgcordinates.geofenceid == regionid select dgcordinates).ToList();
            if (coridinatesList != null)
            {
                foreach (var item in coridinatesList)
                {
                    strb.Append("<Polygon><outerBoundaryIs><LinearRing><coordinates>");
                    strb.Append(item.location);
                    strb.Append("</coordinates></LinearRing></outerBoundaryIs></Polygon>");

                }
            }
            strXML = strXML.Replace("$#$cordinates$#$", strb.ToString());
            string strsessionid = string.Empty;
            if (Session != null) {
                strsessionid = Session.SessionID.ToString();
            }
            System.IO.File.WriteAllText(Server.MapPath("~/content")+"/data/region_data_" + strsessionid + ".kml", strXML);           
                return "region_data_" + strsessionid + ".kml";
        }
    }
}