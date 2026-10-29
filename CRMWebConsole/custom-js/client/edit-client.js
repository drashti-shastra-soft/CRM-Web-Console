$(document).ready(function () {

    $('#MobileNumber').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '').substring(0, 10).replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }

            if (val === '+91') {
                field.val('');
            }
        }
    });

    $('#EmailAddress').on('input', function () {
        let val = $(this).val();

        // Allow only valid email characters
        val = val.replace(/[^a-zA-Z0-9@._\-]/g, '');

        // Prevent double @ symbols
        const parts = val.split('@');
        if (parts.length > 2) {
            val = parts[0] + '@' + parts[1];
        }

        $(this).val(val);
    });

    $('#AadharNumber').mask('0000 0000 0000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        }
    });

    $('#PANNumber').mask('AAAAA0000A', {
        translation: {
            'A': { pattern: /[A-Za-z]/ },
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            val = val.toUpperCase(); // Convert to uppercase
            field.val(val);
        }
    });

    $('#TANNumber').mask('AAAAA0000A', {
        translation: {
            'A': { pattern: /[A-Za-z]/ },
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            val = val.toUpperCase(); // Convert to uppercase
            field.val(val);
        }
    });

    $('#GSTNumber').mask('00AAAAA0000A0A*', {
        translation: {
            '0': { pattern: /[0-9]/ },
            'A': { pattern: /[A-Za-z]/ },
            '*': { pattern: /[A-Za-z0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            val = val.toUpperCase(); // Convert to uppercase
            field.val(val);
        }
    });

    // Auto-fill PAN Number based on GST Number
    $('#GSTNumber').on('blur keyup', function () {
        var gst = $(this).val().trim();

        if (gst.length === 15) {
            var pan = gst.substring(2, 12).toUpperCase();
            $('#PANNumber').val(pan);
        } else {
            $('#PANNumber').val('');
        }
    });


    //$(".btn-submit").click(function () {
    //    if (fnValidation()) {
    //        $('#frmSaveClient').submit();
    //    }
    //});

    $("[data-form]").click(function () {
        if ($(this).data("form") === "frmSaveClient" && fnValidation()) {
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
            $('#frmList').submit();
        }
    }
});

function fnValidation() {
    var isValid = true;

    
    var emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;
    var brand = $.trim($("#Brand").val());
    var dyeingSupervisorId = $.trim($("#DyeingSupervisorId").val());
    var collectionAgentId = $.trim($("#CollectionAgentId").val());
    var clientTypeId = $.trim($("#ClientTypeId").val());
    var name = $.trim($("#Name").val());
    var emailAddress = $.trim($("#EmailAddress").val());
    var mobileNumber = $.trim($("#MobileNumber").val());
    var gstNumber = $.trim($("#GSTNumber").val());
    var panNumber = $.trim($("#PANNumber").val());
    var aadharNumber = $.trim($("#AadharNumber").val());
    var tanNumber = $.trim($("#TANNumber").val());
    var creditLimit = $.trim($("#CreditLimit").val());
    var paymentTerms = $.trim($("#PaymentTerms").val());
    var contactPerson = $.trim($("#ContactPerson").val());



    // contactPerson
    if (contactPerson == null || contactPerson == "" || contactPerson == undefined) {
        $("#ContactPerson").next("span").text("Contact Person is required!");
        isValid = false;
    } else {
        $("#ContactPerson").next("span").text("");
    }

    // dyeingSupervisorId
    if (dyeingSupervisorId == null || dyeingSupervisorId == "" || dyeingSupervisorId == undefined) {
        $("#DyeingSupervisorId").next("span").text("Dyeing Supervisor is required!");
        isValid = false;
    } else {
        $("#DyeingSupervisorId").next("span").text("");
    }


    // collectionAgentId
    if (collectionAgentId == null || collectionAgentId == "" || collectionAgentId == undefined) {
        $("#CollectionAgentId").next("span").text("Collection Agent is required!");
        isValid = false;
    } else {
        $("#CollectionAgentId").next("span").text("");
    }

    // clientTypeId
    if (clientTypeId == null || clientTypeId == "" || clientTypeId == undefined) {
        $("#ClientTypeId").next("span").text("Client Type is required!");
        isValid = false;
    } else {
        $("#ClientTypeId").next("span").text("");
    }

    // Brand
    if (brand == null || brand == "" || brand == undefined) {
        $("#Brand").next("span").text("Brand Name is required!");
        isValid = false;
    } else {
        $("#Brand").next("span").text("");
    }

    // name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    if (gstNumber == null || gstNumber == "" || gstNumber == undefined) {
        $("#GSTNumber").next("span").text("GST Number is required!");
        isValid = false;
    } else if (gstNumber.length < 15) {
        $("#GSTNumber").next("span").text("GST Number must be 15 characters!");
        isValid = false;
    } else {
        $("#GSTNumber").next("span").text("");
    }

    if (panNumber == null || panNumber == "" || panNumber == undefined) {
        $("#PANNumber").next("span").text("PAN Number is required!");
        isValid = false;
    } else if (panNumber.length < 10) {
        $("#PANNumber").next("span").text("PAN Number must be 10 characters!");
        isValid = false;
    } else {
        $("#PANNumber").next("span").text("");
    }

    if (creditLimit == null || creditLimit == "" || creditLimit == undefined) {
        $("#CreditLimit").next("span").text("Credit Limit is required!");
        isValid = false;
    } else {
        $("#CreditLimit").next("span").text("");
    }

    if (paymentTerms == null || paymentTerms == "" || paymentTerms == undefined) {
        $("#PaymentTerms").next("span").text("Payment Terms is required!");
        isValid = false;
    } else {
        $("#PaymentTerms").next("span").text("");
    }

    // Email Address
    if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
        $("#EmailAddress").next("span").text("Email Address   is required!");
        isValid = false;
    } else {
        $("#EmailAddress").next("span").text("");
    }

    // Mobile Number
    if (mobileNumber == null || mobileNumber == "" || mobileNumber == undefined) {
        $("#MobileNumber").next("span").text("Mobile Number is required!");
        isValid = false;
    } else if (mobileNumber.replace(/\D/g, "").length < 12) {
        // Remove non-digits and check length (12 = +91 + 10 digits)
        $("#MobileNumber").next("span").text("Mobile Number must be 10 digits!");
        isValid = false;
    } else {
        $("#MobileNumber").next("span").text("");
    }

    return isValid;
}

