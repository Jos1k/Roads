var RowState = new Object();
RowStateDynamicName = new Object();
RowStateDynamicDescription = new Object();

function EditButtonClick(rowNumber, languageCount) {

    RowState[rowNumber] = new Object();

    //Get previous state
    RowState[rowNumber]["#settingName_" + rowNumber] = $("#settingName_" + rowNumber).val();
    RowState[rowNumber]["#sortNumber" + rowNumber] = $("#sortNumber" + rowNumber).val();
    RowState[rowNumber]["#description" + rowNumber] = $("#description" + rowNumber).val();
    RowState[rowNumber]["#isNumeric" + rowNumber] = $("#isNumeric" + rowNumber).attr('checked');
    RowState[rowNumber]["#isMandatory" + rowNumber] = $("#isMandatory" + rowNumber).attr('checked');
    RowState[rowNumber]["#isNumericValue" + rowNumber] = $("#isNumeric" + rowNumber).val();
    RowState[rowNumber]["#isMandatoryValue" + rowNumber] = $("#isMandatory" + rowNumber).val();
    RowState[rowNumber]["#feedbackModelIdList" + rowNumber] = $("#feedbackModelIdList" + rowNumber).val();

    RowStateDynamicName[rowNumber] = [];
    for (var i = 0; i < languageCount; i++) {
        RowStateDynamicName[rowNumber]["#settingNameValue_" + rowNumber + "_" + (i + 1)] = $("#settingNameValue_" + rowNumber + "_" + (i + 1)).val();
    }

    RowStateDynamicDescription[rowNumber] = [];
    for (var i = 0; i < languageCount; i++) {
        RowStateDynamicDescription[rowNumber]["#descriptionValue_" + rowNumber + "_" + (i + 1)] = $("#descriptionValue_" + rowNumber + "_" + (i + 1)).val();
    }

    //Enable edit mode
    $("#settingName_" + rowNumber).removeAttr('disabled');
    $("#settingName_" + rowNumber).attr('class', 'feedBackInputEdit');
    $("#sortNumber" + rowNumber).removeAttr('disabled');
    $("#sortNumber" + rowNumber).attr('class', 'feedBackInputEdit');
    $("#description" + rowNumber).removeAttr('disabled');
    $("#description" + rowNumber).attr('class', 'feedBackInputEdit');
    $("#isNumeric" + rowNumber).removeAttr('disabled');
    $("#isMandatory" + rowNumber).removeAttr('disabled');
    $("#feedbackModelIdList" + rowNumber).removeAttr('disabled');

    for (var i = 0; i < languageCount; i++) {
        $("#settingNameValue_" + rowNumber + "_" + (i + 1)).removeAttr('disabled');
        $("#settingNameValue_" + rowNumber + "_" + (i + 1)).attr('class', 'feedBackInputEdit');
    }

    for (var i = 0; i < languageCount; i++) {
        $("#descriptionValue_" + rowNumber + "_" + (i + 1)).removeAttr('disabled');
        $("#descriptionValue_" + rowNumber + "_" + (i + 1)).attr('class', 'feedBackInputEdit');
    }

    //Button hide/show section
    $("#EditButton_" + rowNumber).hide();
    $("#DeleteButton_" + rowNumber).hide();
    $("#SaveButton_" + rowNumber).show();
    $("#CancelButton_" + rowNumber).show();
}

function CancelButtonClick(rowNumber, languageCount) {

    //Set previous state
    $("#settingName_" + rowNumber).val(RowState[rowNumber]["#settingName_" + rowNumber]);
    $("#sortNumber" + rowNumber).val(RowState[rowNumber]["#sortNumber" + rowNumber]);
    $("#description" + rowNumber).val(RowState[rowNumber]["#description" + rowNumber]);


    var attr = RowState[rowNumber]["#isNumeric" + rowNumber];
    if (typeof attr !== typeof undefined && attr !== false) {
        $("#isNumeric" + rowNumber).prop('checked', true);
    } else {
        $("#isNumeric" + rowNumber).removeAttr('checked');
        $("#isNumeric" + rowNumber).removeAttr('value');
    }

    attr = RowState[rowNumber]["#isMandatory" + rowNumber];
    if (typeof attr !== typeof undefined && attr !== false) {
        $("#isMandatory" + rowNumber).prop('checked', true);

    } else {
        $("#isMandatory" + rowNumber).removeAttr('checked');
        $("#isMandatory" + rowNumber).removeAttr('value');
    }

    for (var i = 0; i < languageCount; i++) {
        $("#settingNameValue_" + rowNumber + "_" + (i + 1)).val(RowStateDynamicName[rowNumber]["#settingNameValue_" + rowNumber + "_" + (i + 1)]);
    }

    for (var i = 0; i < languageCount; i++) {
        $("#descriptionValue_" + rowNumber + "_" + (i + 1)).val(RowStateDynamicDescription[rowNumber]["#descriptionValue_" + rowNumber + "_" + (i + 1)]);
    }

    $("#feedbackModelIdList" + rowNumber).val(RowState[rowNumber]["#feedbackModelIdList" + rowNumber]);

    //Disable edit mode
    $("#settingName_" + rowNumber).attr('disabled', 'True');
    $("#settingName_" + rowNumber).attr('class', 'feedBackInputNotEdit');
    $("#sortNumber" + rowNumber).attr('disabled', 'True');
    $("#sortNumber" + rowNumber).attr('class', 'feedBackInputNotEdit');
    $("#description" + rowNumber).attr('disabled', 'True');
    $("#description" + rowNumber).attr('class', 'feedBackInputNotEdit');
    $("#isNumeric" + rowNumber).attr('disabled', 'True');
    $("#isMandatory" + rowNumber).attr('disabled', 'True');
    $("#feedbackModelIdList" + rowNumber).attr('disabled', 'True');

    for (var i = 0; i < languageCount; i++) {
        $("#settingNameValue_" + rowNumber + "_" + (i + 1)).attr('disabled', 'True');
        $("#settingNameValue_" + rowNumber + "_" + (i + 1)).attr('class', 'feedBackInputNotEdit');
    }

    for (var i = 0; i < languageCount; i++) {
        $("#descriptionValue_" + rowNumber + "_" + (i + 1)).attr('disabled', 'True');
        $("#descriptionValue_" + rowNumber + "_" + (i + 1)).attr('class', 'feedBackInputNotEdit');
    }

    //Button hide/show section
    $("#EditButton_" + rowNumber).show();
    $("#DeleteButton_" + rowNumber).show();
    $("#SaveButton_" + rowNumber).hide();
    $("#CancelButton_" + rowNumber).hide();
    $("#notNumberError").hide();
    $("#emptyMandatoryFields").hide();
}

function SaveButtonClick(rowNumber, languageCount, url) {
    var canSave = true;
    var RowToSave = new Object();
    var RowToSaveDynamicName = [];
    var RowToSaveDynamicDescription = [];

    //Create objects for save
    RowToSave["feedbackItemId"] = rowNumber;
    RowToSave["settingName"] = $("#settingName_" + rowNumber).val();
    
    if ($("#sortNumber" + rowNumber).val().length === 0) {
        $("#sortNumber" + rowNumber).prop('class', 'feedBackInputEditError');
        $("#emptyMandatoryFields").show();
        canSave = false;
    } else
        if ($("#sortNumber" + rowNumber).val() % 1 != 0) {
            canSave = false;
        }
        else {
            RowToSave["sortNumber"] = $("#sortNumber" + rowNumber).val();
            $("#sortNumber" + rowNumber).prop('class', 'feedBackInputEdit');
        }

    RowToSave["description"] = $("#description" + rowNumber).val();
    RowToSave["isNumeric"] = $("#isNumeric" + rowNumber).is(":checked");
    RowToSave["isMandatory"] = $("#isMandatory" + rowNumber).is(":checked");
    RowToSave["feedbackModelId"] = $("#feedbackModelIdList" + rowNumber).val();

    for (var i = 0; i < languageCount; i++) {
        if ($("#settingNameValue_" + rowNumber + "_" + (i + 1)).val().length === 0) {
            $("#settingNameValue_" + rowNumber + "_" + (i + 1)).prop('class', 'feedBackInputEditError');
            $("#emptyMandatoryFields").show();
            canSave = false;
        } else {
            $("#settingNameValue_" + rowNumber + "_" + (i + 1)).prop('class', 'feedBackInputEdit');
            RowToSaveDynamicName.push({
                'Value': $("#settingNameValue_" + rowNumber + "_" + (i + 1)).val(),
                'DynamicKey': $("#settingName_" + rowNumber).val(),
                'LanguageId': i + 1
            });
        }
    }

    for (var i = 0; i < languageCount; i++) {
        if ($("#descriptionValue_" + rowNumber + "_" + (i + 1)).val().length === 0) {
            $("#descriptionValue_" + rowNumber + "_" + (i + 1)).prop('class', 'feedBackInputEditError');
            $("#emptyMandatoryFields").show();
            canSave = false;
        } else {
            $("#descriptionValue_" + rowNumber + "_" + (i + 1)).prop('class', 'feedBackInputEdit');
            RowToSaveDynamicDescription.push({
                'Value': $("#descriptionValue_" + rowNumber + "_" + (i + 1)).val(),
                'DynamicKey': $("#description" + rowNumber).val(),
                'LanguageId': i + 1
            });
        }
    }

    var dynamicTranslation = RowToSaveDynamicName.concat(RowToSaveDynamicDescription);

    if (canSave == true) {
        $.ajax(
        {
            type: "POST",
            url: url,
            contentType: 'application/json',
            data: JSON.stringify({ itemForSave: RowToSave, dynamicTranslationsList: dynamicTranslation }),
            dataType: "JSON"
        });

        //Disable edit mode
        $("#settingName_" + rowNumber).attr('disabled', 'True');
        $("#settingName_" + rowNumber).attr('class', 'feedBackInputNotEdit');
        $("#sortNumber" + rowNumber).attr('disabled', 'True');
        $("#sortNumber" + rowNumber).attr('class', 'feedBackInputNotEdit');
        $("#description" + rowNumber).attr('disabled', 'True');
        $("#description" + rowNumber).attr('class', 'feedBackInputNotEdit');
        $("#isNumeric" + rowNumber).attr('disabled', 'True');
        $("#isMandatory" + rowNumber).attr('disabled', 'True');
        $("#feedbackModelIdList" + rowNumber).attr('disabled', 'True');

        for (var i = 0; i < languageCount; i++) {
            $("#settingNameValue_" + rowNumber + "_" + (i + 1)).attr('disabled', 'True');
            $("#settingNameValue_" + rowNumber + "_" + (i + 1)).attr('class', 'feedBackInputNotEdit');
        }

        for (var i = 0; i < languageCount; i++) {
            $("#descriptionValue_" + rowNumber + "_" + (i + 1)).attr('disabled', 'True');
            $("#descriptionValue_" + rowNumber + "_" + (i + 1)).attr('class', 'feedBackInputNotEdit');
        }

        //Button hide/show section
        $("#EditButton_" + rowNumber).show();
        $("#DeleteButton_" + rowNumber).show();
        $("#SaveButton_" + rowNumber).hide();
        $("#CancelButton_" + rowNumber).hide();
        $("#emptyMandatoryFields").hide();
    }
}

function DeleteButtonClick(rowNumber, url) {

    var RowToSave = new Object();

    RowToSave["feedbackItemId"] = rowNumber;
    RowToSave["settingName"] = $("#settingName_" + rowNumber).val();
    RowToSave["sortNumber"] = $("#sortNumber" + rowNumber).val();
    RowToSave["description"] = $("#description" + rowNumber).val();
    RowToSave["isNumeric"] = $("#isNumeric" + rowNumber).attr('checked');
    RowToSave["isMandatory"] = $("#isMandatory" + rowNumber).attr('checked');
    RowToSave["isNumeric"] = $("#isNumeric" + rowNumber).val();
    RowToSave["isMandatory"] = $("#isMandatory" + rowNumber).val();
    RowToSave["feedbackModelId"] = $("#feedbackModelIdList" + rowNumber).val();

    $.ajax(
    {
        type: "POST",
        url: url,
        data: RowToSave
    });
    $("#tableRow_" + rowNumber).remove();
}

function CreateNewButtonClick() {
    $("#newFedBackItemForm").modal('show');
}

function CancelNewButtonClick(languageCount) {

    $("#settingName_New").val(null);
    $("#sortNumberNew").val(null);
    $("#sortNumberNew").prop('class', 'feedBackInputEdit');
    $("#descriptionNew").val(null);
    $("#isNumericNew").prop('checked', false);
    $("#isMandatoryNew").prop('checked', false);
    $("#feedbackModelIdNew").val(null);
    $("#feedbackModelIdListNew").val(null);

    for (var i = 0; i < languageCount; i++) {
        $("#settingNameValue_New_" + (i + 1)).val(null);
        $("#settingNameValue_New_" + (i + 1)).prop('class', 'feedBackInputEdit');
        $("#descriptionValue_New_" + (i + 1)).val(null);
        $("#descriptionValue_New_" + (i + 1)).prop('class', 'feedBackInputEdit');
    }

    $("#notNumberNewError").hide();
    $("#emptyMandatoryFieldsNew").hide();
    $("#newFedBackItemForm").modal('hide');
}

function SaveNewButtonClick(languageCount, url) {
    var canSave = true;
    var rowToSave = new Object();
    var guid = createGuid();
    var nameDynamicKey = 'FBS_Title_' + guid;
    var descriptionDynamicKey = 'FBS_Description_' + guid;

    rowToSave["settingName"] = nameDynamicKey;
    if ($("#sortNumberNew").val().length === 0) {
        $("#sortNumberNew").prop('class', 'feedBackInputEditError');
        $("#emptyMandatoryFieldsNew").show();
        canSave = false;
    } else
        if ($("#sortNumberNew").val() % 1 != 0) {
            canSave = false;
        }
        else {
            rowToSave["sortNumber"] = $("#sortNumberNew").val();
            $("#sortNumberNew").prop('class', 'feedBackInputEdit');
        }

    rowToSave["description"] = descriptionDynamicKey;
    rowToSave["isNumeric"] = $("#isNumericNew").is(":checked");
    rowToSave["isMandatory"] = $("#isMandatoryNew").is(":checked");
    rowToSave["feedbackModelId"] = $("#feedbackModelIdListNew").val();

    var rowToSaveDynamicName = [];
    var rowToSaveDynamicDescription = [];

    for (var i = 0; i < languageCount; i++) {
        if ($("#settingNameValue_New_" + (i + 1)).val().length === 0) {
            $("#settingNameValue_New_" + (i + 1)).prop('class', 'feedBackInputEditError');
            $("#emptyMandatoryFieldsNew").show();
            canSave = false;
        } else {
            $("#settingNameValue_New_" + (i + 1)).prop('class', 'feedBackInputEdit');
            rowToSaveDynamicName.push({
                'Value': $("#settingNameValue_New_" + (i + 1)).val(),
                'DynamicKey': nameDynamicKey,
                'LanguageId': i + 1
            });
        }
    }

    for (var i = 0; i < languageCount; i++) {
        if ($("#descriptionValue_New_" + (i + 1)).val().length === 0) {
            $("#descriptionValue_New_" + (i + 1)).prop('class', 'feedBackInputEditError');
            $("#emptyMandatoryFieldsNew").show();
            canSave = false;
        } else {
            $("#descriptionValue_New_" + (i + 1)).prop('class', 'feedBackInputEdit');
            rowToSaveDynamicDescription.push({
                'Value': $("#descriptionValue_New_" + (i + 1)).val(),
                'DynamicKey': descriptionDynamicKey,
                'LanguageId': i + 1
            });
        }
    }
    var dynamicTranslation = rowToSaveDynamicName.concat(rowToSaveDynamicDescription);

    if (canSave == true) {
        $.ajax(
            {
                type: "POST",
                url: url,
                contentType: 'application/json',
                data: JSON.stringify({ newFeedbackItemSettings: rowToSave, newDynamicTranslations: dynamicTranslation }),
                success: function (data) {
                    $("#FeedBackItems").submit();
                },
                error: function () {
                    $("#FeedBackItems").submit();
                }
            });
        $("#newFedBackItemForm").modal('hide');
        $("#emptyMandatoryFieldsNew").hide();
    }
}

function createGuid() {
    return 'xxxxxxxx_xxxx_4xxx_yxxx_xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}


function InputNumericOnly(e, errorDiv) {
    if (e.value % 1 == 0) {
        $(e).prop('class', 'feedBackInputEdit');
        $(errorDiv).hide();
    }
    else {
        $(e).prop('class', 'feedBackInputEditError');
        $(errorDiv).show();
    }
}

function CreateNewRow(id) {

//    var result = "";

//    var tableid = "tableRow" + id;
//    var settingNameId = 'settingName_' + id;
//    var settingNameValue = 1;


//    var isChecked = 'checked="checked"';

//    result = result + '<tr id="' + tableid + '">';

//    result = result + '<td>';
//    result = result + '<input class="feedBackInputNotEdit" disabled="True" id="'+settingNameId+'" name="item.settingName" type="text" value="' + settingNameValue + '">';
//    result = result + '</td>';

//    result = result + '<td>';
//    result = result + '<input class="feedBackInputNotEdit" disabled="True" id="'+sortNumberId+'" name="item.sortNumber" type="text" value="' + sortNumberValue + '">';
//    result = result + '</td>';

//    result = result + '<td>';
//    result = result + '<input class="feedBackInputNotEdit" disabled="True" id="'+descriptionId+'" name="item.description" type="text" value="' + descriptionValue + '">';
//    result = result + '</td>';

//    result = result + '<td>';
//    result = result + '<input '+isChecked+'  type="checkbox" class="feedBackInputNotEdit" disabled="True" id="'+isNumericId+'" name="item.isNumeric" type="text" value="' + isNumericValue + '">';
//    result = result + '<input name="item.isNumeric" type="hidden" value="false">';
//    result = result + '</td>';


//    result = result + '<td>';
//    result = result + '<input '+isChecked+'  type="checkbox" class="feedBackInputNotEdit" disabled="True" id="'+isMandatoryId+'" name="item.isMandatory" type="text" value="' + isMandatoryValue + '">';
//    result = result + '<input name="item.isMandatory" type="hidden" value="false">';
//    result = result + '</td>';

//    result = result + '<td>';

//    result = result + '<input '+isChecked+'  type="checkbox" class="feedBackInputNotEdit" disabled="True" id="'+isMandatoryId+'" name="item.isMandatory" type="text" value="' + isMandatoryValue + '">';


//    result = result + '</td>';


//    //    <td>
//    //        <div class="btn btn-default" id="EditButton_37" onclick="EditButtonClick(37)">Edit</div>
//    //        <div class="btn btn-default" id="DeleteButton_37" onclick="DeleteButtonClick(37, '/AdminZone/DeleteFeedback')">Delete</div>
//    //        <div class="btn btn-default" id="SaveButton_37" onclick="SaveButtonClick(37, '/AdminZone/FeedbackSettings')" style="display: none;">Save</div>
//    //        <div class="btn btn-default" id="CancelButton_37" onclick="CancelButtonClick(37)" style="display: none;">Cancel</div>

//    //    </td>
//    //</tr>
}