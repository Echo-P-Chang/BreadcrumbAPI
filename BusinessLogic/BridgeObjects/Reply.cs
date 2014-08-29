using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic.BridgeObjects
{
    public class Reply
    {
        public SimpleUser Replier { get; set; }
        public DateTime ReplyDate { get; set; }
        public string Message { get; set; }
        public List<SimpleUser> likes { get; set; }
    }
}