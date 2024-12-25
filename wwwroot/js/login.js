$(document).ready(function() {
    $('button').click(function() {
        const username = $('#username').val();
        const password = $('#password').val();

        // Kiểm tra tên đăng nhập không có ký tự đặc biệt
        const usernameSpecialChars = /[!@#$%^&*(),.?":{}|<>\s]/;
        if (username === ''){
            $('#username-error').text('Bạn chưa nhập tài khoản');
            return;
        }else if (usernameSpecialChars.test(username)) {
            $('#username-error').text('Tên đăng nhập sai.');
            return;
        }else{
            $('#username-error').hide();
        }

        // Kiểm tra mật khẩu có ít nhất một chữ hoa, một số và một ký tự đặc biệt
        const passwordUpperCase = /[A-Z]/;
        const passwordNumber = /[0-9]/;
        const passwordSpecialChars = /[!@#$%^&*(),.?":{}|<>\s]/;
        if (password === ''){
            $('#password-error').text('Bạn chưa nhập mật khẩu');
            return;
        }else if(password.length < 8){
            $('#password-error').text('Mật khẩu sai.');
            return;
        }else if (!passwordUpperCase.test(password) || !passwordNumber.test(password) || !passwordSpecialChars.test(password)) {
            $('#password-error').text('Mật khẩu sai.');
            return;
        }else{
            $('#password-error').hide();
        }


        // Nếu không có lỗi, hiển thị thông báo đăng nhập thành công
        // Hiển thị cửa sổ modal khi đăng ký thành công
        $('#myModal').css('display', 'block');

        // Đóng cửa sổ modal khi người dùng click vào nút đóng
        $('.close').click(function() {
            $('#myModal').css('display', 'none');
        });

        // Add code here to submit the form or perform further actions after successful registration
    });
});
