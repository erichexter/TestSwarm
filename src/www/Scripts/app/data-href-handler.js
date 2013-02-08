$(function () {
    $("[data-href]").each(function () {
        var element = $(this);
        element.click(function () { window.location = element.data("href"); });
    });
});