$(function () {
    "use strict";

    var viewModel = function () {

        var
            logger = window.console || {
                log: function () { },
                debug: function () { }
            },      

            programId,

            jobId,

            hub,

            browsers = ko.observableArray(),

            runResults = ko.observableArray(),

            statusChanged = function (data) {

            },

            parseIds = function () {
                programId = parseValueFromInput('programId');
                jobId = parseValueFromInput('job');

                return !isNaN(jobId) && !isNaN(programId);
            },

            parseValueFromInput = function (id) {
                var $input = $(id);

                if ($input.length > 0) {
                    return parseInt($input.val(), 10);
                }
                else {
                    return NaN;
                }
            },

            hubCallbacks = {
                statusChanged: statusChanged
            },

            startHub = function () {
                hub = $.connection.lastJobStatusHub;

                $.extend(hub.client, hubCallbacks);

                logger.log('Starting hub...');

                $.connection.hub.start().done(function () {
                    logger.log("Hub started");
                    hub.server.subscribeTo(jobId);
                });
            },

            init = function () {
                if (parseIds()) {
                    startHub();
                }
            };

        init();

        return {
            browsers: browsers,
            runResults: runResults
        };
    }();

    ko.applyBindings(viewModel);
});