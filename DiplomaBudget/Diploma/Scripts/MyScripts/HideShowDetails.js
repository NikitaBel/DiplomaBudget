$(document).ready(function () {
    $('#Detailsicon').click(function () {
        var $body = $('#DetailsModule');
        if ($body.is(':hidden')) {
            $body.show();
            $(this).text('+ Детали');
        }
        else {
            $body.hide();
            $(this).text('- Детали');
        }
    });

    $('#Headericon').click(function () {
        var $body = $('#HeaderModule');
        if ($body.is(':hidden')) {
            $body.show();
            $(this).text('+ Заголовок');
        }
        else {
            $body.hide();
            $(this).text('- Заголовок');
        }
    });
});