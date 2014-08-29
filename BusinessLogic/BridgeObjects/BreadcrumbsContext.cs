using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic.BridgeObjects
{
    public class BreadcrumbsContext
    {
        //personel
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string HeadshotURL { get; set; }

        //coverage
        public string Title { get; set; }
        public bool IsGroup { get; set; }
        public DateTime PinStartDate { get; set; }
        public DateTime PinEndDate { get; set; }
        public List<SimpleUser> WithWho { get; set; }
        public List<SimpleUser> Likes { get; set; }
        public List<Reply> Replies { get; set; }

        //pins
        public List<PinModel> Pins { get; set; }

        
    }
}