using BusinessLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PinService.Controllers.Pins
{
    public class GetPinController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetPinWithUserID(string userID, double since, int takeCnt)
        {
            PinsService pinService = new PinsService();
            string yourJson = JsonConvert.SerializeObject(pinService.GetPinsWithUserID(userID,since,takeCnt));
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(yourJson, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        public HttpResponseMessage GetPinWithUserIDs(dynamic req)
        {
            double since = req.since;
            int takeCnt = req.takeCnt;
            List<string> users = req.users.ToObject<List<string>>();


            PinsService pinService = new PinsService();
            string yourJson = JsonConvert.SerializeObject(pinService.GetPinWithUserIDs(users, since, takeCnt));
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(yourJson, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
