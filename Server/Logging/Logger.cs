using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logging
{
    public class Logger : ILogger
    {
        private StreamWriter streamWriter;

        public Logger(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            //streamWriter = new StreamWriter(path, true);
        }

        public void Log<T>(T log)
        {
            string message;
            if(log is string)
            {
                message = (string) (object) log;
            } 
            else if (log is Exception)
            {
                Exception exc = (Exception)(object)log;
                message = "Exception has occured: \n" + exc.Message + "\nSTACK TRACE: \n" + exc.StackTrace; 
            } 
            else
            {
                message = JsonConvert.SerializeObject(log);
            }


            Console.WriteLine(message);

        }
    }
}
