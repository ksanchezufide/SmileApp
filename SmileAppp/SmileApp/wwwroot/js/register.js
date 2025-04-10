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

    // Validación de contraseñas coincidentes
    $('form').submit(function (e) {
        const password = $('#password').val();
        const confirmPassword = $('#confirmPassword').val();

        if (password !== confirmPassword) {
            e.preventDefault();
            $('#confirmPassword').addClass('is-invalid');
            alert('Las contraseñas no coinciden');
            return false;
        }

        if (!$('#terms').is(':checked')) {
            e.preventDefault();
            alert('Debe aceptar los términos y condiciones');
            return false;
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