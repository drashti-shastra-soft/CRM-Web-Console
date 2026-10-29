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

    //$('#IFSCode').mask('AAAA0******', {
    //    translation: {
    //        'A': { pattern: /[A-Za-z]/ },
    //        '0': { pattern: /0/ },
    //        '*': { pattern: /[A-Za-z0-9]/, transform: function (c) { return c.toUpperCase(); } }
    //    },
    //    onKeyPress: function (val, e, field, options) {
    //        val = val.toUpperCase(); // Convert to uppercase
    //        field.val(val);
    //    }
    //});
});

function fnValidation() {
    var isValid = true;

    var name = $.trim($("#Name").val());
    var accountType = $.trim($("#AccountType").val());
    var accountNumber = $.trim($("#AccountNumber").val());
    var branchName = $.trim($("#BranchName").val());
    var ifsCode = $.trim($("#IFSCode").val());
    var contactPerson = $.trim($("#ContactPerson").val());
    var brokerage = $.trim($("#Brokerage").val()).replace(/[^\d]/g, '');


    //Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }


    //accountType
    if (accountType == null || accountType == "" || accountType == undefined) {
        $("#AccountType").next("span").text("Account Type is required!");
        isValid = false;
    } else {
        $("#AccountType").next("span").text("");
    }

    //accountNumber
    if (accountNumber == null || accountNumber == "" || accountNumber == undefined) {
        $("#AccountNumber").next("span").text("Account Number is required!");
        isValid = false;
    } else {
        $("#AccountNumber").next("span").text("");
    }

    //branchName
    if (branchName == null || branchName == "" || branchName == undefined) {
        $("#BranchName").next("span").text("Branch Name is required!");
        isValid = false;
    } else {
        $("#BranchName").next("span").text("");
    }

    //IFSCode
    if (ifsCode == null || ifsCode == "" || ifsCode == undefined) {
        $("#IFSCode").next("span").text("IFSCode is required!");
        isValid = false;
    } else if (/\d/.test(ifsCode)) {
        $("#IFSCode").next("span").text("IFSCode must not contain numbers!");
        isValid = false;
    } else {
        $("#IFSCode").next("span").text("");
    }

    return isValid;
}
