var currentFeedbackId = 0;
var unValidatedElements = [];
var curStateButton = "";
var isFormValid = false;


$("#SubmitFormForAddRoadStep2").validate();

$(function () {
    //changeRouteNodeBtns("0", "grean");

    $('#fb_0').prop("class", "btn btn-grean clrhover btn-lg active");

    $(".txtMult_FB_TextAreaControl_Title_OverallComment").addClass("form-control");

    $('#fb_lbl_1').prop("class", "label clrhover cityPointLabelActive");
    $('#fb_lbl_2').prop("class", "label clrhover cityPointLabelActive");
});

$(window).on('beforeunload', function() {
    if (isFormValid !== true)
        return getTranslationByKey("FB_MessageBox_Warning_DataLostOnRefresh_Content");
});
/*$('[name="btnRouteNode"]').hover(
    function () {
        curStateButton = String($(this).prop('class')).substring(String($(this).prop('class')).lastIndexOf('-') + 1, String($(this).prop('class')).length);
        $(this).prop("class", ".btnRouteNode btn btn-grean ");
        changeRouteNodeLbls(this.id.substring(this.id.lastIndexOf('_') + 1, this.id.length), "grean");
    },
    function () {
        $(this).prop("class", ".btnRouteNode btn btn-" + curStateButton);
        changeRouteNodeLbls(this.id.substring(this.id.lastIndexOf('_') + 1, this.id.length), "darkgrey");
        changeRouteNodeLbls(currentFeedbackId, "grean");
    }
);
*/

function changeRouteNodeLbls(routeNodeId, className) {
    $('#fb_lbl_' + String(parseInt(routeNodeId) + 1)).prop("class", "label " + className + " clrhover ");
    $('#fb_lbl_' + String(parseInt(routeNodeId) + 2)).prop("class", "label " + className + " clrhover");
}

function gotoNextRouteNode(feedbackObj) {
    getValueFromCurrentControlsSetValueToModel();
    if (unValidatedElements.length > 0 && containsRouteObject('fb_' + String(currentFeedbackId),unValidatedElements)) {
        $('#fb_' + String(parseInt(currentFeedbackId))).prop("class", ".btnRouteNode btn btn-danger btn-lg ");
    } else {
        $('#fb_' + String(parseInt(currentFeedbackId))).prop("class", ".btnRouteNode btn btn-darkgrey btn-lg ");

    }
    //changeRouteNodeLbls(currentFeedbackId, "cityPointLabel");
    changeRouteNodeLbls(currentFeedbackId, "cityPointLabel");

    // $(this).prop("class", ".btnRouteNode btn btn-grean");
    $(feedbackObj).prop("class", ".btnRouteNode btn btn-grean btn-lg active");

    //changeRouteNodeLbls(feedbackObj.id.substring(feedbackObj.id.lastIndexOf('_') + 1, feedbackObj.id.length), "cityPointLabelActive");
    changeRouteNodeLbls(feedbackObj.id.substring(feedbackObj.id.lastIndexOf('_') + 1, feedbackObj.id.length), "cityPointLabelActive");
    setValueToControlsFromModel(feedbackObj);
}

function getTranslationByKey(translationKey) {
    return translationsList.filter(function (obj) {
        return obj.key == translationKey;
    })[0].translation;
}

function containsRouteObject(obj, list) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i].feedbackId === obj) {
            return true;
        }
    }
    return false;
}

function containsControlAndRouteObject(control,route, list) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i].feedbackId === route && list[i].controlDivEnds ===control) {
            return true;
        }
    }
    return false;
}

function getValueFromCurrentControlsSetValueToModel() {
    var valuesFromControls = [];
    for (var i = 0; i < nameTranslationKeys.length; i++) {
        getValueFunc = window["getValue_" + nameTranslationKeys[i].NameTranslationKey];
        var funcResult = getValueFunc();
        if ((String(funcResult) == "undefined" || String(funcResult) == "") && nameTranslationKeys[i].IsMandatory == true) {
            if (!containsControlAndRouteObject(nameTranslationKeys[i].NameTranslationKey,'fb_' + String(currentFeedbackId), unValidatedElements))
                unValidatedElements.push({ feedbackId: 'fb_' + String(currentFeedbackId), controlDivEnds: nameTranslationKeys[i].NameTranslationKey });
        } else {
            if (containsControlAndRouteObject(nameTranslationKeys[i].NameTranslationKey,'fb_' + String(currentFeedbackId), unValidatedElements)) {
                unValidatedElements = unValidatedElements.filter(function(el) {
                    return el.feedbackId !== 'fb_' + String(currentFeedbackId)
                        && el.controlDivEnds !== nameTranslationKeys[i].NameTranslationKey;
                });
            }
        }
        valuesFromControls.push(funcResult);
    }
    for (var y = 0; y < valuesFromControls.length; y++) {
        var element = "Feedbacks_" + String(currentFeedbackId) + "__FeedbackValues_" + String(y) + "__Value";
        $('#' + element).val(String(valuesFromControls[y]));
    }
    return true;
}

function setValueToControlsFromModel(feedbackObj) {
    $("#SubmitFormForAddRoadStep2").valid();

    //$('#routeCitiesName').text(feedbackObj.value);
    var value = $(feedbackObj).attr('value');
    $('#routeCitiesName').text(value);

    var valuesFromModel = [];
    for (var y = 0; y < nameTranslationKeys.length; y++) {
        var element = "Feedbacks_" + String(feedbackObj.id.substring(
            feedbackObj.id.lastIndexOf('_') + 1,
            feedbackObj.id.length)) + "__FeedbackValues_" + String(y) + "__Value";
        $('#' + element).val();
        valuesFromModel.push(String($('#' + element).val()));
    }

    for (var i = 0; i < nameTranslationKeys.length; i++) {
        setValueFunc = window["setValue_" + nameTranslationKeys[i].NameTranslationKey];
        if (containsControlAndRouteObject(nameTranslationKeys[i].NameTranslationKey, String(feedbackObj.id), unValidatedElements)) {
            $("div[class$='" + nameTranslationKeys[i].NameTranslationKey + "']").attr('id', "feedBackCustomControl" + (i.toString()));
            $("#feedBackCustomControl" + (i.toString())).addClass('input-validation-error');
        } else {
            $("#feedBackCustomControl" + (i.toString())).removeClass('input-validation-error');
        }
        setValueFunc(valuesFromModel[i]);
    }
    currentFeedbackId = String(feedbackObj.id.substring(feedbackObj.id.lastIndexOf('_') + 1,feedbackObj.id.length));
    checkIfLast();
}

function saveCurrentAndChangeRouteNodeButtonDesign(routeNodeButtonIdGet, routeNodeButtonSet) {
    getValueFromCurrentControlsSetValueToModel();
    if (unValidatedElements.length > 0 && containsRouteObject('fb_' + String(routeNodeButtonIdGet), unValidatedElements)) {
        $('#fb_' + String(parseInt(routeNodeButtonIdGet))).prop("class", ".btnRouteNode btn btn-danger");
    } else {
        $('#fb_' + String(parseInt(routeNodeButtonIdGet))).prop("class", ".btnRouteNode btn btn-darkgrey");
    }
    setValueToControlsFromModel(routeNodeButtonSet);
}

function saveAndFinish() {
    var currentRouteNodeBeforeFinish = currentFeedbackId;
    var routeNodesButtons = $("[name='btnRouteNode']");
    for (var i = 0; i < routeNodesButtons.length; i++) {
        saveCurrentAndChangeRouteNodeButtonDesign(currentFeedbackId, routeNodesButtons[i]);
    }

    saveCurrentAndChangeRouteNodeButtonDesign(currentFeedbackId, routeNodesButtons[routeNodesButtons.length - 1]);
    saveCurrentAndChangeRouteNodeButtonDesign(currentRouteNodeBeforeFinish, routeNodesButtons[currentRouteNodeBeforeFinish]);

    if (unValidatedElements.length == 0
        && $("#SubmitFormForAddRoadStep2").valid()) {
        isFormValid = true;
        $('form[name="SubmitFormForAddRoadStep2"]').submit();
    } else {
        bootbox.alert(getTranslationByKey("FB_MessageBox_Warning_Content"));
    }
}

function saveBeforeNextOrSubmit() {
    if (parseInt(currentFeedbackId) < parseInt(countOfFeedback)-1) {
        $('#fb_'+String(parseInt(currentFeedbackId)+1)).click();
    }
    else {
        $('#fb_'+String(currentFeedbackId)).click();
        if (unValidatedElements.length == 0
            && $("#SubmitFormForAddRoadStep2").valid()) {
            saveAndFinish();
        }
        else {
            bootbox.alert(getTranslationByKey("FB_MessageBox_Warning_Content"));
        }
    }
}

function checkIfLast() {
    if (parseInt(currentFeedbackId) != parseInt(countOfFeedback) - 1) {
        $("#FB_Button_SaveAndNext").text(getTranslationByKey("FB_Button_SaveAndNext"));
    } else {  
        $("#FB_Button_SaveAndNext").text(getTranslationByKey("FB_Button_SaveAndFinish"));
    }
}