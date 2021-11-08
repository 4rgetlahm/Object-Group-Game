using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Configuration
    {
        private static Configuration _obj;
        public Dictionary<string, string> Settings = new Dictionary<string, string>();
        private string FilePath { get; set; }

        private Configuration(string FilePath)
        {
            this.FilePath = FilePath;
            this.ReadConfig();
        }

        public static Configuration GetInstance(string FilePath = @"config.cfg")
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
                        int equalSignLocation = line.IndexOf('=');
                        string key = line.Substring(0, equalSignLocation);
                        string value = line.Substring(equalSignLocation + 1, line.Length - equalSignLocation - 1);
                        Settings.Add(key, value);
                        line = reader.ReadLine();
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception(string.Format("Access denied to read config", ex.Message), ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception(string.Format("Config file not found", ex.Message), ex);
            }

        }

    }
}
