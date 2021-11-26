

var generalExamTable;
var _examID;
function reloadGeneralExamTableList(examID) {
    _examID = examID;
    generalExamTable = setDataTable("GeneralExamQuestiosList", "LoadGeneralExamQuestionList", function (d) { d.ExamID = examID },
        [
            // datatable.js columns documentation 
            // documentation => https://datatables.net/reference/option/columns
            // searchable default= true
            // orderable default= true
            // visible default= true

            { data: "ID", searchable: false, visible: false },
            { data: "Title", name: "Question.Title", searchable: true, orderable: true, width: "80%" },
            { data: "Barom", searchable: false, orderable: true, width: "10%" },
            {
                searchable: false,
                orderable: false,
                width: "10%",
                render: function (data, type, row) {
                    //render html
                    return `<button type="button" class="btn-sm btn-danger" onclick="deletePrompt(${row.ID})"><i class="ficon ft-trash-2"></i></button>`;
                }
            }
        ]
    );

    return generalExamTable;
}

function deletePrompt(id) {
    deleteSwal(doDelete, id);
}

async function doDelete(id) {
    $.ajax({
        type: "POST",
        url: "/Edu/Exam/DeleteQuestionFromExam",
        data: {
            ID: id
        },
        success: function (data) {
            if (data.IsSuccess) {
                successSwal(data.statusText);
                reloadTable(generalExamTable, false);
                getExamBarom(_examID);
            }
            else {
                errorSwal(data.statusText);
            }
        }, error: function (data) {
            errorSwal(data.statusText);
        }
    });  
}


function AddRowToListSelection(id) {

    //inja bayad baresi konixvhpd
    var barom = $("#Rowbarom_" + id).val();
    if (barom == 0) {
        return
    }
    $(".question-button").prop("disabled", true);
    $.ajax({
        type: "POST",
        url: "/edu/Exam/AddQuestionToExam",
        data: {
            ID: id,
            examId:_examID,
            barom: barom
        },
        success: function (data) {
            if (data) {
                $(".question-button").prop("disabled", false);
                reloadTable(generalExamTable, false);
                getExamBarom(_examID);
            }
            else {
                errorSwal(data.statusText);
            }
        }, error: function (data) {
            errorSwal(data.statusText);
        }
    });

}

function getExamBarom(id) {

    $.ajax({
        type: "POST",
        url: "/edu/Exam/ExamBarom",
        data: {
            id: id,
        },
        success: function (data) {
            if (data.IsSuccess) {
                $("#SumOfBarom").text(data.data)
            }
        }
    });

}

