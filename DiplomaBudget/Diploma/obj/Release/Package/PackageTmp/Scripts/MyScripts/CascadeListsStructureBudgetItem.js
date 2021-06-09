function LoadItems(BudgetID) {
    $.ajax({
        type: "GET",
        url: $("#LoadItems").attr("href"),
        data: { 'BudgetName': $(BudgetID).val() },
        success: function (Data) {
            renderBudgetItem($(BudgetID).parents('.mycontainer').find('select.Item'), Data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderBudgetItem(element, data) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Выберите'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.ID).text(val.Name));
    })
}