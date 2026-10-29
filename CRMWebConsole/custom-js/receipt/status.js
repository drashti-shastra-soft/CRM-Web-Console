$(document).ready(function () {
    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmStatus').submit();
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
});

function fnValidation() {
    var isValid = true;
    var status = $.trim($("#Status").val());

    //Status
    if (status == null || status == "" || status == undefined) {
        $("#Status").next("span").text("Status is required!");
        isValid = false;
    } else {
        $("#Status").next("span").text("");
    }

    return isValid;
}
