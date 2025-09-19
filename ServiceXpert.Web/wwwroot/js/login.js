$(document).ready(function () {
    $('#login-form').submit(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/Accounts/LoginUserAsync',
            data: new FormData($(this)[0]),
            processData: false,
            contentType: false,
            dataType: 'JSON',
            success: function (response) {
                // Set delay for Lax to take effect
                setTimeout(() => {
                    window.location.href = response.redirectUrl;
                }, 50);
            },
            error: function (xhr) {
                if (HasBadRequestErrors(xhr)) {
                    showPageAlert('danger', xhr.responseJSON.join("<br>"));
                    return;
                }
            }
        });
    });
});
