$(document).ready(function () {
    $('#add').click(function () {
        //Клонирую строку
        var $newRow = $('#mainrow').clone().removeAttr('id');
        $('.Item', $newRow).val($('#Item').val());
        $('.Budget', $newRow).val($('#Budget').val());

        //Заменяю кнопку Добавить на Удалить
        $('#add', $newRow).addClass('remove').val('Удалить').removeClass('btn-success').addClass('btn-danger');

        //Удаляю атрибут id у клона, чтобы не мешало действию других функций
        $('#Item,#Budget,#add', $newRow).removeAttr('id');
        $('.text-danger', $newRow).remove();
        $('#StructureBudgetItems tbody').append($newRow);

        $('#Item,#Budget').val('0');
        $('#BudgetItemsError').empty();
    })

    $('#StructureBudgetItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    })
})