using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary
{
    public class Expedition
    {
        public Mission Mission { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Duration { get; set; }

        public Expedition(Mission mission, DateTime startTime, DateTime duration)
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
