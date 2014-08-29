using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExceptionHandler
{
    public class PinException : Exception
    {
        public PinException() : base() { }
        public PinException(string message) : base(message) { }
        public PinException(string message, Exception innerException) : base(message, innerException) { }
        public StringBuilder MakeLogString()
        {
            StringBuilder sb = new StringBuilder(string.Format("Timestamp:{0}\r\n", DateTime.Now.ToString("yyyy/MM/dd HH:hh:ss")));

            if (this.Message != null)
            {
                sb.Append(string.Format("Message:{0}\r\n",this.Message));
            }

            if (this.InnerException != null && this.InnerException.StackTrace != null)
            {
                sb.Append(string.Format("StackTrace:{0}\r\n", this.InnerException.StackTrace));
            }

            return sb;
        }
    }
}
