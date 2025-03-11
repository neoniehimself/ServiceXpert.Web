/**
 * Displays the Bootstrap's alert on screen
 * @param {any} alertClass - the Bootstrap's alert class
 * @param {any} alertMessage
 */
function initializeAlert(alertClass, alertMessage) {
    var alertTimeInMilliseconds = 4000;

    if (alertClass == null) {
        alertClass = 'alert-danger';
    }

    if (alertMessage == null) {
        alertMessage = 'No alert message specified!';
    }

    var alert = $('<div class="alert ' + alertClass + ' d-flex align-items-center" role="alert">');
    var svg = $();

    if (alertClass == 'alert-primary') {
        svg = svg.add($('<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-info-circle-fill flex-shrink-0 me-2" viewBox="0 0 16 16" role="img" style="width: 1rem; height: auto;">'
            + '<path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16m.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2"/>'
            + '</svg>'));
    } else if (alertClass == 'alert-success') {
        svg = svg.add($('<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-check-circle-fill flex-shrink-0 me-2" viewBox="0 0 16 16" role="img" style="width: 1rem; height: auto;">'
            + '<path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>'
            + '</svg>'));
    } else {
        svg = svg.add($('<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-exclamation-triangle-fill flex-shrink-0 me-2" viewBox="0 0 16 16" role="img" style="width: 1rem; height: auto;">'
            + '<path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>'
            + '</svg>'));
    }

    alert.append(svg);
    alert.append('<div>' + alertMessage + '</div>');

    $('#alert-container').html(alert);

    // Auto-close after N seconds in milliseconds
    //setTimeout(function () {
    //    alert.alert('close'); // Use Bootstrap's alert close method to hide the alert
    //    location.reload();
    //}, alertTimeInMilliseconds);
}

$(document).ready(function () {
    // Global AJAX event to set cursor when any AJAX request starts
    $(document).ajaxStart(function () {
        $('body').addClass('loading'); // Change the cursor to 'wait'
    });

    // Global AJAX event to reset cursor when all AJAX requests complete
    $(document).ajaxStop(function () {
        $('body').removeClass('loading'); // Reset the cursor back to normal
    });
});

/* Bootstrap Theme Switching
function toggleTheme() {
    const html = document.documentElement;
    const themeToggleIcon = document.getElementById("themeToggleIcon");

    const currentTheme = html.getAttribute("data-bs-theme");
    const newTheme = currentTheme === "light" ? "dark" : "light";

    html.setAttribute("data-bs-theme", newTheme);
    localStorage.setItem("theme", newTheme);

    // Toggle icon
    if (newTheme === "light") {
        themeToggleIcon.classList.replace("bi-moon-fill", "bi-sun-fill"); // Sun for light mode
        themeToggleIcon.style.color = "var(--bs-body-color)"; // Make it visible
    } else {
        themeToggleIcon.classList.replace("bi-sun-fill", "bi-moon-fill"); // Moon for dark mode
        themeToggleIcon.style.color = "var(--bs-light)"; // White in dark mode
    }
}

// Load theme preference on page load
document.addEventListener("DOMContentLoaded", () => {
    const savedTheme = localStorage.getItem("theme") || (window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light");
    document.documentElement.setAttribute("data-bs-theme", savedTheme);

    // Set correct icon on page load
    const themeToggleIcon = document.getElementById("themeToggleIcon");
    themeToggleIcon.classList.toggle("bi bi-transparency", savedTheme === "light");
    themeToggleIcon.classList.toggle("bi bi-transparency", savedTheme === "dark");
});
*/