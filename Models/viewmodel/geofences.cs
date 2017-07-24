using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ThibanProject.Models.viewmodel
{
    public class geofences
    {

        public int geofenceid;
        public string zone_name;
        public string city_name;
        public string direction_name;
        public string area;
        public string population;
        public List<string> cordinates=null;
    }
}