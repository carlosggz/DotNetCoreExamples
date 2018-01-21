function JwtTestVm(config) {

    var self = this;

    this.token = ko.observable("");
    this.result = ko.observable("");
    this.error = ko.observable("");

    this.login = function () {

        self
            .token("")
            .result("");

        $.ajax({
            url: config.loginUrl,
            type: "POST",
            data: {
                Login: "test",
                Password: "test"
            },
            success: function (data) {

                self.token(data.token);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                self.error(errorThrown);
            }
        });
    };

    this.logout = function () {

        self
            .error("")
            .token("")
            .result("");
    };

    this.load = function () {

        self
            .result("");

        $.ajax({
            url: config.timeUrl,
            type: "GET",
            beforeSend: function (xhr, settings) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + self.token());
            },
            success: function (data) {

                self.result(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                self.error(errorThrown);
            }
        });
    };
}