function Increase(e) {
    var control = $("#" + e);
    control.val(parseInt(control.val()) + (control.val() < 1000 ? 1 : 0));
}

function Decrease(e) {
    var control = $("#" + e);
    control.val(parseInt(control.val()) - (control.val() > 1 ? 1 : 0));

}

function NumericOnly(e) {
    var reg = new RegExp('^[0-9]+$');
    if (!reg.test(e.value)) {
        var correctNumbere = e.value.replace(/\D/g, '');

        if (correctNumbere != "") {
            e.value = correctNumbere;
        } else {
            e.value = 1;
        }
    }
}
