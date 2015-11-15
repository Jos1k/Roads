
$(function () {
    for (var i = 1; i < count; i++) {
        $('#tabs-' + i).hide();
        $('#btn-' + i).removeClass("active");
    }
    selectTab(0);
});

function selectTab(id) {
    for (var i = 0; i < count; i++) {
        $('#tabs-' + i).hide();
        $('#btn-' + i).removeClass("active");
        $('#spanlb-' + i).addClass("cityPointLabel");
        $('#spanlb-' + i).removeClass("cityPointLabelActive");
    }

    $('#tabs-' + id).show();
    $('#btn-' + id).addClass("active");
    $('#spanlb-' + id).removeClass("cityPointLabel");
    $('#spanlb-' + id).addClass("cityPointLabelActive");
    $('#spanlb-' + (id + 1)).removeClass("cityPointLabel");
    $('#spanlb-' + (id + 1)).addClass("cityPointLabelActive");

}
