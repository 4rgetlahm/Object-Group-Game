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
    public static class GetConfiguration
    {
        public static string GetSQL()
        {
            var someData = new Dictionary<string, string>() { { "ConnectionString", "" } };
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"config.txt");
            try
            {
                using StreamReader sr = new StreamReader(path);
                for (int i = 0; i < 5; i++)
                {
                    someData["ConnectionString"] += sr.ReadLine() + ";";
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

            string sql = someData["ConnectionString"];
            return sql;
        }


    }
}
