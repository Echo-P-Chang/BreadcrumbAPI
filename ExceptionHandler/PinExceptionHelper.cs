using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public static class PinExceptionHelper
    {
        public static void SaveToFile(List<object> obj)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];

            StringBuilder sb = new StringBuilder();
            foreach (object item in obj)
            {
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    sb.Append(string.Format("{0} : {1}\r\n", property.Name, property.GetValue(item, null)));
                }
                sb.Append("\r\n");
            }

            using (StreamWriter sw = (File.Exists(logPath)) ? File.AppendText(logPath) : File.CreateText(logPath))
            {
                sw.WriteLine(sb);
            }
        }

        public static void SaveToFile(object obj)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];

            StringBuilder sb = new StringBuilder();

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                sb.Append(string.Format("{0} : {1}\r\n", property.Name, property.GetValue(obj, null)));
            }

            using (StreamWriter sw = (File.Exists(logPath)) ? File.AppendText(logPath) : File.CreateText(logPath))
            {
                sw.WriteLine(sb);
            }
        }

        public static void SendMail()
        {
            string receiver = ConfigurationManager.AppSettings["errorMailReceiver"];
            string subject = ConfigurationManager.AppSettings["errorMailSubject"];
            string body = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["logPath"]).Replace("\r\n", "%0d%0A");
            string mailtoString = string.Format("mailto:{0}?subject={1}&body={2}", receiver, subject, body);
            Process.Start(mailtoString);
            //deleteCurrentErrorLog();

        }
        private static void deleteCurrentErrorLog()
        {
            File.Delete(ConfigurationManager.AppSettings["logPath"]);
        }
    }
}
