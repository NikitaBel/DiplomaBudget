$(document).ready(function () {
    $('#Modal').dialog({
        draggable: true,
        resizable: false,
        modal: true,
        autoOpen: false,
        buttons: {
            "Да": function (e) {
                e.preventDefault();
                let data = { Version: $('#Modal .Versions').val(), Item: $('#Modal .Items').val() };
                $.ajax({
                    type: 'Post',
                    data: JSON.stringify(data),
                    url: $('#UpdateSalePlan').attr("href"),
                    contentType: 'application/json',
                    success: function () {
                        alert('Обновление успешно выполнено!');
                        location.reload(true);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            },
            "Нет": function () {
                $(this).dialog('close');
            }
        }
    });
    $("#ExecUpdateSalePlan").click(function () {
        $('#Modal').dialog('open');
        $.ajax({
            type: 'Post',
            url: $('#LoadItems').attr("href"),
            contentType: 'application/json',
            success: function (Data) {
                var $ele = $('.Items');
                $ele.empty();
                $.each(Data, function (i, val) {
                    $ele.append($('<option/>').val(val.ID).text(val.Name));
                })
            },
            error: function (error) {
                console.log(error);
            }
        });
        $.ajax({
            type: 'Post',
            url: $('#LoadVersions').attr("href"),
            contentType: 'application/json',
            success: function (Data) {
                var $ele = $('.Versions');
                $ele.empty();
                $.each(Data, function (i, val) {
                    $ele.append($('<option/>').val(val.ID).text(val.Name));
                })
            },
            error: function (error) {
                console.log(error);
            }
        });
    })
})
