using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfiler.Models
{
    public class TwitterViewModel
    {
        public string ProfileName { get; set; }

        public int FollowerCount { get; set; }

        public int FollowingCount { get; set; }
    }
}