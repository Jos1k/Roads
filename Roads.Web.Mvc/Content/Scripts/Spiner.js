function Increase(e) {
    $("#" + e).val(parseInt($("#" + e).val(), 10) + 1);
}

function Decrease(e) {
    $("#" + e).val(parseInt($("#" + e).val(), 10) - 1);
}

function NumericOnly(e) {
    var reg = new RegExp('^[0-9]+$');
    if (!reg.test(e.value)) {
        var correctNumbere = e.value.replace(/\D/g, '');

        if (correctNumbere != "") {
            e.value = correctNumbere;
        } else {
            e.value = 0;
        }
    }
}