using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using BusinessLogic;
using System.IO;


namespace PinService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AccountService ac = new AccountService();
            ac.testMethod();
            //ac.RegistNewAccount("CAAFfZA12v9xkBAJywVEEkDh5mFChKpEkYT54ZC5b6aN6RDSlHYMNQjcCNWPiabxrq9XPw4VxExfZBjxfS0LAJhw1Y1BO0LSy1IWxtZCdO50k6Tv21XHj7HKShFiQ3J68FIieenUIx2uFngBOBcboJZCqH2epzOm0ZCrHEgvrQYuGsjCxKI9dCOKW2ZCuJItZCXIZD");
            //double sss = (DateTime.UtcNow - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;

            return View();
        }


        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files)
        {
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                ModelState.AddModelError("", "Uploaded.");
            }
            else
            {
                ModelState.AddModelError("", "Please upload a file.");
            }
            return View();

        }
    }
}
