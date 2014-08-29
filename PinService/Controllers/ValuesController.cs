using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace PinService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "hi, this is test method from Pin Service.";
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]string accessToken)
        {
            //AccountService ac = new AccountService();
            //if (ac.RegistNewAccount(accessToken))
            //{
            return Request.CreateResponse(HttpStatusCode.OK, true);
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, false);
            //}
            

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}