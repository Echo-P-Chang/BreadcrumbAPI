using DataRepository;
using S3Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.IO;
using BusinessLogic.BridgeObjects;
using Utilities;
using FacebookHandler;

namespace BusinessLogic
{
    public class PinsService 
    {
        public Dictionary<string, object> StorePin(PinModel pin, Dictionary<string, Stream> files, string accessToken)
        {
            Dictionary<string, object> retval = new Dictionary<string, object>();
            try
            {
                //instance handlers
                IDataHelper db = Container.getInstance("dataHelper").Resolve<IDataHelper>();
                IS3Helper s3 = Container.getInstance("s3Helper").Resolve<IS3Helper>();
                IFacebookHelper fb = Container.getInstance("facebookHelper").Resolve<IFacebookHelper>();

                //get facebook profile and friend list
                dynamic fbProfile = fb.GetUserHeadshot(accessToken);
                pin.OwnerID = fbProfile.id;
                pin.OwnerHeadshot = fbProfile.picture.data.url;
                pin.OwnerName = fbProfile.name;

                //insert db
                pin.LastSyncDate = DateTime.UtcNow.ToUnixTimestamp();
                db.StorePin(pin.GenerateToDictionary());

                //upload to S3
                foreach (var item in files)
                {
                    if (!s3.Upload(pin.OwnerID, item.Key, item.Value))
                    {
                        //return false;
                    }
                }
                retval.Add("state", true);
                retval.Add("message", "");

            }
            catch (Exception ext)
            {

                retval.Add("state", false);
                retval.Add("message", ext.Message);
                throw;
            }
            return retval;
        }

        public List<PinModel> GetFriendsBreads(string accessToken, double startTime, int takeCount)
        {
            IDataHelper db = Container.getInstance("dataHelper").Resolve<IDataHelper>();
            IFacebookHelper fb = Container.getInstance("facebookHelper").Resolve<IFacebookHelper>();

            //get facebook profile and friend list
            List<string> friends = fb.GetUsersFriends(accessToken);

            List<Dictionary<string, object>> tempPinSet = db.GetPinWithUserIDs(friends, startTime, takeCount);
            List<PinModel> retval = new List<PinModel>();
            PinModel tempPin = null;
            foreach (var item in tempPinSet)
            {
                tempPin = new PinModel()
                {
                    Title = item["Title"].ToString(),
                    OwnerID = item["Owner"].ToString(),
                    OwnerHeadshot = item["OwnerHeadshot"].ToString(),
                    OwnerName = item["OwnerName"].ToString(),
                    Latitude = Convert.ToDouble(item["Latitude"]),
                    Longitude = Convert.ToDouble(item["Longitude"]),
                    PinDate = Convert.ToDouble(item["PinDate"]),
                    Images = (List<string>)item["Images"]
                };
                retval.Add(tempPin);
            }
            return retval;

        }

        public PinModel GetPinWithPinID(string owner, double pinID)
        {
            
            IDataHelper db = Container.getInstance("dataHelper").Resolve<IDataHelper>();
            Dictionary<string, object> tempPin = db.GetPinWithPinID(owner, pinID);
            PinModel retval = new PinModel()
            {
                Title = tempPin["Title"].ToString(),
                OwnerID = tempPin["Owner"].ToString(),
                OwnerHeadshot = tempPin["OwnerHeadshot"].ToString(),
                OwnerName = tempPin["OwnerName"].ToString(),
                Latitude = Convert.ToDouble(tempPin["Latitude"].ToString()),
                Longitude = Convert.ToDouble(tempPin["Longitude"].ToString()),
                PinDate = Convert.ToDouble(tempPin["PinDate"].ToString()),
                Images = (List<string>)tempPin["Images"]
            };
            return retval;
        }

        public List<PinModel> GetPinsWithUserID(string userID, double since, int takeCnt)
        {

            IDataHelper db = Container.getInstance("dataHelper").Resolve<IDataHelper>();
            List<Dictionary<string, object>> tempPinSet = db.GetPinWithUserID(userID, since, takeCnt);
            List<PinModel> retval = new List<PinModel>();
            PinModel tempPin = null;
            foreach (var item in tempPinSet)
            {
                tempPin = new PinModel()
                {
                    Title = item["Title"].ToString(),
                    OwnerID = item["Owner"].ToString(),
                    OwnerHeadshot = item["OwnerHeadshot"].ToString(),
                    OwnerName = item["OwnerName"].ToString(),
                    Latitude = Convert.ToDouble(item["Latitude"]),
                    Longitude = Convert.ToDouble(item["Longitude"]),
                    PinDate = Convert.ToDouble(item["PinDate"]),
                    Images = (List<string>)item["Images"]
                };
                retval.Add(tempPin);
            }
            return retval;
        }

        public List<PinModel> GetPinWithUserIDs(List<string> userID, double since, int takeCnt)
        {
            IDataHelper db = Container.getInstance("dataHelper").Resolve<IDataHelper>();
            List<Dictionary<string, object>> tempPinSet = db.GetPinWithUserIDs(userID, since, takeCnt);
            List<PinModel> retval = new List<PinModel>();
            PinModel tempPin = null;
            foreach (var item in tempPinSet)
            {
                tempPin = new PinModel()
                {
                    Title = item["Title"].ToString(),
                    OwnerID = item["Owner"].ToString(),
                    OwnerHeadshot = item["OwnerHeadshot"].ToString(),
                    OwnerName = item["OwnerName"].ToString(),
                    Latitude = Convert.ToDouble(item["Latitude"]),
                    Longitude = Convert.ToDouble(item["Longitude"]),
                    PinDate = Convert.ToDouble(item["PinDate"]),
                    Images = (List<string>)item["Images"]
                };
                retval.Add(tempPin);
            }
            return retval;

        }
    }
}
