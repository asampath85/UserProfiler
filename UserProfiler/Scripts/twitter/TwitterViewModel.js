var TwitterViewModel = function () {
    debugger;
    var self = this;
    self.GetUserDetails = function () {
        debugger;
        self.isLoading(true);
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/SocialAnalyzer/twitter/GetUserDetails?id=" + self.UserName()
        }).done(function (data) {
            debugger;
            self.isLoading(false);
            self.FollowersCount(data.FollowerCount);
            self.FollowingCount(data.FollowingCount);
            self.FavouritesCount(data.FavouritesCount);
            self.ProfileName(data.ProfileName);

            self.Tweets(data.TweetList);

        }).error(function (ex) {
            self.isLoading(false);
            alert("Error");
        });


        debugger;
      
        
    };

    self.SetSelectedTweet = function(item){
        this.SelectedTweet(item);
    };

    self.isLoading = ko.observable(false);

    self.UserName = ko.observable();
    self.ProfileName = ko.observable("");
    self.FollowersCount = ko.observable();
    self.FollowingCount = ko.observable();
    self.FavouritesCount = ko.observable();
    self.Followers = ko.observableArray([]);
    self.Following = ko.observableArray([]);

    self.SelectedTweet = ko.observable();
    self.Tweets = ko.observableArray();


};

