function FormCheck() {
    if ($('#password').val().length > 5 &&
        $('#login').val().length > 2 &&
        $('#confirm-password').val() == $('#password').val() &&
        $('#github').val().match("https://github.com/[a-zA-Z0-9_]"))
    {
        $('#register-btn').removeAttr('disabled');
    } else {
        $('#register-btn').attr('disabled', 'true');
    }
}

$('#password').keyup(() => FormCheck());
$('#confirm-password').keyup(() => FormCheck());
$('#github').keyup(() => FormCheck());
FormCheck();