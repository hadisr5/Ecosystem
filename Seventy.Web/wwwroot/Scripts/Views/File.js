

var fileTable;
function reloadFileList() {

    fileTable = setDataTable("FileList", "LoadData", "",
        [
            // datatable.js columns documentation 
            // documentation => https://datatables.net/reference/option/columns
            // searchable default= true
            // orderable default= true
            // visible default= true

            { data: "ID", searchable: false, visible: false },
            { data: "Title", searchable: true, orderable: true },
            { data: "Description", searchable: true, orderable: true },
            {
                data: "Type", searchable: true, orderable: true,
                render: function (data, type, row) {
                    switch (data) {
                        case 0:
                            return `سایر`;
                        case 1:
                            return `ویدئو`;
                        case 2:
                            return `HTML`;
                    }
                }
            },
            { data: "RegDate", searchable: true, orderable: true },
            {
                searchable: false,
                orderable: false,
                render: function (data, type, row) {
                    //render html
                    return `<button type="button" class="btn-sm btn-danger" onclick="deletePrompt(${row.ID})"><i class="ficon ft-trash-2"></i></button>`;
                }
            }
        ]
    )
}

function deletePrompt(id) {
    deleteSwal(doDelete, id);
}

function doDelete(id) {
    postDelete("/Edu/file/delete", id, fileTable);
}


