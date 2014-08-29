using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace FacebookHandler
{
    public class FacebookHelper :IFacebookHelper
    {
        public string GetUserID(string accessToken)
        {
            //string accessToken = "CAAFfZA12v9xkBAADEDiKh2sqJ3bZALBzS3EeBJTgOURM3DhCZBk4d4jKTLDb8BJPZBkSdslu0HQTFerFlPGYlwDLdpKRTe0FYyrql8bpAYkTBVNZBUDoLtbGEuFmyZCuNchp2dkTYp8wNaaJWxLJBPIM7n6Np7tkf7ZAKFbEyzp11TzMi7PoVav1SXWZA86rd1A2ZAEJhBY1gBaFTsgLlhNdxgSOHObJtm6sZD";
            var fb = new FacebookClient(accessToken);

            //dynamic me = fb.Get("me?fields=friends,name,email,favorite_athletes");
            dynamic me = fb.Get("me?fields=id");//friends.limit(5000)  //picture.type(large),favorite_athletes

            //var athletes = me.favorite_athletes;
            string id = me.id; // Store in database
            //string picUrl = me.picture.data.url;
            //string email = me.email; // Store in database
            //string FBName = me.name; // Store in database     
            return id;
        }
        public dynamic GetUserHeadshot(string accessToken)
        {
            var fb = new FacebookClient(accessToken);
            dynamic me = fb.Get("me?fields=id,name,picture.type(small)");
            return me;
        }
        public List<string> GetUsersFriends(string accessToken)
        {
            List<string> friends = new List<string>();
            var fb = new FacebookClient(accessToken);
            dynamic me = fb.Get("me?fields=friends.limit(5000)");
            
            foreach (var friend in (JsonArray)me.friends["data"])
            {
                friends.Add((string)(((JsonObject)friend)["id"]));
            }
            friends.Add(me.id);
            return friends;
        }
        public dynamic GetUserProfile(string accessToken)
        {
            var fb = new FacebookClient(accessToken);

            dynamic me = fb.Get("me?fields=id,name,friends.limit(5000),picture.type(large)");

            var fs = me.friends;
            List<string> friends = new List<string>();
            foreach (var friend in (JsonArray)fs["data"])
            {
                friends.Add((string)(((JsonObject)friend)["id"]));
            }
            me.friends = friends;
            return me;
        }
        //object GetUserProfile(string accessToken)
        //{
            
        //    var fb = new FacebookClient(accessToken);


        //    dynamic me = fb.Get("me?fields=picture.type(large),friends.limit(5000)");

            


        //    //var athletes = me.favorite_athletes;
        //    //retval["id"] = me.id; // Store in database
        //    //string picUrl = me.picture.data.url;
        //    //string email = me.email; // Store in database
        //    //string FBName = me.name; // Store in database   
        //    //NameText.Visible = true;
        //    //NameText.Text = FBName;

        //    //ViewData["FBName"] = FBName; // Storing User's Name in ViewState

        //    //var friends = me.friends;

        //    //string tempName, tempID;
        //    //foreach (var friend in (JsonArray)friends["data"])
        //    //{
        //    //    tempName = (string)(((JsonObject)friend)["name"]);
        //    //    tempID = (string)(((JsonObject)friend)["id"]);
        //    //}


        //    //foreach (var athlete in (JsonArray)athletes)
        //    //{
        //    //    SportsPersonList.Items.Add((string)(((JsonObject)athlete)["name"]));
        //    //}
        //    return me;
        //}


    }
}
