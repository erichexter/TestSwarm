$(function () {
    "use strict";

    var viewModel = function () {

        var
            logger = window.console || {
                log: function () { },
                debug: function () { }
            },      

            hub,

            browsers = ko.observableArray(),

            runResults = ko.observableArray(),

            statusChanged = function (data) {

            },

            hubCallbacks = {
                statusChanged: statusChanged
            },

            init = function () {
                hub = $.connection.lastJobStatusHub;

                $.extend(hub.client, hubCallbacks);

                logger.log('Starting hub...');

                $.connection.hub.start().done(function () {
                    logger.log("Hub started");
                    hub.server.connect();
                });
            };

        init();

        return {
            browsers: browsers,
            runResults: runResults
        };
    }();

    ko.applyBindings(viewModel);
});