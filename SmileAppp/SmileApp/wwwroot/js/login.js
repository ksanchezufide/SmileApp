$(document).ready(function () {
    // Mostrar/ocultar contraseña
    $('#togglePassword').click(function () {
        const passwordInput = $('#password');
        const icon = $(this).find('i');

        if (passwordInput.attr('type') === 'password') {
            passwordInput.attr('type', 'text');
            icon.removeClass('fa-eye').addClass('fa-eye-slash');
        } else {
            passwordInput.attr('type', 'password');
            icon.removeClass('fa-eye-slash').addClass('fa-eye');
        }
    });

    // Validación del formulario
    $('form').submit(function (e) {
        const username = $('#username').val().trim();
        const password = $('#password').val().trim();

        if (!username) {
            e.preventDefault();
            $('#username').addClass('is-invalid');
            $('#username').focus();
            return false;
        } else {
            $('#username').removeClass('is-invalid');
        }

        if (!password) {
            e.preventDefault();
            $('#password').addClass('is-invalid');
            $('#password').focus();
            return false;
        } else {
            $('#password').removeClass('is-invalid');
        }
    });

    // Efecto hover para la tarjeta
    $('.login-card').hover(
        function () {
            $(this).css('transform', 'translateY(-5px)');
            $(this).css('box-shadow', '0 15px 35px rgba(0, 0, 0, 0.15)');
        },
        function () {
            $(this).css('transform', 'translateY(0)');
            $(this).css('box-shadow', '0 10px 30px rgba(0, 0, 0, 0.1)');
        }
    );
});