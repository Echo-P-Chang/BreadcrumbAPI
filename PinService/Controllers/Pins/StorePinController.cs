using BusinessLogic;
using BusinessLogic.BridgeObjects;
using Newtonsoft.Json;
using PinService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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
    public class StorePinController : ApiController
    {
        public async Task<HttpResponseMessage> StorePin()
        {
            PinModel pin = new PinModel();
            Dictionary<string, object> retval = new Dictionary<string, object>();
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                Dictionary<string, Stream> files = new Dictionary<string, Stream>();
                string accessToken = "";
                await Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((tsk) =>
                {
                    MultipartMemoryStreamProvider prvdr = tsk.Result;

                    foreach (HttpContent ctnt in prvdr.Contents)
                    {
                        switch (ctnt.Headers.ContentDisposition.Name)
                        {
                            case "\"accessToken\"":
                                accessToken = ctnt.ReadAsStringAsync().Result.ToString();
                                break;
                            case "\"Title\"":
                                pin.Title = ctnt.ReadAsStringAsync().Result.ToString();
                                break;
                            case "\"PinDate\"":
                                pin.PinDate = Convert.ToDouble(ctnt.ReadAsStringAsync().Result.ToString());
                                break;
                            case "\"Latitude\"":
                                pin.Latitude = Convert.ToDouble(ctnt.ReadAsStringAsync().Result.ToString());
                                break;
                            case "\"Longitude\"":
                                pin.Longitude = Convert.ToDouble(ctnt.ReadAsStringAsync().Result.ToString());
                                break;
                            //case "\"LastSyncDate\"":
                            //    pin.LastSyncDate = Convert.ToDouble(ctnt.ReadAsStringAsync().Result.ToString());
                            //    break;
                            case "\"File\"":
                                pin.Images.Add(ConfigurationManager.AppSettings["S3Path"] + pin.OwnerName + @"\" + pin.PinDate.ToString("0") + ctnt.Headers.ContentDisposition.FileName.Replace(@"""", ""));
                                files.Add(pin.PinDate.ToString("0") + ctnt.Headers.ContentDisposition.FileName.Replace(@"""", ""), ctnt.ReadAsStreamAsync().Result);
                                break;
                            default:
                                break;
                        }
                    }
                    if (files.Count > 0)
                    {
                        PinsService pinService = new PinsService();
                        retval = pinService.StorePin(pin, files, accessToken);
                    }
                });

            }
            catch (Exception ext)
            {
                retval.Add("state", false);
                retval.Add("message", ext.Message);
                throw;
            }


            string yourJson = JsonConvert.SerializeObject(retval);
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(yourJson, Encoding.UTF8, "application/json");
            return response;
        }

        
    }
}
