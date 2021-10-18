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
    class Settings
    {
        Dictionary<string, string> allconfig = new Dictionary<string, string>();
        public void ReadConfig(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    string? line;
                    line = reader.ReadLine();
                    while (line != null)
                    {
                       var keyValue = line.Split('=');
                       allconfig.Add(keyValue[0].Trim(), keyValue[1].Trim());    
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
