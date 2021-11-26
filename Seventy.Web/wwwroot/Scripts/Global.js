var additionalData;
function setDataTable(dataTableId, dataSourceUrl, dataFunction, columns, columnDefs, order, createdRow) {
    if (order == undefined)
        order = [[0, 'desc']];
    var table = $('#' + dataTableId).DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        orderMulti: true,
        pageLength: 5,
        createdRow: createdRow,
        language: datatable_language,
        ajax: {
            url: dataSourceUrl,
            type: 'POST',
            datatype: "json",
            data: (dataFunction != "") ? dataFunction : function (d) { },

            dataFilter: function (data) {
                var json = jQuery.parseJSON(data);
                additionalData = json.additionalData;
                return JSON.stringify(json); // return JSON string
            }
        },
        processing: true,
        lengthMenu: [5, 10, 25, 50],
        lengthChange: true,
        columns: columns,
        columnDefs: columnDefs,
        dom: "<'row'<'col-sm-6'f><'col-sm-6 mt-1'l>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        order: order
    });
    return table;
}
function setDataTableWidoutSearch(dataTableId, dataSourceUrl, dataFunction, columns, columnDefs, order, createdRow) {
    if (order == undefined)
        order = [[0, 'desc']];
    var table = $('#' + dataTableId).DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        orderMulti: true,
        searching: false,
        pageLength: 5,
        createdRow: createdRow,
        language: datatable_language,
        ajax: {
            url: dataSourceUrl,
            type: 'POST',
            datatype: "json",
            data: (dataFunction != "") ? dataFunction : function (d) { },

            dataFilter: function (data) {
                if (data) {
                    var json = jQuery.parseJSON(data);
                    additionalData = json.additionalData;
                    return JSON.stringify(json); // return JSON string
                }
                return data;
            }
        },
        processing: true,
        lengthMenu: [5, 10, 25, 50],
        lengthChange: true,
        columns: columns,
        columnDefs: columnDefs,
        order: order
    });
    return table;
}

function reloadTable(table, resetPaging, callback) {
    table.ajax.reload(callback, resetPaging);
}
function reloadTable(table, resetPaging) {
    table.ajax.reload(null, resetPaging);
}

function successSwal(title, message) {
    swal(title, message, "success");
}
function successSwal(message) {
    swal("انجام شد", message, "success");
}
function errorSwal(title, message) {
    swal(title, message, "error");
}
function errorSwal(message) {
    swal("خطا", message, "error");
}
function deleteSwal(callback) {
    swal({
        title: 'جهت حذف این اطلاعات مطمئن هستید؟',
        text: 'شما نمی توانید این را برگردانید!',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ، آن را حذف میکنم!',
        cancelButtonText: 'منصرف شدم'
    }).then((result) => {
        if (result.value) {
            callback(arguments[1]);
        }
    });
}
function editSwal(callback) {
    swal({
        title: 'جهت ویرایش این اطلاعات مطمئن هستید؟',
        text: 'شما نمی توانید این را برگردانید!',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ، آن را ویرایش میکنم!',
        cancelButtonText: 'منصرف شدم',
    }).then((result) => {
        if (result.value) {
            callback(arguments[1]);
        }
    });
}

async function postDelete(url, id, table) {
    $.ajax({
        type: "POST",
        url: url,
        data: {
            ID: id
        },
        success: function (data) {
            if (data) {
                successSwal(data.statusText);
                reloadTable(table, false);
            }
            else {
                errorSwal(data.statusText);
            }
        }, error: function (data) {
            errorSwal(data.statusText);
        }
    });
}
function showAlert() {
    var alertClass = ".haftad-alert";
    var haftad_alert = $(alertClass)[0];
    var type = $(haftad_alert).attr("type");
    var title = $(haftad_alert).attr("title");
    $(haftad_alert).html(`<div class="alert alert-${type} alert-dismissible fade show p-2 col-12" role="alert">
                                                                <strong>${title}</strong>
                                                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                                    <span aria-hidden="true">×</span>
                                                                </button>
                                                            </div>`);
    $(haftad_alert).fadeOut(5000)
}
function loadNotifs(isRead) {
    $("#UnreadNotif").hide();
    $(".ft-bell").removeClass("bell-shake");
    $.ajax({
        type: "GET",
        url: "/getNotifications",
        data: { "isRead": isRead },
        cache: false,
        success: function (data) {
            if (data.count > 0) {
                $("#UnreadNotif").text(data.count);
                $("#UnreadNotif").show();
                $(".ft-bell").addClass("bell-shake");
                $("#notifSection").html(data.list);
            }
        }
    });
}


function formatMoney(number, decPlaces, decSep, thouSep) {
    decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
        decSep = typeof decSep === "undefined" ? "." : decSep;
    thouSep = typeof thouSep === "undefined" ? "," : thouSep;
    var sign = number < 0 ? "-" : "";
    var i = String(parseInt(number = Math.abs(Number(number) || 0).toFixed(decPlaces)));
    var j = (j = i.length) > 3 ? j % 3 : 0;

    return sign +
        (j ? i.substr(0, j) + thouSep : "") +
        i.substr(j).replace(/(\decSep{3})(?=\decSep)/g, "$1" + thouSep) +
        (decPlaces ? decSep + Math.abs(number - i).toFixed(decPlaces).slice(2) : "");
}