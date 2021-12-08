using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameLibrary
{
    public class Expedition
    {
        [Key]
        public int ExpeditionID { get; set; }
        public Mission Mission { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }

        public Expedition(Mission mission, DateTime startTime, TimeSpan duration)
        {
            this.Mission = mission;
            this.StartTime = startTime;
            this.Duration = duration;
        }

        protected Expedition()
        {

        }
    }
}
