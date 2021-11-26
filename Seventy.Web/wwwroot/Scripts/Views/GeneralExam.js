

var generalExamTable;
function reloadGeneralExamTableList() {

    generalExamTable = setDataTable("GeneralExamTableList", "LoadGeneralExamList", "",
        [
            // datatable.js columns documentation 
            // documentation => https://datatables.net/reference/option/columns
            // searchable default= true
            // orderable default= true
            // visible default= true

            { data: "ID", searchable: false, visible: false},
            { data: "Title", searchable: true, orderable: true, width: "10%" },
            { data: "Description", searchable: true, orderable: true, width: "30%" },
            { data: "StartDate", searchable: true, orderable: true, width: "20%" },
            { data: "EndDate", searchable: true, orderable: true, width: "20%" },
            { data: "RegDate", searchable: true, orderable: true, width: "20%" },
            {
                searchable: false,
                orderable: false,
                width: "10%",
                render: function (data, type, row) {
                    //render html
                    if (row.Editable) {
                        return `<button type="button" class="btn-sm btn-warning" onclick="editPrompt(${row.ID})"><i class="ficon ft-edit"></i></button>
                            <button type="button" class="btn-sm btn-danger" onclick="deletePrompt(${row.ID})"><i class="ficon ft-trash-2"></i></button>`;
                    }
                    return "";
                }
            }
        ]
    )
}

function editPrompt(id) {
    editSwal(doEdit, id);
}
function doEdit(id) {
    document.location = "/edu/Exam/EditGeneralExam?ExamID=" + id;
}

function deletePrompt(id) {
    deleteSwal(doDelete, id);
}

function doDelete(id) {
    postDelete("/Edu/Exam/DeleteGeneralExam", id, generalExamTable);
}


