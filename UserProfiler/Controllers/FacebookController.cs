using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserProfiler.Models;

namespace UserProfiler.Controllers
{
    public class FacebookController : Controller
    {

        private const string ConsumerKey = "894436167297693";
        private const string ConsumerSecret = "1ca7de717a84a9f6de6808d82428582e";

        //
        // GET: /Facebook/

        public ActionResult Index()
        {
            if (Session["AccessToken"] != null && !string.IsNullOrEmpty(Session["AccessToken"].ToString()))
                return RedirectToAction("Home");

            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConsumerKey,
                client_secret = ConsumerSecret,
                redirect_uri = "http://userprofiler.azurewebsites.net/facebook/Callback",
                response_type = "code",
                scope = "email"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult Callback()
        {
            var verifierCode = Request.Params.Get("code");

            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConsumerKey,
                client_secret = ConsumerSecret,
                redirect_uri = "http://userprofiler.azurewebsites.net/facebook/Callback",
                code = verifierCode
            });

            var accessToken = result.access_token;

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

           



            return RedirectToAction("Home");
        }

        public JsonResult GetUserDetails()
        {
            var fb = new FacebookClient();
            fb.AccessToken = Session["AccessToken"].ToString();


            // Get the user's information, like email, first name, middle name etc
            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string firstname = me.first_name;
            string middlename = me.middle_name;
            string lastname = me.last_name;

            var model = new FacebookViewModel { Email = me.email, FirstName = me.first_name, MiddleName = me.middle_name, LastName = me.last_name };

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Home()
        {
            return View("Index");
        }


    }
}
