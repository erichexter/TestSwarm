$(function () {
    "use strict";

    var viewModel = function () {

        var
            programId,
            jobId,
            hub,
            imagesPath = $('#imagesPath').val(),
            jobName = ko.observable(),
            browsers = ko.observableArray(),
            runResults = ko.observableArray(),

            logger = window.console || {
                log: function () { },
                debug: function () { },
                error: function () { }
            },

            statusChanged = function (runResultsChange) {
                // update cell contents and status css
                _.each(runResultsChange, function (runResultChange) {
                    // find the run result in the grid that matches the status change
                    var runResult = runResults.find(function (rr) {
                        return rr.runId === runResultChange.RunId;
                    });

                    if (runResult) {
                        _.each(runResultChange.Cells, function (cell) {
                            // find the browser column that matches the status change
                            var browser = runResult.browsers.find(function (b) {
                                return b.clientId === cell.ClientId;
                            });

                            // update the browaser status
                            if (browser) {
                                browser.statusText(cell.CellContents);
                                browser.statusClass(cell.Status.Css);
                            }
                        });
                    }
                });
            },

            parseIds = function () {
                programId = parseValueFromInput('programId');
                jobId = parseValueFromInput('jobId');

                return !isNaN(jobId) && !isNaN(programId);
            },

            parseValueFromInput = function (id) {
                var $input = $('#' + id);

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

            mapRunResultsData = function (data) {
                _.each(data, function (run) {
                    runResults.push({
                        runId: run.RunId,
                        runName: run.RunName,
                        runUrl: run.RunUrl,
                        browsers: function (cells) {
                            var browserStatuses = [];

                            _.each(cells, function (cell) {
                                browserStatuses.push({
                                    cliendId: cell.ClientId,
                                    statusText: ko.observable(cell.CellContents),
                                    statusClass: ko.observable(cell.Status.Css),
                                    statusUrl: ko.observable('/Run/Status?RunId=' + run.RunId)
                                });
                            });

                            return ko.observableArray(browserStatuses);
                        }(run.Cells)
                    });
                });
            },

            mapJobStatusData = function (data) {
                logger.debug('Subscription successful.');

                jobName(data.JobName + " Status");

                _.each(data.Browsers, function (browser) {
                    browsers.push({
                        name: browser.Name,
                        iconAttributes: {
                            src: imagesPath + browser.Browser + '.sm.png',
                            title: browser.Name,
                            alt: browser.Name
                        }
                    });
                });

                mapRunResultsData(data.RunResults);
            },

            subscriptionFailed = function (data) {
                //logger.error(data);

                //TODO: show msg to user
                jobName('not working'); // temp
            },

            hubStarted = function () {
                logger.log("Hub started");

                hub.server.subscribeTo(jobId)
                    .done(mapJobStatusData)
                    .fail(subscriptionFailed);
            },

            startHubFailed = function (msg) {
                //logger.error(data);

                //TODO: show msg to user
                jobName('not working'); // temp
            },

            startHub = function () {
                hub = $.connection.jobStatusHub;

                $.extend(hub.client, hubCallbacks);

                logger.log('Starting hub...');

                $.connection.hub.start()
                    .done(hubStarted)
                    .fail(startHubFailed);
            },

            init = function () {
                if (parseIds()) {
                    startHub();
                }
            };

        init();

        return {
            jobName: jobName,
            browsers: browsers,
            runResults: runResults
        };
    }();

    ko.applyBindings(viewModel);
});