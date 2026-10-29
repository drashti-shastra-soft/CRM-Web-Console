$(document).ready(function () {
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

    $('#ClientId').on('change', function () {
        var clientId = parseInt($(this).val());
        if (clientId > 0) {
            fnInvoicesBy(clientId);
        }
    });

    const now = new Date();
    const dateStr = now.toISOString().split('T')[0];
    const id = parseInt($('#Id').val());

    if (id <= 0) {
        $('#VoucherDate').val(dateStr);
    }

    if (id <= 0) {
        $('#ChequeDate').val(dateStr);
    }

    if (parseInt($('#VoucherNumber').val()) <= 0) {
        $('#VoucherNumber').val('');
    }
});

$(document).on('keyup', '.payment-amount', function () {
    var total = 0;

    $('.payment-amount').each(function () {
        var val = parseFloat($(this).val()) || 0;
        total += val;
    });

    $('#spnTotalAmount').text(total.toFixed(2));
});

$(document).on('click', '.btn-submit', function () {
    if (fnValidation()) {
        const payload = {
            Id: $('#Id').val(),
            VoucherNumber: $('#VoucherNumber').val(),
            CollectionAgentId: $('#CollectionAgentId').val(),
            ClientId: $('#ClientId').val(),
            VoucherDate: $('#VoucherDate').val(),
            Method: $('#Method').val(),
            TotalPayment: ($("#TotalPayment").val() || "").toString().replace(/,/g, ''),
            CashDiscount: ($('#CashDiscount').val() || "").toString().replace(/,/g, ''),
            TDS: ($('#TDS').val() || "").toString().replace(/,/g, ''),
            RateDifference: ($('#RateDifference').val() || "").toString().replace(/,/g, ''),
            ClaimAmount: ($('#ClaimAmount').val() || "").toString().replace(/,/g, ''),
            FinalAmount: ($('#FinalAmount').val() || "").toString().replace(/,/g, ''),
            BankId: $('#BankId').val(),
            ReferenceNumber: $('#ReferenceNumber').val(),
            ChequeDate: $('#ChequeDate').val(),
            Status: ($('#Status').val().val() || "").toString().replace(/,/g, ''),
            ReceiptAllocations: CollectInvoicePayment(),
        }

        var requestData = JSON.stringify(payload);



        $.ajax({
            url: '/Receipt/Create',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: requestData, beforeSend: function () {
                $('#loading-element').show();
            },
            success: function (response) {
                if (response) {
                    $('.modal').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    showToastr(response.NotificationType, response.NotificationMsg);
                    $('#frmList').submit();
                }
            }, error: function () {
                $('#loading-element').hide();
            }
        })
    }
});

$(document).on('input', '#RateDifference', function () {
    calculateTotalPayment();

    // Save cursor position
    var input = this;
    var selectionStart = input.selectionStart;
    var selectionEnd = input.selectionEnd;

    // Get raw value (remove commas)
    var rawValue = $(this).val().replace(/,/g, '');
    var numValue = parseFloat(rawValue);

    if (!isNaN(numValue)) {
        // Format with commas and 2 decimal places
        var formattedValue = numValue.toLocaleString('en-IN', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });

        // Update value
        $(this).val(formattedValue);

        // Restore cursor position
        input.setSelectionRange(selectionStart, selectionEnd);
    }
});

$(document).on('input', '#ClaimAmount', function () {
    calculateTotalPayment();

    // Save cursor position
    var input = this;
    var selectionStart = input.selectionStart;
    var selectionEnd = input.selectionEnd;

    // Get raw value (remove commas)
    var rawValue = $(this).val().replace(/,/g, '');
    var numValue = parseFloat(rawValue);

    if (!isNaN(numValue)) {
        // Format with commas and 2 decimal places
        var formattedValue = numValue.toLocaleString('en-IN', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        });

        // Update value
        $(this).val(formattedValue);

        // Restore cursor position
        input.setSelectionRange(selectionStart, selectionEnd);
    }
});

$(document).on('change', '.invoice-checkbox', function () {
    calculateTotalPayment();
});

function CollectInvoicePayment() {
    const mReceiptAlloctions = [];

    $('.invoice-checkbox').each(function () {

        var checkbox = $(this);

        if (checkbox.is(':checked')) {
            var mAllocation = {};
            mAllocation.InvoiceId = $(this).data('invoice-id');
            mAllocation.Amount = $(this).data('amount');
            mReceiptAlloctions.push(mAllocation);
        }
    });

    return mReceiptAlloctions;
}

function fnValidation() {
    var isValid = true;

    var voucherNumber = $.trim($("#VoucherNumber").val());
    var clientId = $.trim($("#ClientId").val());
    var collectionAgentId = $.trim($("#CollectionAgentId").val());
    var voucherDate = $.trim($("#VoucherDate").val());
    var method = $.trim($("#Method").val());
    var referenceNumber = $.trim($("#ReferenceNumber").val());
    var status = $.trim($("#Status").val());


    //Voucher Number
    if (voucherNumber == null || voucherNumber == "" || voucherNumber == undefined) {
        $("#VoucherNumber").next("span").text("Voucher Number is required!");
        isValid = false;
    } else {
        $("#VoucherNumber").next("span").text("");
    }

    //Collection Agent
    if (collectionAgentId == null || collectionAgentId == "" || collectionAgentId == undefined) {
        $("#CollectionAgentId").next("span").text("Collection Agent is required!");
        isValid = false;
    } else {
        $("#CollectionAgentId").next("span").text("");
    }

    //ClientId
    if (clientId == null || clientId == "" || clientId == undefined) {
        $("#ClientId").next("span").text("Client is required!");
        isValid = false;
    } else {
        $("#ClientId").next("span").text("");
    }

    //voucher Date
    if (voucherDate == null || voucherDate == "" || voucherDate == undefined) {
        $("#VoucherDate").next("span").text("Voucher Date is required!");
        isValid = false;
    } else {
        $("#VoucherDate").next("span").text("");
    }

    //Method
    if (method == null || method == "" || method == undefined) {
        $("#Method").next("span").text("Method is required!");
        isValid = false;
    } else {
        $("#Method").next("span").text("");
    }

    //Status
    if (status == null || status == "" || status == undefined) {
        $("#Status").next("span").text("Status is required!");
        isValid = false;
    } else {
        $("#Status").next("span").text("");
    }

    //Reference Number
    if (method != "Cash") {
        if (referenceNumber == null || referenceNumber == "" || referenceNumber == undefined) {
            $("#ReferenceNumber").next("span").text("Reference Number is required!");
            isValid = false;
        } else {
            $("#ReferenceNumber").next("span").text("");
        }
    } else {
        $("#ReferenceNumber").next("span").text("");
    }
    return isValid;
}

function fnInvoicesBy(clientId) {
    $('#loading-element').show();
    $.ajax({
        url: '/Receipt/GetInvoicesBy?clientId=' + clientId,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            var $tbody = $("#tblInvoices tbody");
            $tbody.empty();
            if (response) {
                if (response.length > 0) {
                    $.each(response, function (index, item) {
                        //invoice date
                        const invoiceDate = parseJsonDate(item.Date);

                        // Normalize both dates to midnight
                        const today = new Date();
                        today.setHours(0, 0, 0, 0);
                        invoiceDate.setHours(0, 0, 0, 0);

                        // Calculate difference in days
                        const diffDays = Math.floor((today - invoiceDate) / (1000 * 60 * 60 * 24));
                        var row = `
                                <tr id="tr_${index}">
                                    <td><input type="checkbox" class="invoice-checkbox" data-invoice-id="${item.Id}" data-amount="${item.AmountDue}"/></td>
                                    <td>${parseJsonDate(item.Date).toLocaleDateString()}</td>
                                    <td>${item.InvoiceNumber}</td>
                                    <td>${item.NetAmount}</td>
                                    <td>${item.AmountDue}</td>
                                    <td>${diffDays}</td>
                                </tr>
                            `;
                        $tbody.append(row);
                    });
                } else {
                    var row = `
                                <tr>                                    
                                    <td colspan="5" class="text-danger text-center">There is currently no content to display</td>
                                </tr>
                            `;
                    $tbody.append(row);
                }
            }
            $('#loading-element').hide();
        }, error: function () {
            $('#loading-element').hide();
        }
    })
}

function calculateTotalPayment() {
    var total = 0;
    $(".invoice-checkbox:checked").each(function () {
        total += parseFloat($(this).data("amount")) || 0;
    });

    // Total Payment
    $("#TotalPayment").val(
        total.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
    );

    // Calculate 3% discount
    var discount = total * 0.03;
    $("#CashDiscount").val(
        discount.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
    );

    // Calculate 2% TDS
    var tds = total * 0.02;
    $("#TDS").val(
        tds.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
    );

    // Rate Difference entered by User
    var rateDiff = parseFloat($("#RateDifference").val().replace(/,/g, '')) || 0;

    // Claim Amount entered by User
    var claimDiff = parseFloat($("#ClaimAmount").val().replace(/,/g, '')) || 0;

    // Calculate Final Amount
    var finalAmount = total - discount - tds - rateDiff - claimDiff;
    $("#FinalAmount").val(
        finalAmount.toLocaleString('en-IN', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
    );
}
