var maincontent = $('.main__content1');
var linkcart = maincontent.find('.link-cart1');
var mainbtn = $('.main__btn');
var LCLength = linkcart.length;
var visibleCartCount = 4;


    for (var i = 0; i < LCLength; i++) {
        linkcart.eq(i).addClass("show");
    }
