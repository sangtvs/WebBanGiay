$(document).ready(function () {
    $(".search").click(function () {
        if ($(".BlockSearch").css('display') === 'flex') {
            $(".BlockSearch").css('display', 'none');
        } else {
            $(".BlockSearch").css('display', 'flex');
        }

    });
    
    var maincontent = $('.main__content');
    var linkcart = maincontent.find('.link-cart');
    var mainbtn = $('.main__btn');
    var LCLength = linkcart.length;
    var visibleCartCount = 4;

    if (LCLength < 4) {
        for (var i = 0; i < LCLength; i++) {
            linkcart.eq(i).addClass("show");
        }
        mainbtn.css("display" , "none");
    } else {
        for (var i = 0; i < visibleCartCount; i++) {
            linkcart.eq(i).addClass("show");
        }
        mainbtn.click(function () {
            if (LCLength - visibleCartCount >= 4) {
                visibleCartCount += 4;
                for (var i = 0; i < visibleCartCount; i++) {
                    linkcart.eq(i).addClass("show");
                }
            } else {
                for (var i = 0; i < LCLength; i++) {
                    linkcart.eq(i).addClass("show");
                }
                mainbtn.css("display" , "none");
            }
        });
    }




});