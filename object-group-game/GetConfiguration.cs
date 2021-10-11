using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    public class GetConfiguration
    {
        public static void GetConfigurationValue()
        {
            string connect = ConfigurationManager.AppSettings["ConnectionString"];
            int MaxItems = Int16.Parse(ConfigurationManager.AppSettings["MaxItems"]);
            int MaxGold = Int16.Parse(ConfigurationManager.AppSettings["MaxItems"]);
        }
    }
}
