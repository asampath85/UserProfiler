using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfiler.Models
{
    public class TwitterViewModel
    {
        public string ProfileName { get; set; }

        public string Description { get; set; }

        public int FollowerCount { get; set; }

        public int FollowingCount { get; set; }

        public List<TweetViewModel> TweetList { get; set; }
    }

    public class TweetViewModel
    {
        public string TweetText { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string HashTag { get; set; }

    }
}