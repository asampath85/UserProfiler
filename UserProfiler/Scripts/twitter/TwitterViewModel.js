var TwitterViewModel = function () {
    debugger;
    var self = this;
    self.GetUserDetails = function () {
        debugger;
        self.isLoading(true);
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/twitter/GetUserDetails?id=" + self.UserName() + "&key=" + self.Keyword() + "&geo=" + self.GeoLatitude() + "," + self.GeoLongitude() + ",1"
        }).done(function (data) {
            debugger;
            self.isLoading(false);
            self.FollowersCount(data.FollowerCount);
            self.FollowingCount(data.FollowingCount);
            self.FavouritesCount(data.FavouritesCount);
            self.ProfileName(data.ProfileName);

            data.TweetList.forEach(function (element, index) {
                
                data.TweetList[index].TweetText = element.TweetText.replace(/Sexual/gi, '<span style="color: red; font-weight: bold">Sexual</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/Assault/gi, '<span style="color: red; font-weight: bold">Assault</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/Fined/gi, '<span style="color: red; font-weight: bold">Fined</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/Sex/gi, '<span style="color: red; font-weight: bold">Sex</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/Punishes/gi, '<span style="color: red; font-weight: bold">Punishes</span>');

                data.TweetList[index].TweetText = element.TweetText.replace(/Safety/gi, '<span style="color: yellow; font-weight: bold">Safety</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/Questions/gi, '<span style="color: yellow; font-weight: bold">Questions</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/Risk/gi, '<span style="color: yellow; font-weight: bold">Risk</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/So hot/gi, '<span style="color: yellow; font-weight: bold">So hot</span>');

                data.TweetList[index].TweetText = element.TweetText.replace(/Cooler/gi, '<span style="color: green; font-weight: bold">Cooler</span>');
                data.TweetList[index].TweetText = element.TweetText.replace(/Great/gi, '<span style="color: green; font-weight: bold">Great</span>');
            });

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
        self.isCity(true);
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
    self.isUser = ko.observable(true);
    self.isCity = ko.observable(true);

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

