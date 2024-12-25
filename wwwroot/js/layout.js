$(document).ready(function () {
    $(".search").click(function () {
        if ($(".BlockSearch").css('display') === 'flex') {
            $(".BlockSearch").css('display', 'none');
        } else {
            $(".BlockSearch").css('display', 'flex');
        }
    });
    $('.infor_login').click(function () {
         if ($('.showselect').css('display') === 'none') {
            $('.showselect').css('display', 'block');
        } else {
            $('.showselect').css('display', 'none');
        }
    });
    $('.close-button').click(function () {
        $('.danggiay').css('display', 'none');
        $(".userprofile").css('display', 'none');
    })
    $(".add_admin").click(function () {
        if ($(".danggiay").css('display') === 'none') {
            $(".danggiay").css('display', 'block');
        } else {
            $(".danggiay").css('display', 'none');
        }
    });
    $(".showprofile").click(function () {
        if ($(".userprofile").css('display') === 'none') {
            $(".userprofile").css('display', 'block');
            $('.showselect').css('display', 'none');
        } else {
            $(".userprofile").css('display', 'none');
        }
    });
    $('#editProfile').click(function () {
        //  an phan hien thi thong tin ca nhan và hien thi form chinh sua
        $('.profile').css('display', 'none');
        $('.edit-profile-form').css('display', 'block');

        // lay thong tin hien tai va them vao phan chinh sua
        $('#editUsername').val($('#username').text());
        $('#editEmail').val($('#email').text());
        $('#editPhone').val($('#phone').text());
        $('#editadd').val($('#add').text());
    });

    $('#cancelEdit, .closeProfile').click(function () {
        //  hien thi lai phan hien thi thong tin ca nhan va an form chinh sua
        $('.edit-profile-form').css('display', 'none');
        $('.profile').css('display', 'block');

        // Reset  gia tri cua form ve gia tri ban dau
        $('#editUsername').val($('#username').text());
        $('#editEmail').val($('#email').text());
        $('#editPhone').val($('#phone').text());
        $('#editadd').val($('#add').text());
    });

    $('#editForm').submit(function (event) {
        event.preventDefault();

        //  lay gia tri tu form chinh sua va cap nhat thong tin ca nhan
        var editedUsername = $('#editUsername').val();
        var editedEmail = $('#editEmail').val();
        var editedPhone = $('#editPhone').val();
        var editadd = $('#editadd').val();

        //  cap nhat thong tin ca nhan tren trang
        $('#username').text(editedUsername);
        $('#email').text(editedEmail);
        $('#phone').text(editedPhone);
        $('#add').text(editadd);

        //  hien thi lai phan hien thi thong tin ca nhan va an form chinh sua
        $('.profile').show();
        $('.edit-profile-form').hide();
    });

});