using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logging
{
    public class Logger
    {
        private static readonly Lazy<Logger> _instance =
             new Lazy<Logger>(() => new Logger());

        public static Logger Instance
        {
            get
            {
                return _instance.Value;
            }
        }


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
            streamWriter = new StreamWriter(path, true);
        }

        public Logger()
        {
            streamWriter = null;
        }

        public static void Log<T>(T log)
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

            /*
            if (streamWriter != null)
            {
                lock (streamWriter)
                {
                    streamWriter.WriteLine(message);
                }
            }*/
            Console.WriteLine(message);

        }
    }
}
