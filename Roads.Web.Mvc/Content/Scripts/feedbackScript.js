var RowState = new Object();


function EditButtonClick(rowNumber) {

    RowState[rowNumber] = new Object();

    RowState[rowNumber]["#settingName_" + rowNumber] = $("#settingName_" + rowNumber).val();
    RowState[rowNumber]["#sortNumber" + rowNumber] = $("#sortNumber" + rowNumber).val();
    RowState[rowNumber]["#description" + rowNumber] = $("#description" + rowNumber).val();

    RowState[rowNumber]["#isNumeric" + rowNumber] = $("#isNumeric" + rowNumber).attr('checked');
    RowState[rowNumber]["#isMandatory" + rowNumber] = $("#isMandatory" + rowNumber).attr('checked');

    RowState[rowNumber]["#isNumericValue" + rowNumber] = $("#isNumeric" + rowNumber).val();
    RowState[rowNumber]["#isMandatoryValue" + rowNumber] = $("#isMandatory" + rowNumber).val();



    $("#settingName_" + rowNumber).removeAttr('disabled');
    $("#settingName_" + rowNumber).attr('class', 'feedBackInputEdit');
    $("#sortNumber" + rowNumber).removeAttr('disabled');
    $("#sortNumber" + rowNumber).attr('class', 'feedBackInputEdit');
    $("#description" + rowNumber).removeAttr('disabled');
    $("#description" + rowNumber).attr('class', 'feedBackInputEdit');
    $("#isNumeric" + rowNumber).removeAttr('disabled');
    $("#isMandatory" + rowNumber).removeAttr('disabled');


    $("#EditButton_" + rowNumber).hide();
    $("#DeleteButton_" + rowNumber).hide();
    $("#SaveButton_" + rowNumber).show();
    $("#CancelButton_" + rowNumber).show();
}


function CancelButtonClick(rowNumber) {


    $("#settingName_" + rowNumber).val(RowState[rowNumber]["#settingName_" + rowNumber]);
    $("#sortNumber" + rowNumber).val(RowState[rowNumber]["#sortNumber" + rowNumber]);
    $("#description" + rowNumber).val(RowState[rowNumber]["#description" + rowNumber]);


    var attr =  RowState[rowNumber]["#isNumeric" + rowNumber];
    if (typeof attr !== typeof undefined && attr !== false)
    {
        //$("#isNumeric" + rowNumber).val(RowState[rowNumber]["#isNumericValue" + rowNumber]);
        //$("#isNumeric" + rowNumber).attr('checked', RowState[rowNumber]["#isNumeric" + rowNumber]);
        $("#isNumeric" + rowNumber).prop('checked', true);
    } else
    {
        $("#isNumeric" + rowNumber).removeAttr('checked');
        $("#isNumeric" + rowNumber).removeAttr('value');
    }

    attr =  RowState[rowNumber]["#isMandatory" + rowNumber];
    if (typeof attr !== typeof undefined && attr !== false)
    {
        //$("#isMandatory" + rowNumber).attr('checked', RowState[rowNumber]["#isMandatory" + rowNumber]);
        //$("#isMandatory" + rowNumber).val( RowState[rowNumber]["#isMandatoryValue" + rowNumber] );
        $("#isMandatory" + rowNumber).prop('checked', true);

    } else
    {
        $("#isMandatory" + rowNumber).removeAttr('checked');
        $("#isMandatory" + rowNumber).removeAttr('value');
    }

    $("#settingName_" + rowNumber).attr('disabled', 'True');
    $("#settingName_" + rowNumber).attr('class', 'feedBackInputNotEdit');

    $("#sortNumber" + rowNumber).attr('disabled', 'True');
    $("#sortNumber" + rowNumber).attr('class', 'feedBackInputNotEdit');

    $("#description" + rowNumber).attr('disabled', 'True');
    $("#description" + rowNumber).attr('class', 'feedBackInputNotEdit');

    $("#isNumeric" + rowNumber).attr('disabled', 'True');
    $("#isMandatory" + rowNumber).attr('disabled', 'True');

    $("#EditButton_" + rowNumber).show();
    $("#DeleteButton_" + rowNumber).show();
    $("#SaveButton_" + rowNumber).hide();
    $("#CancelButton_" + rowNumber).hide();
}


function SaveButtonClick(rowNumber, url) {

    var RowToSave = new Object();


    RowToSave["feedbackItemId"] = rowNumber;
    RowToSave["settingName"] = $("#settingName_" + rowNumber).val();
    RowToSave["sortNumber"] = $("#sortNumber" + rowNumber).val();
    RowToSave["description"] = $("#description" + rowNumber).val();
    RowToSave["isNumeric"] = $("#isNumeric" + rowNumber).attr('checked');
    RowToSave["isMandatory"] = $("#isMandatory" + rowNumber).attr('checked');
    RowToSave["isNumeric"] = $("#isNumeric" + rowNumber).val();
    RowToSave["isMandatory"] = $("#isMandatory" + rowNumber).val();
    RowToSave["feedbackModelId"] = $("#feedbackModelId" + rowNumber).val();

    $.ajax(
    {
        type:"POST",
        url: url,
        data: RowToSave
});

$("#settingName_" + rowNumber).attr('disabled', 'True');
$("#settingName_" + rowNumber).attr('class', 'feedBackInputNotEdit');

$("#sortNumber" + rowNumber).attr('disabled', 'True');
$("#sortNumber" + rowNumber).attr('class', 'feedBackInputNotEdit');

$("#description" + rowNumber).attr('disabled', 'True');
$("#description" + rowNumber).attr('class', 'feedBackInputNotEdit');

$("#isNumeric" + rowNumber).attr('disabled', 'True');
$("#isMandatory" + rowNumber).attr('disabled', 'True');


$("#EditButton_" + rowNumber).show();
$("#DeleteButton_" + rowNumber).show();
$("#SaveButton_" + rowNumber).hide();
$("#CancelButton_" + rowNumber).hide();
}


function DeleteButtonClick(rowNumber,url) {

    var RowToSave = new Object();

    RowToSave["feedbackItemId"] = rowNumber;
    RowToSave["settingName"] = $("#settingName_" + rowNumber).val();
    RowToSave["sortNumber"] = $("#sortNumber" + rowNumber).val();
    RowToSave["description"] = $("#description" + rowNumber).val();
    RowToSave["isNumeric"] = $("#isNumeric" + rowNumber).attr('checked');
    RowToSave["isMandatory"] = $("#isMandatory" + rowNumber).attr('checked');
    RowToSave["isNumeric"] = $("#isNumeric" + rowNumber).val();
    RowToSave["isMandatory"] = $("#isMandatory" + rowNumber).val();
    RowToSave["feedbackModelId"] = $("#feedbackModelId" + rowNumber).val();

    $.ajax(
    {
        type:"POST",
        url: url,
        data: RowToSave
});
$("#tableRow_" + rowNumber).remove();
}


function CreateNewButtonClick() {
    $("#newFedBackItemForm").modal('show');
}


function CancelButtonModalClick() {

    $("#settingName_New").val(null);
    $("#sortNumberNew").val(null);
    $("#descriptionNew").val(null);
    $("#isNumericNew").prop('checked', false);
    $("#isMandatoryNew").prop('checked', false);
    $("#feedbackModelIdNew").val(null);
    $("#newFedBackItemForm").modal('hide');
}

function SaveNewButtonClick(url) {

    var RowToSave = new Object();

    //RowToSave["feedbackItemId"] = 0;
    RowToSave["settingName"] = $("#settingName_New").val();
    RowToSave["sortNumber"] = $("#sortNumberNew").val();
    RowToSave["description"] = $("#descriptionNew").val();
    RowToSave["isNumeric"] = $("#isNumericNew").val();
    RowToSave["isMandatory"] = $("#isMandatoryNew").val();
    RowToSave["feedbackModelId"] = $("#feedbackModelIdNew").val();

    $.ajax(
   {
       type:"POST",
       url: url,
        data: RowToSave
});
$("#newFedBackItemForm").modal('hide');

}
