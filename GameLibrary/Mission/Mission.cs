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
        public string Title { get; set; }
        public string Description { get; set; }
        public MissionType MissionType { get; set; }
        public TimeSpan MinDuration { get; set; }
        public TimeSpan MaxDuration { get; set; }
        public List<DropItem> Drops { get; set; }

        public Mission(string title, string description, MissionType missionType, TimeSpan min, TimeSpan max, List<DropItem> drops)
        {
            this.Title = title;
            this.Description = description;
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
