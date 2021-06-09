$('#submit').click(function () {
    var list = [];
    $('#Details tbody .ValueEntryDetail').each(function (index, ele) {
        if (!isNaN(parseFloat($('.Amount', this).val()))) {
            var AmountDetail = parseFloat($('.Amount', this).val().replace(",", "."));
            var Date = $('.DetailBudgetDate', this).val();
            var detailItem = {
                ID: parseInt($('.DetailID', this).val()),
                ValueEntryHeader_ID: parseInt($('.ValueEntryHeader_ID').val()),
                BudgetDateID: Date,
                Amount: AmountDetail
            }
            list.push(detailItem);
        }

    })    


    $.ajax({
        type: 'POST',
        url: $("#Edit").attr("href"),
        data: JSON.stringify(list),
        contentType: 'application/json',
        success: function (data) {
            if (data.status) {
                alert('Детали сохранены');
                list = [];
            }
            else {
                alert('Ошибка при сохранении данных');
            }
        },
        error: function (error) {
            console.log(error);
        }
    })

})