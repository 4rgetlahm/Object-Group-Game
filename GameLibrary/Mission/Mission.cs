using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameLibrary
{
    public class Mission
    {
        [Key]
        public int MissionID { get; set; }
        public Location Location { get; set; }
        public MissionType MissionType { get; set; }
        public DateTime MinDuration { get; set; }
        public DateTime MaxDuration { get; set; }
        public List<DropItem> Drops { get; set; }

        public Mission(Location location, MissionType missionType, DateTime min, DateTime max, List<DropItem> drops)
        {
            this.Location = location;
            this.MissionType = missionType;
            this.MinDuration = min;
            this.MaxDuration = max;
            this.Drops = drops;
        }

        protected Mission()
        {

        }
    }
}
