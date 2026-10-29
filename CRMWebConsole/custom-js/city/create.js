$(document).ready(function () {
    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmCreate').submit();
        }
    });

    var notificationType = $.trim($('#NotificationType').val());
    var notificationMsg = $.trim($('#NotificationMsg').val());

    if (notificationType != null && notificationType != undefined && notificationType != '') {
        if (notificationMsg != null && notificationMsg != undefined && notificationMsg != '') {

            showToastr(notificationType, notificationMsg)
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }

    $('#Name').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

});

function fnValidation() {
    var isValid = true;

    var name = $.trim($("#Name").val());
    var stateId = $.trim($("#StateId").val());

    //Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    //State
    if (stateId == null || stateId == "" || stateId == undefined) {
        $("#StateId").next("span").text("State Code is required!");
        isValid = false;
    } else {
        $("#StateId").next("span").text("");
    }

    return isValid;
}