using BusinessLogic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BridgeObjects
{
    public class User : SerializableClass
    {
        public string userID { get; set; }
        public string userName { get; set; }
        public string headshotURL { get; set; }
        public List<string> friends { get; set; }
        public bool IsPayUser { get; set; }
        //[Column(TypeName = "DateTime2")]
        public double registDate { get; set; }
        //[Column(TypeName = "DateTime2")]
        public double lastLoginDate { get; set; }
        //[Column(TypeName = "DateTime2")]
        public double lastSyncDate { get; set; }
        public User(string uid)
        {
            this.userID = uid;
        }
    }
}
