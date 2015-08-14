var FacebookViewModel = function () {
    debugger;
    var self = this;
    self.GetUserDetails = function () {
        debugger;
        self.isLoading(true);
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/Facebook/GetUserDetails"
        }).done(function (data) {
            debugger;
            self.isLoading(false);
            self.Email(data.Email);
            self.FirstName(data.FirstName);
            self.LastName(data.LastName);
            self.MiddleName(data.MiddleName);

        }).error(function (ex) {
            self.isLoading(false);
            alert("Error");
        });


        debugger;
        //self.References(DummyProfile);

    };

    self.isLoading = ko.observable(false);

    self.UserName = ko.observable();
    self.Email = ko.observable("");
    self.FirstName = ko.observable();
    self.MiddleName = ko.observable();
    self.LastName = ko.observable();
   

};