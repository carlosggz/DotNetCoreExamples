function LoginVm(config) {

    var self = this;
    this.uid = ko.observable(config.uid);
    this.pwd = ko.observable(config.pwd);
    this.inProgress = ko.observable(false);
    this.errors = ko.observableArray([]);

    this._validate = function () {

        var messages = [];

        if ($.trim(self.uid()) == "") {
            messages.push("Type a login");
        }

        if ($.trim(self.pwd()) == "") {
            messages.push("Type a password");
        }

        self.errors(messages);
        return messages.length == 0;
    };

    this.login = function () {

        if (!self._validate())
            return;

        self.inProgress(true);

        $.ajax({
            url: config.url,
            type: "POST",
            headers: {
                "X-CSRF-TOKEN": config.xsrf
            },
            data: {
                Login: self.uid(),
                Password: self.pwd()
            },
            success: function (data) {

                self.inProgress(false);

                if (!data.success) {
                    self.errors(data.messages);
                } else {
                    document.location.href = data.url;
                }
            },
            error: function () {
                self.inProgress(true);
                self.errorMessage("Error in access to the server, please try again");
            }
        });
    };
}