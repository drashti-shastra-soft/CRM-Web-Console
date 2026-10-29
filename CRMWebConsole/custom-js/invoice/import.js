$(document).ready(function () {
    $("#importFile").change(function () {

        var fileUpload = $("#importFile").get(0);
        var files = fileUpload.files;

        if (files.length === 0) return;

        var fileData = new FormData();
        fileData.append("file", files[0]);
        fnHandleAjaxRequest({
            url: "/Invoice/Import",
            method: "POST",
            dataType: "json", // assuming JSON response
            contentType: false,
            processData: false,
            data: fileData,
            onSuccess: function (resultData) {
                var $tbody = $("#tblImportInvoice tbody");
                $tbody.empty();
                if (resultData) {
                    var validationCount = 0;

                    $.each(resultData, function (index, item) {
                        var errorHtml = "";
                        if (item.Validation && item.Validation.length > 0) {
                            errorHtml = `<ol>`;
                            $.each(item.Validation, function (i, msg) {
                                errorHtml += `<li>${msg.replace(/"/g, '&quot;')}</li>`;
                            });
                            errorHtml += `</ol>`;
                            validationCount = item.Validation.length;
                        }
                        var row = `
                                <tr id="tr_${index}" class="main-row">
                                    <td>
                                        <i class="fa fa-plus cursor-pointer" onclick="ExpandRow(${index}, this)"></i>
                                    </td>
                                    <td class="text-danger">${errorHtml}</td>
                                    <td>${item.RawDate}</td>
                                    <td>${item.BookType}</td>
                                    <td>${item.ClientName}</td>
                                    <td>${item.InvoiceNumber}</td>
                                    <td>${item.ChallanNumber}</td>
                                    <td>${item.LRNumber}</td>
                                    <td>${item.VehicleNumber}</td>
                                    <td>${item.DyeingSupervisorName}</td>
                                    <td>${item.CollectionAgentName}</td>
                                    <td>${item.BrokerName}</td>
                                    <td>${item.TaxableValue}</td>
                                    <td>${item.SGST}</td>
                                    <td>${item.CGST}</td>
                                    <td>${item.IGST}</td>
                                    <td>${item.NetAmount}</td>
                                    <td hidden>${item.ClientId}</td>
                                    <td hidden>${item.DyeingSupervisorId}</td>
                                    <td hidden>${item.CollectionAgentId}</td>
                                    <td hidden>${item.BrokerId}</td>
                                </tr>
                                <tr style="display:none;" class="inner-row child-row child-${index}">
                                    <td colspan="21">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Item Name</th>
                                                    <th>Style</th>
                                                    <th>Lot Number</th>
                                                    <th>Grey Meter</th>
                                                    <th>Piece</th>
                                                    <th>Part Piece</th>
                                                    <th>Grey Value</th>
                                                    <th>Finish Meter</th>
                                                    <th>Shortage</th>
                                                    <th>Rate</th>
                                                    <th>Amount</th>
                                                    <th>Add To Total</th>
                                                    <th>Subtract From Total</th>
                                                    <th hidden>ItemId</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyChild${index}">
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            `;
                        $tbody.append(row);
                        // Append child rows                        
                        if (item.LineItems && item.LineItems.length > 0) {
                            var $childTbody = $(`#tbodyChild${index}`);
                            $.each(item.LineItems, function (i, line) {
                                var validationHtml = "";
                                if (line.Validation && line.Validation.length > 0) {
                                    validationHtml = `<ol>`;
                                    $.each(line.Validation, function (i, message) {
                                        validationHtml += `<li>${message.replace(/"/g, '&quot;')}</li>`;
                                    });
                                    validationHtml += `</ol>`;
                                }
                                var childRow = `
                                        <tr>
                                            <td class="text-danger">${validationHtml}</td>
                                            <td>${line.ItemName}</td>
                                            <td>${line.Style}</td>
                                            <td>${line.LotNumber}</td>
                                            <td>${line.GreyMeter}</td>
                                            <td>${line.Piece}</td>
                                            <td>${line.PartPiece}</td>
                                            <td>${line.GreyValue}</td>
                                            <td>${line.FinishMeter}</td>
                                            <td>${line.Shortage}</td>
                                            <td>${line.Rate}</td>
                                            <td>${line.Amount}</td>
                                            <td>${line.AddToTotal}</td>
                                            <td>${line.SubtractFromTotal}</td>
                                            <td hidden>${line.ItemId}</td>
                                        </tr>`;
                                $childTbody.append(childRow);
                            });
                        }
                    });

                    if (parseInt(validationCount) == 0) {
                        $('#btn-upload').removeClass('btn-default').addClass('btn-success');
                    }
                }
            },
            onError: function () {
                showToastr("Failed to import invoice!");
            }
        });

    });

    $('#btn-upload').on('click', function () {

        if ($(this).hasClass('btn-default')) return;

        toggleLoading(true);
        setTimeout(function () {
            const rows = $("#tblImportInvoice tr.main-row");
            const totalRecord = rows.length;
            let processedRecords = 0;
            rows.each(function (index) {
                const $row = $(this);
                const loaderTD = $row.find('td:eq(1)');
                loaderTD.html(`<img src='/Content/assets/img/loader.gif' style='max-width:50px;height:40px;'>`);
                const getCell = (i) => $row.find(`td:eq(${i})`).html();
                const mInvoice = {
                    Date: getCell(2),
                    BookType: getCell(3),
                    ClientName: getCell(4),
                    InvoiceNumber: getCell(5),
                    ChallanNumber: getCell(6),
                    LRNumber: getCell(7),
                    VehicleNumber: getCell(8),
                    DyeingSupervisorName: getCell(9),
                    CollectionAgentName: getCell(10),
                    BrokerName: getCell(11),
                    TaxableValue: getCell(12),
                    SGST: getCell(13),
                    CGST: getCell(14),
                    IGST: getCell(15),
                    NetAmount: getCell(16),
                    ClientId: getCell(17),
                    DyeingSupervisorId: getCell(18),
                    CollectionAgentId: getCell(19),
                    BrokerId: getCell(20),
                    LineItems: []
                };
                $(`#tbodyChild${index} tr`).each(function () {
                    const $line = $(this);
                    const getLineCell = (i) => $line.find(`td:eq(${i})`).html();
                    mInvoice.LineItems.push({
                        ItemName: getLineCell(1),
                        Style: getLineCell(2),
                        LotNumber: getLineCell(3),
                        GreyMeter: getLineCell(4),
                        Piece: getLineCell(5),
                        PartPiece: getLineCell(6),
                        GreyValue: getLineCell(7),
                        FinishMeter: getLineCell(8),
                        Shortage: getLineCell(9),
                        Rate: getLineCell(10),
                        Amount: getLineCell(11),
                        AddToTotal: getLineCell(12),
                        SubtractFromTotal: getLineCell(13),
                        ItemId: getLineCell(14)
                    });
                });

                $.ajax({
                    url: '/Invoice/Create',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(mInvoice),
                    success: function (result) {
                        processedRecords++;
                        const isSuccess = result?.type === "success";
                        const message = result?.message || (isSuccess ? "Accepted" : "Something went wrong!");

                        loaderTD
                            .removeClass("text-danger alert alert-success alert-danger")
                            .addClass(isSuccess ? "alert alert-success" : "alert alert-danger")
                            .html($.trim(message));

                        if (processedRecords === totalRecord) toggleLoading(false);
                    },
                    error: function () {
                        processedRecords++;
                        loaderTD
                            .removeClass("text-danger alert alert-success")
                            .addClass("alert alert-danger")
                            .html("Something went wrong!");

                        if (processedRecords === totalRecord) toggleLoading(false);
                    }
                });
            });
        }, 3000);
    });
});