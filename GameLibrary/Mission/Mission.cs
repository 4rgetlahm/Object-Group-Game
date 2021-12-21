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
        public double GoldReward { get; set; }

        public double ExperienceReward { get; set; }

        public Mission(string title, string description, MissionType missionType, TimeSpan min, TimeSpan max, List<DropItem> drops, double experienceReward = 0, double goldReward = 0)
        {
            this.Title = title;
            this.Description = description;
            this.MissionType = missionType;
            this.MinDuration = min;
            this.MaxDuration = max;
            this.Drops = drops;
            this.GoldReward = goldReward;
            this.ExperienceReward = experienceReward;
        }

        protected Mission()
        {

        }
    }
}
