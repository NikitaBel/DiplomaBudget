function LoadItems(VersionID) {
    $.ajax({
        type: "GET",
        url: $("#LoadItems").attr("href"),
        data: { 'Version': $(VersionID).val() },
        success: function (Data) {
            renderBudgetItem($('#Date'), Data);
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
        $ele.append($('<option/>').val(val.StartOfMonth).text(val.BudgetDate));
    })
}