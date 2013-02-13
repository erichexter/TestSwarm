<script>
    
    $(function() {
        var fn = function() {
            if ($("td.notdone").length) {
                $.get('@Url.Action("StatusTable", new {id = Model.JobId})', function(html) {
                    var table = html, insert = jQuery("table.results");
                    if (table !== insert.html()) {
                        insert.html(table);
                    }
                    setTimeout(fn, 2500);
                });
            }
        };
        setTimeout(fn, 2500);
        $("td:has(a)").live("dblclick", function () {
            var params = /\?(.*)$/.exec($(this).find("a").attr("href"))[1];
            $.ajax({
                url: "@Url.Action("Reset", "Run")",
                type: "POST",
            data: params + ''
        });
        $(this).empty().attr("class", "notstarted notdone");
    });
$("div.browser").live('click', function() {
    var url = $(this).attr('rel');
    var browserName = $('img', this).attr('alt');
    $('#dialog').load(url, function () {
        $(this).dialog({title: browserName});
    });
});
setInterval(function() {


    var percentof = function(part, whole) {

        var result = (part / whole) * 100;
        var round = Math.round(result);
        if (isNaN(round))
            return 0;
        return round;

    };

    $.getJSON('@Url.Action("Index", "JobSummary",new {area = "api", id = Model.JobId})', function(data) {

        data.get_passing_test_count = function() { return this.TotalTests - this.TotalFailedTests; };
        data.get_pass_percent = function() { return percentof(this.get_passing_test_count(), this.TotalTests); };
        data.get_pass_runs_percent = function() { return percentof(this.PassRuns, this.TotalRuns); };
        data.get_exec_runs_percent = function() { return percentof(this.PassRuns + this.FailRuns, this.TotalRuns); };

        $("div.summary").find('p#passTest span.answer').html(data.get_passing_test_count());
        $("div.summary").find('p#passPercent span.answer').html(data.get_pass_percent());
        $("div.summary").find('p#passRunsPercent span.answer').html(data.get_pass_runs_percent());
        $("div.summary").find('p#execRunsPercent span.answer').html(data.get_exec_runs_percent());
        $("div.summary").find('p#totalRuns span.answer').html(data.TotalRuns);
        $("div.summary").find('p#totalTest span.answer').html(data.TotalTests);

    });

}, 6000);
});
*/
</script>
