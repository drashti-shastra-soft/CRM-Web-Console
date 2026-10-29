$(document).ready(function () {
    $("[data-form]").click(function () {
        if ($(this).data("form") === "frmSavePhoneNumber" && fnValidationSaveNumber()) {
            $('#' + $(this).data("form")).submit();
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
        }
    }

    $('#PhoneNumber1').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '')
                    .substring(0, 10)
                    .replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }

            if (val === '+91') {
                field.val('');
            }
        }
    });

    $('#PhoneNumber2').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '')
                    .substring(0, 10)
                    .replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }

            if (val === '+91') {
                field.val('');
            }
        }
    });
    $('#PhoneNumber3').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '')
                    .substring(0, 10)
                    .replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }

            if (val === '+91') {
                field.val('');
            }
        }
    });
})


function fnValidationSaveNumber() {
    var isValid = true;
    var phoneNumber1 = $.trim($("#PhoneNumber1").val());
    var phoneNumber2 = $.trim($("#PhoneNumber2").val());
    var phoneNumber3 = $.trim($("#PhoneNumber3").val());

    //Phone Number
    if (phoneNumber1 == null || phoneNumber1 == "" || phoneNumber1 == undefined) {
        $("#PhoneNumber1").next("span").text("Mobile Number is required!");
        isValid = false;
    } else {
        var digitsOnly = phoneNumber1.replace(/\D/g, '');
        if (digitsOnly.length < 10) {
            $("#PhoneNumber1").next("span").text("Mobile Number must be 10 digits!");
            isValid = false;
        } else {
            $("#PhoneNumber1").next("span").text("");
        }
    }

    // Phone Number 2 (Optional, but must be 10 digits if entered)
    if (phoneNumber2) {
        var digitsOnly2 = phoneNumber2.replace(/\D/g, '');
        if (digitsOnly2.length < 10) {
            $("#PhoneNumber2").next("span").text("Mobile Number must be 10 digits!");
            isValid = false;
        } else {
            $("#PhoneNumber2").next("span").text("");
        }
    } else {
        $("#PhoneNumber2").next("span").text("");
    }

    // Phone Number 3 (Optional, but must be 10 digits if entered)
    if (phoneNumber3) {
        var digitsOnly3 = phoneNumber3.replace(/\D/g, '');
        if (digitsOnly3.length < 10) {
            $("#PhoneNumber3").next("span").text("Mobile Number must be 10 digits!");
            isValid = false;
        } else {
            $("#PhoneNumber3").next("span").text("");
        }
    } else {
        $("#PhoneNumber3").next("span").text("");
    }

    return isValid;
}
