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
            //var redirectURL = "http://userprofiler.azurewebsites.net/twitter/Callback";
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

        public JsonResult GetUserDetails(string id)
        {
            string message = "";

         

            var user = Tweetinvi.User.GetUserFromScreenName(id);

                  


            TwitterViewModel model = new TwitterViewModel {
            ProfileName = user.Name,
            FollowerCount = user.FollowersCount,
            FollowingCount = user.FriendsCount
            };


            return Json(model, JsonRequestBehavior.AllowGet);
        }



    }
}
