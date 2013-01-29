$(function () {
    "use strict";

    var viewModel = function () {

        var
            logger = window.console,

            hub,

            browsers = ko.observableArray(),

            runResults = ko.observableArray(),

            init = function () {
            };

        init();

        return {
            browsers: browsers,
            runResults: runResults
        };
    }();

    ko.applyBindings(viewModel);
});