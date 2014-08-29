using BusinessLogic;
using Newtonsoft.Json;
using PinService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace PinService.Controllers.Account
{
    public class RegistController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Regist(dynamic req)
        {
            string accessToken = "";
            Dictionary<string, object> retval = new Dictionary<string, object>();
            AccountService ac = new AccountService();
            try
            {
                accessToken = req.accessToken;
                retval = ac.RegistNewAccount(accessToken);
            }
            catch (Exception ext)
            {
                retval.Add("state", false);
                retval.Add("message", ext.Message);
            }

            string yourJson = JsonConvert.SerializeObject(retval);
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(yourJson, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
