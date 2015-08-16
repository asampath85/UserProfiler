var TwitterViewModel = function () {
    debugger;
    var self = this;
    self.GetUserDetails = function () {
        debugger;
        self.isLoading(true);
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/SocialAnalyzer/twitter/GetUserDetails?id=" + self.UserName() + "&key=" + self.Keyword() + "&geo=" + self.GeoLatitude() + "," + self.GeoLongitude() + ",1"
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

    self.GetGeoLocation = function () {
        debugger;
        var geocoder = new google.maps.Geocoder();
        var address = self.GeoAddress();

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                self.GeoLatitude(results[0].geometry.location.lat());
                self.GeoLongitude(results[0].geometry.location.lng());                
            } else {
                alert("Geolocation api failed.");
            }
        });
    };

    self.GetCityTweets = function () {

        self.isLoading(true);
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/SocialAnalyzer/twitter/GetCityTweets?cityName=" + self.CityName()
        }).done(function (data) {

            self.isLoading(false);
            self.Tweets(data.TweetList);

        }).error(function (ex) {
            self.isLoading(false);
            alert("Error");
        });


    };

    self.SetSelectedTweet = function (item) {
        this.SelectedTweet(item);
    };

    self.isLoading = ko.observable(false);


    self.UserName = ko.observable("");
    self.Keyword = ko.observable("");
    self.GeoLatitude = ko.observable("");
    self.GeoLongitude = ko.observable("");
    self.GeoAddress = ko.observable("");

    self.GeoAddress.subscribe(function () {
        
        if (self.GeoAddress().trim() == "")
        {
            self.GeoLatitude("");
            self.GeoLongitude("");
            return;
        }
        self.GetGeoLocation();
    });

    self.CityName = ko.observable();

    self.ProfileName = ko.observable("");
    self.FollowersCount = ko.observable();
    self.FollowingCount = ko.observable();
    self.FavouritesCount = ko.observable();
    self.Followers = ko.observableArray([]);
    self.Following = ko.observableArray([]);

    self.SelectedTweet = ko.observable();
    self.Tweets = ko.observableArray();


};

