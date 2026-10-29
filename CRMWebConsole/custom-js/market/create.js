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

    $('#Rank').blur(function () {
        var rank = $.trim($('#Rank').val());
        if (rank == null || rank == "" || rank == undefined) {
            $("#Rank").next("span").text("Rank  is required!");
        }
        else {
            CheckRankExist(rank);
        }
    });



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

    if (parseInt($('#Rank').val()) <= 0) {
        $('#Rank').val('');
    }

    $('#Name').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });


    $("#ZipCode").inputmask("000000");
});

function fnValidation() {
    var isValid = true;

    var name = $.trim($("#Name").val());
    var line1 = $.trim($("#Line1").val());
    var state = $.trim($("#State").val());
    var city = $.trim($("#City").val());
    var zipCode = $.trim($("#ZipCode").val());
    var rank = $.trim($("#Rank").val());

    //Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    //Line1
    if (line1 == null || line1 == "" || line1 == undefined) {
        $("#Line1").next("span").text("Line1 is required!");
        isValid = false;
    } else {
        $("#Line1").next("span").text("");
    }

    //State
    if (state == null || state == "" || state == undefined) {
        $("#State").next("span").text("State is required!");
        isValid = false;
    } else {
        $("#State").next("span").text("");
    }

    //City
    if (city == null || city == "" || city == undefined) {
        $("#City").next("span").text("City is required!");
        isValid = false;
    } else {
        $("#City").next("span").text("");
    }

    //Zip Code
    if (zipCode == null || zipCode == "" || zipCode == undefined) {
        $("#ZipCode").next("span").text("Zip Code is required!");
        isValid = false;
    } else {
        $("#ZipCode").next("span").text("");
    }

    //Rank
    if (rank == null || rank == "" || rank == undefined) {
        $("#Rank").next("span").text("Rank is required!");
        isValid = false;
    } else {
        $("#Rank").next("span").text("");
    }

    return isValid;
}

function fnBindCity(city, state) {
    fnHandleAjaxRequest({
        url: `/Market/GetCityBy?state=${state}`,
        dataType : 'json',
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

function CheckRankExist(rank) {
    var mMarket = {};
    mMarket.Id = parseInt($('#Id').val());
    mMarket.Rank = rank;

    fnHandleAjaxRequest({
        url: `/Market/CheckRankExist`,
        method: 'POST',
        dataType: 'json',
        data: JSON.stringify(mMarket),
        onSuccess: function (response) {
            if (response) {
                if (response.StatusCode === "Ok") {
                    if (response.IsExist === true) {
                        $('.btn-submit').removeClass('btn-submit').addClass('disabled-link');
                        $("#Rank").next("span").text("Rank already in use. Choose another.");
                    }
                    else if (response.IsExist === false) {
                        $('.disabled-link').removeClass('disabled-link').addClass('btn-submit');
                        $("#Rank").next("span").text("");
                    }
                } else {

                }
            }
        },
        onError: function () {
            showToastr("Failed to check duplicate user!");
        }
    });
}