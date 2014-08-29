using BusinessLogic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic.BridgeObjects
{
    public class PinModel : SerializableClass
    {
        public string OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string OwnerHeadshot { get; set; }
        public string Title { get; set; }
        public double PinDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> Images { get; set; }
        public double LastSyncDate { get; set; }
        public PinModel()
        {
            Images = new List<string>();

        }
    }
}