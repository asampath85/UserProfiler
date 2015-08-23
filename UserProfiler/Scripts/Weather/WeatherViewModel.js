var WeatherViewModel = function () {
    debugger;
    var self = this;

    self.GetWeatherDetails = function () {
        debugger;

        self.isLoading(true);

        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/weather/GetCityWeatherDetails?id=" + self.CityName()
        }).done(function (data) {

            self.isLoading(false);
            self.WeatherDetails(data);

        }).error(function (ex) {
            self.isLoading(false);
            alert("Error");
        });


    };

    self.isLoading = ko.observable(false);
    self.CityName = ko.observable();
    self.WeatherDetails = ko.observable();




};

