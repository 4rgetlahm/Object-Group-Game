using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    class Configuration
    {
        private static Configuration _obj;
        public Dictionary<string, string> Settings = new Dictionary<string, string>();
        private string FilePath { get; set; }

        private Configuration(string FilePath)
        {
            this.FilePath = FilePath;
            this.ReadConfig();
        }

        public static Configuration GetInstance(string FilePath=@"config.cfg")
        {
            if(_obj == null)
            {
                _obj = new Configuration(FilePath);
            }
            return _obj;
        }

        public void ReadConfig()
        {
            try
            {
                using (var reader = new StreamReader(this.FilePath))
                {
                    string line;
                    line = reader.ReadLine();
                    while (line != null)
                    {
                       var keyValue = line.Split('=');
                       Settings.Add(keyValue[0].Trim(), keyValue[1].Trim());    
                       line = reader.ReadLine();
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception(string.Format("Access denied to read config.txt", ex.Message), ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception(string.Format("config.txt file not found", ex.Message), ex);
            }

        }

    }
}
