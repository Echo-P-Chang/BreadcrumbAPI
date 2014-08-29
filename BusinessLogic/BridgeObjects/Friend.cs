using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BridgeObjects
{
    public class Friend
    {
        public string ownID { get; set; }
        public string fid { get; set; }
        public Friend(string oid, string uid)
        {
            this.ownID = oid;
            this.fid = uid;
        }
    }
}
