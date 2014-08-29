using BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace PinService.Controllers.Breadcrumbs
{
    public class GetFriendsBreadsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetFriendsBreads(dynamic req)
        {
            string accessToken;
            double startTime;
            int takeCount;
            accessToken = req.accessToken;
            startTime = req.startTime;
            takeCount = req.takeCount;

            PinsService pinService = new PinsService();
            string yourJson = JsonConvert.SerializeObject(pinService.GetFriendsBreads(accessToken, startTime, takeCount));
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(yourJson, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
