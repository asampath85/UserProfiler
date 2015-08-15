var TwitterViewModel = function () {
    debugger;
    var self = this;
    self.GetUserDetails = function () {
        debugger;
        self.isLoading(true);
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/twitter/GetUserDetails?id=" + self.UserName()
        }).done(function (data) {
            debugger;
            self.isLoading(false);
            self.FollowersCount(data.FollowerCount);
            self.FollowingCount(data.FollowingCount);
            self.FavouritesCount(data.FavouritesCount);
            self.ProfileName(data.ProfileName);

        }).error(function (ex) {
            self.isLoading(false);
            alert("Error");
        });


        debugger;
        //self.References(DummyProfile);
        
    };

    self.isLoading = ko.observable(false);

    self.UserName = ko.observable();
    self.ProfileName = ko.observable("");
    self.FollowersCount = ko.observable();
    self.FollowingCount = ko.observable();
    // Public data properties
    self.Followers = ko.observableArray([]);
    self.Following = ko.observableArray([]);

};

//var DummyProfile = [
//    {
//        "ProfileId": 1,
//        "FirstName": "Anand",
//        "LastName": "Pandey",
//        "Email": "anand@anandpandey.com"
//    },
//    {
//        "ProfileId": 2,
//        "FirstName": "John",
//        "LastName": "Cena",
//        "Email": "john@cena.com"
//    }
//]
