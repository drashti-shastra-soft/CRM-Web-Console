$(document).ready(function () {
    $("[data-form]").click(function () {
        if ($(this).data("form") === "frmSaveAddress" && fnValidationSaveAddress()) {
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

    $('#State').change(function () {
        var state = $(this).val()
        if (state) {
            fnBindCity(0, state)
        } else {
            const $cityDropdown = $('#City');
            $cityDropdown.empty();
            $cityDropdown.append($('<option>', {
                value: '',
                text: 'Select City'
            }));
        }
    });

    $('#MarketId').on('change', function () {
        var marketId = $(this).val();
        fnGetMarketAddress(marketId);
    });
    
});

function fnValidationSaveAddress() {
    var isValid = true;

    var line1 = $.trim($("#Line1").val());
    var city = $.trim($("#City").val());
    var state = $.trim($("#State").val());
    var zipCode = $.trim($("#ZipCode").val());


    // line1
    if (line1 == null || line1 == "" || line1 == undefined) {
        $("#Line1").next("span").text("Line 1 is required!");
        isValid = false;
    } else {
        $("#Line1").next("span").text("");
    }

    // city
    if (city == null || city == "" || city == undefined) {
        $("#City").next("span").text("City is required!");
        isValid = false;
    } else {
        $("#City").next("span").text("");
    }

    // State
    if (state == null || state == "" || state == undefined) {
        $("#State").next("span").text("State is required!");
        isValid = false;
    } else {
        $("#State").next("span").text("");
    }

    // ZipCode
    if (zipCode == null || zipCode == "" || zipCode == undefined) {
        $("#ZipCode").next("span").text("ZipCode is required!");
        isValid = false;
    } else {
        $("#ZipCode").next("span").text("");
    }

    return isValid;
}

function fnBindCity(city, state) {
    fnHandleAjaxRequest({
        url: `/Client/GetCityBy?state=${state}`,
        dataType: 'json',
        onSuccess: function (resultData) {
            if (resultData && resultData.Cities) {
                const $cityDropdown = $('#City');
                $cityDropdown.empty();
                $cityDropdown.append($('<option>', {
                    value: '',
                    text: 'Select City'
                }));
                
                resultData.Cities.forEach(function (city) {
                    const intValue = parseInt(city.Value, 10);
                    $cityDropdown.append($('<option>', {
                        value: city.Text,
                        text: city.Text
                    }));
                });

                if (city) {
                    $cityDropdown.val(city);
                }
            }
        }
    });
}

function fnGetMarketAddress(marketId) {
    $('#loading-element').show();
    $.ajax({
        url: '/Client/GetAddressByMarket?marketId=' + marketId,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                $('#State').val(response.State).trigger('change');
                $('#ZipCode').val(response.ZipCode || '');
                $('#City').val(response.City || '');
                $('#Line3').val(response.Line2 || '');
                $('#Line2').val(response.Line1 || '');
            }
            $('#loading-element').hide();
        },
        error: function () {
            alert('Error fetching market address.');
            $('#loading-element').hide();
        }
    });
}