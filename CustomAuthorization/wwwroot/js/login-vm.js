function LoginVm(config) {

    var self = this;
    this.channels = ko.observableArray([]);
    this.inProgress = ko.observable(false);
    this.errors = ko.observableArray([]);

    config.channels.forEach(function (channel) {
        self.channels.push({
            id: channel.id,
            text: channel.text,
            selected: ko.observable(false)
        });
    });


    this.login = function () {

        var c = 0;

        self.channels().forEach(function (channel) {
            if (channel.selected())
                c += channel.id;
        });

        self.inProgress(true);

        $.ajax({
            url: config.url,
            type: "POST",
            headers: {
                "X-CSRF-TOKEN": config.xsrf
            },
            data: {
                Channels: c
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