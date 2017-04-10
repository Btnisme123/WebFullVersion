using PagedList;
using ShopMyPham_MVC.Areas.Admin.Models;
using ShopMyPham_MVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham_MVC.CodeEntity
{
    public class CodeBeacon
    {
        private ShopBanHangDbContext context = null;
        public CodeBeacon()
        {
            context = new ShopBanHangDbContext();
        }

        public IEnumerable<BeaconModel> GetAllBeacon()
        {
            var model = from a in context.Beacons             
                        select new BeaconModel()
                        {
                            MACID = a.MACID,
                            LocationX=a.LocationX,
                            LocationY = a.LocationY,
                            ShopID = a.ShopID,
                            Information = a.Information,
                            Name = a.Name
                        };
            return model.OrderBy(x => x.Name).ToList();

        }
        public void insert(Beacon beacon)
        {        
            context.Beacons.Add(beacon);
            context.SaveChanges();
        }

        public void Update(Beacon beacon)
        {
            var result = context.Beacons.Find(beacon.MACID);
            result.Name = beacon.Name;
            result.LocationX =  beacon.LocationX;
            result.LocationY = beacon.LocationY;
            result.ShopID = beacon.ShopID;
            result.Information = beacon.Information;
            context.SaveChanges();

        }
    }
}