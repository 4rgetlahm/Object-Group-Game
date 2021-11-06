using GameLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class LocationList
    {
        private static LocationList _obj;
        public List<Location> Locations { get; set; }
        private LocationList()
        {
            Locations = new List<Location>();
            try
            {
                using (var context = new DataContext())
                {
                    Locations.AddRange(context.Location.ToList());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static LocationList GetInstance()
        {
            if(_obj == null)
            {
                _obj = new LocationList();
            }
            return _obj;
        }
    }
}
