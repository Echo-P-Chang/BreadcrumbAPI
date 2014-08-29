using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PinService.Models
{
    [DataContract]
    public class DefaultRsp
    {

        [DataMember]
        public bool ok { get; set; }
        [DataMember]
        public object rsp { get; set; }
        [DataMember]
        public string msg { get; set; }
        [DataMember]
        public int code { get; set; }

    }
}