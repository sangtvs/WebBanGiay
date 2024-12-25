
$(document).ready(function () {
    $("#btn_edit").click(function(){
        if($(".showedit").css('display') === 'none'){
            $(".showedit").css('display', 'block');
        }else {
            $(".showedit").css('display', 'none');
        }
    });
});