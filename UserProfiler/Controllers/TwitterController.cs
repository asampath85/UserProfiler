using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces.Parameters;
using Tweetinvi.Core.Interfaces.Streaminvi;
using Tweetinvi.Core.Parameters;
using Tweetinvi.Credentials;
using Tweetinvi.Streams;
using UserProfiler.Models;

namespace UserProfiler.Controllers
{
    public class TwitterController : Controller
    {

        //private const string TwitterConsumerKey = "yeztpKZcCqNBQLEWoondDcvH7";
        //private const string TwitterConsumerSecret = "0kqgCZ1ZzJUHk7VO7XkYwonKVYVxIFX9n5xmgXbBlDHrvdZHVk";
        private const string TwitterConsumerKey = "unZjcnD6BB0vbU5TfTiXPGnVe";
        private const string TwitterConsumerSecret = "7VoiPTrbqaq1vnuu87U4CAbYDyfiqJwlSanN6LvzGkfQ43f1fj";

        //
        // GET: /Twitter/

        public ActionResult Index()
        {
            if (Session["TwitterLogin"] != null && Session["TwitterLogin"].ToString() == "YES")
                return RedirectToAction("Home");

            var appCreds = new ConsumerCredentials(TwitterConsumerKey, TwitterConsumerSecret);
           // var redirectURL = "http://userprofiler.azurewebsites.net/twitter/Callback";
            var redirectURL = "http://127.0.0.1/SocialAnalyzer/twitter/Callback";
            var url = Tweetinvi.CredentialsCreator.GetAuthorizationURL(appCreds, redirectURL);

            return Redirect(url);
        }

        public ActionResult Home()
        {
            return View("Index");
        }

        public ActionResult Callback()
        {
            var verifierCode = Request.Params.Get("oauth_verifier");
            var authorizationId = Request.Params.Get("authorization_id");
            ExceptionHandler.SwallowWebExceptions = false;

            var userCreds = Tweetinvi.CredentialsCreator.GetCredentialsFromVerifierCode(verifierCode, authorizationId);
            Auth.SetCredentials(userCreds);


            Session["TwitterLogin"] = "YES";


            return RedirectToAction("Home");
        }

        public JsonResult GetUserDetails(string id, string key, string geo)
        {

            string message = "";
            TwitterViewModel model = new TwitterViewModel { TweetList = new List<TweetViewModel>() };

            if (!string.IsNullOrEmpty(id))
            {
                var user = Tweetinvi.User.GetUserFromScreenName(id);

                model.ProfileName = user.Name;
                model.FollowerCount = user.FollowersCount;
                model.FollowingCount = user.FriendsCount;

            }

            string query = string.IsNullOrEmpty(key) ? "" : key + " ";
            query += string.IsNullOrEmpty(id) ? "" : "from:" + id;



            var searchParameter = new TweetSearchParameters("")
            {
                Lang = Language.English,

                SearchQuery = query.TrimEnd()
            };

            if(!string.IsNullOrEmpty(geo))
            {
                string[] result = geo.Split(',');
                searchParameter.GeoCode = new GeoCode(double.Parse(result[0].TrimEnd()), double.Parse(result[1].TrimEnd()), double.Parse(result[2].TrimEnd()), DistanceMeasure.Miles);
            }
           
            var tweets = Search.SearchTweets(searchParameter);

            foreach (var item in tweets.OrderByDescending(res => res.CreatedAt))
            {
                model.TweetList.Add(new TweetViewModel
                {
                    TweetText = item.Text,
                    CreatedAt = String.Format("{0:d/M/yyyy HH:mm:ss}", item.CreatedAt),
                    CreatedBy = id,
                    HashTag = item.Hashtags.Any() ? item.Hashtags[0].Text : ""
                });
            }




            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityTweets(string cityName)
        {
                       
            TwitterViewModel model = new TwitterViewModel
            {
                ProfileName = string.Empty,
                FollowerCount = 0,
                FollowingCount = 0,
                TweetList = new List<TweetViewModel>()
            };

            var searchParameter = new TweetSearchParameters("")
            {
                Lang = Language.English,
                //hard coding the geo location for newyork
                GeoCode = Geo.GenerateGeoCode(Geo.GenerateCoordinates(-74.006, 40.742), 1000, DistanceMeasure.Miles)
            };
            var tweets = Search.SearchTweets(searchParameter);



            foreach (var item in tweets.OrderByDescending(res => res.CreatedAt))
            {
                model.TweetList.Add(new TweetViewModel
                {
                    TweetText = item.Text,
                    CreatedAt = String.Format("{0:d/M/yyyy HH:mm:ss}", item.CreatedAt),
                    CreatedBy = item.CreatedBy.Id.ToString(),
                    HashTag = item.Hashtags.Any() ? item.Hashtags[0].Text : ""
                });
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }



    }
}
