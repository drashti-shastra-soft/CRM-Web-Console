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

    $('#Code').mask('00', {
        translation: {
            '0': { pattern: /[0-9]/ }
        }
    });

    if (parseInt($('#Code').val()) <= 0) {
        $('#Code').val('');
    }
});

function fnValidation() {
    var isValid = true;

    var name = $.trim($("#Name").val());
    var code = $.trim($("#Code").val());

    //Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    //Code
    if (code == null || code == "" || code == undefined || code === 0) {
        $("#Code").next("span").text("Code is required!");
        isValid = false;
    } else {
        $("#Code").next("span").text("");
    }

    return isValid;
}