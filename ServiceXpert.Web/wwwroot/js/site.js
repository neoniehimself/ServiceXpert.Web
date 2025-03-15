var alertContainer = $('#alert-container');
var alertTimeInMilliseconds = 4000;

// Displays the Bootstrap's alert on screen
function initializeAlert(alertClass = 'warning', alertMessage = 'No alert message specified!', hasCloseButton = false, isAutoClose = false, isReloadPage = false) {
    var alert = $('<div class="alert alert-' + alertClass + ' alert-dismissible fade show d-flex align-items-center" role="alert">');
    var svg = $();

    if (alertClass == 'primary') {
        svg = svg.add($('<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-info-circle-fill flex-shrink-0 me-2" viewBox="0 0 16 16" role="img" style="width: 1rem; height: auto;">'
            + '<path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16m.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2"/>'
            + '</svg>'));
    } else if (alertClass == 'success') {
        svg = svg.add($('<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-check-circle-fill flex-shrink-0 me-2" viewBox="0 0 16 16" role="img" style="width: 1rem; height: auto;">'
            + '<path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>'
            + '</svg>'));
    } else {
        svg = svg.add($('<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-exclamation-triangle-fill flex-shrink-0 me-2" viewBox="0 0 16 16" role="img" style="width: 1rem; height: auto;">'
            + '<path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>'
            + '</svg>'));
    }

    alert.append(svg);
    alert.append('<div>' + alertMessage);

    if (hasCloseButton) {
        alert.append(' <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>');
    }

    alert.append('</div>');

    alertContainer.html(alert);

    if (isAutoClose) {
        // Auto-close after N seconds in milliseconds
        setTimeout(function () {
            alert.alert('close'); // Use Bootstrap's alert close method to hide the alert

            if (isReloadPage) {
                alert.one('closed.bs.alert', function () {
                    location.reload();
                });
            }
        }, alertTimeInMilliseconds);
    }
}

alertContainer.on('closed.bs.alert', '.alert', function () {
    $(this).remove();
});

function configureAjaxSettings() {
    // AJAX settings
    $(document).ajaxStart(function () {
        $('body').addClass('loading'); // Change the cursor to 'wait'
    });

    $(document).ajaxStop(function () {
        $('body').removeClass('loading'); // Reset the cursor back to normal
    });
}

function loadThemePreference() {
    const savedTheme = localStorage.getItem("theme") || (window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light");
    document.documentElement.setAttribute("data-bs-theme", savedTheme);

    if (savedTheme == 'light') {
        $('#themes-modal-form-rb-light').prop('checked', true);
    } else {
        $('#themes-modal-form-rb-dark').prop('checked', true);
    }
}

function themesModalFormSubmitAction() {
    $('#themes-modal-form').submit(function (e) {
        e.preventDefault();

        const html = document.documentElement;
        var newTheme = '';

        var rbSelectedThemeID = $('input[name="themes-modal-rb"]:checked').attr('id');

        if (rbSelectedThemeID == 'themes-modal-form-rb-light') {
            $(rbSelectedThemeID).prop('checked', true);
            newTheme = 'light';
        } else {
            $(rbSelectedThemeID).removeProp('checked');
            newTheme = 'dark';
        }

        html.setAttribute("data-bs-theme", newTheme);
        localStorage.setItem("theme", newTheme);
    });
}

$(document).ready(function () {
    loadThemePreference();
    configureAjaxSettings();
    themesModalFormSubmitAction();
});