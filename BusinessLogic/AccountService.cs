using DataRepository;
using ExceptionHandler;
using FacebookHandler;
using S3Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Reflection;
using System.Collections;
using BusinessLogic.BridgeObjects;
using Utilities;

namespace BusinessLogic
{
    public class AccountService
    {
        public void testMethod()
        {
            try
            {

                IFacebookHelper fb = Container.getInstance("facebookHelper").Resolve<IFacebookHelper>();
                string sss = fb.GetUserHeadshot("CAAFfZA12v9xkBAADEDiKh2sqJ3bZALBzS3EeBJTgOURM3DhCZBk4d4jKTLDb8BJPZBkSdslu0HQTFerFlPGYlwDLdpKRTe0FYyrql8bpAYkTBVNZBUDoLtbGEuFmyZCuNchp2dkTYp8wNaaJWxLJBPIM7n6Np7tkf7ZAKFbEyzp11TzMi7PoVav1SXWZA86rd1A2ZAEJhBY1gBaFTsgLlhNdxgSOHObJtm6sZD");
            }
            catch (Exception ext)
            {
                PinException pe = new PinException(ext.Message, ext);
                User us = new User("");
                us.IsPayUser = false;
                us.registDate = DateTime.MinValue.ToUnixTimestamp();
                us.userID = "123";
                us.headshotURL = "url";
                List<object> logs = new List<object>();
                logs.Add(us);
                PinExceptionHelper.SaveToFile(logs);
                //PinExceptionHelper.SendMail();
            }
        }
        public Dictionary<string, object> RegistNewAccount(string accessToken)
        {
            Dictionary<string, object> retval = new Dictionary<string, object>();
            try
            {
                //instance handlers
                IFacebookHelper fb = Container.getInstance("facebookHelper").Resolve<IFacebookHelper>();
                IDataHelper db = Container.getInstance("dataHelper").Resolve<IDataHelper>();
                IS3Helper s3 = Container.getInstance("s3Helper").Resolve<IS3Helper>();
                
                //List<string> sss = fb.GetUsersFriends(accessToken);
                //get facebook profile and friend list
                dynamic fbProfile = fb.GetUserProfile(accessToken);

                User user = new User(fbProfile.id)
                {
                    userName = fbProfile.name,
                    headshotURL = fbProfile.picture.data.url,
                    friends = fbProfile.friends,
                    registDate = DateTime.UtcNow.ToUnixTimestamp(),
                    lastLoginDate = DateTime.UtcNow.ToUnixTimestamp(),
                    lastSyncDate = DateTime.UtcNow.ToUnixTimestamp()
                };

                //if user doesn't exist then insert to table:user
                if (!db.CheckUserIsExist(user.userID))
                {
                    db.InsertUser(user.GenerateToDictionary());
                }

                ////if current bucket doesn't exist then create account bucket
                if (!s3.CheckBucketIsExist(user.userID))
                {
                    s3.CreateBucket(user.userID);    
                }
                
                retval.Add("state", true);
                retval.Add("message", "");
            }
            catch (Exception ext)
            {
                PinException pe = new PinException(ext.Message, ext);
                PinExceptionHelper.SaveToFile(pe);
                retval.Add("state", false);
                retval.Add("message", ext.Message);
            }

            return retval;
        }

        
    }
}
