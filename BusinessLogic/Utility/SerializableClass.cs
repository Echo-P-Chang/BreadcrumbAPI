using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utility
{
    public class SerializableClass
    {
        public virtual Dictionary<string, object> GenerateToDictionary()
        {
            Dictionary<string, object> retval = new Dictionary<string, object>();

            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                retval.Add(property.Name, property.GetValue(this, null));
            }
            return retval;
        }
    }
}
