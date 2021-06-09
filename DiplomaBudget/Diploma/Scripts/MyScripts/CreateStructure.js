$('#submit').click(function () {
    $('#BudgetItemsError').text('');
    var BudgetItemsList = [];
    $('#StructureBudgetItems tbody tr').each(function (index, ele) {
        var BudgetItem = {
            BudgetItemID: parseInt($('select.Item', this).val())
        }
        BudgetItemsList.push(BudgetItem);
    })

    let data = {
        Name: $('#Name').val(),
        ParentID: $('#Parent').val(),
        Active: $('#Active').val(),
        Structure_Budget_Items: BudgetItemsList
    }

    $.ajax({
        type: 'POST',
        url: $("#CreateSave").attr("href"),
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function (data) {
            if (data.status) {
                alert('Документ сохранён');
                BudgetItemsList = [];
                $('#StructureBudgetItems').empty();
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