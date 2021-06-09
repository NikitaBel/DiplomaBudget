function TotalAmount() {
    var Amount = 0;
    $('#Details tbody .ValueEntryDetail').each(function (index, ele) {
        if (!isNaN(parseFloat($('.Amount', this).val()))) {
            var flat = $('.Amount', this).val().replace(",", ".");
            Amount += parseFloat(flat);
        }        
    })
    $('#AmountTotal').val(Amount);
}

$('.Amount').keyup(function (e) {
    TotalAmount();
})