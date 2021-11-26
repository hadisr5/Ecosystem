

var singleSkillTable;
function reloadSingleSkillList() {

    singleSkillTable = setDataTable("SingleSkillTable", "singleSkill/LoadData", "",
        [
            // datatable.js columns documentation 
            // documentation => https://datatables.net/reference/option/columns
            // searchable default= true
            // orderable default= true
            // visible default= true

            { data: "ID", searchable: false, visible: false },
            { data: "Title", searchable: true, orderable: true },
            { data: "Duration", searchable: true, orderable: true },
            { data: "Price", searchable: false, orderable: true, render: function (data) { return formatMoney(data,0, ".", ","); } },
            {
                data:null,
                searchable: false,
                orderable: false,
                render: function (data, type, row) {
                    //render html
                    if (data.IsActive) {
                        return `<a class="btn btn-primary" style="color:white">ثبت نام شده</a>`;
                    }
                    return `<a class="btn btn-info" style="color:white" onclick="return confirm('آیا مطمئن هستید؟')">ثبت نام</a>`;
                }
            }
        ]
    )
}

function deletePrompt(id) {
    deleteSwal(doDelete, id);
}

function doDelete(id) {
    postDelete("/Edu/SingleSkill/delete", id, singleSkillTable);
}
