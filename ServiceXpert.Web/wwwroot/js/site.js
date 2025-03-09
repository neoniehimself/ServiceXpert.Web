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