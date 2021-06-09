$('.CreateDoc').click(function () {
    $.ajax({
        type: 'Post',
        url: $("#CreateDoc").attr("href"),
        contentType: 'application/json',
        success: function (data) {
            alert("Документы были успешно созданы.");
        },
        error: function (error) {
            console.log(error);
        }
    })
})

$('.DeleteDoc').click(function (ID) {
    $.ajax({
        type: 'Post',
        url: $("#DeleteDoc").attr("href"),
        contentType: 'application/json',
        success: function (data) {
            alert("Документы были успешно удалены.");
        },
        error: function (error) {
            console.log(error);
        }
    })
})