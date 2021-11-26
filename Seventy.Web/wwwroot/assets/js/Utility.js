

function fillSelect(url,name){

    $.ajax({
        url:url,
        method:'get',
        success:function(res) {

            let val= $(name).attr('preValue');
            let html='';
            for(let i = 0; i <res.length ; i++) {
                let option;
                if((val+"")===(res[i].id+""))
                {
                    option ="<option  selected='selected' value='"+res[i].id+"'>"+res[i].name+"</option>";

                }else{
                    option ="<option value='"+res[i].id+"'>"+res[i].name+"</option>";

                }
                html+=option;
            }

            $(name).html(html);

        },error:function(e) {
            console.error(e);
        }
    })


}