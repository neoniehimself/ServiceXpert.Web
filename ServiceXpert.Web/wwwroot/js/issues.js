function showSpinner(show) {
    var spinner = $('#issues-tabs-content-spinner');

    if (show) {
        if (!spinner.hasClass('d-flex')) {
            $(spinner).addClass('d-flex');
        }
    } else {
        $(spinner).removeClass('d-flex');
        $(spinner).addClass('d-none');
    }
}

function getTabContent(tab) {
    showSpinner(true);

    $.ajax({
        type: 'GET',
        url: 'Issues/GetIssuesTabContent',
        data: { tab: tab },
        success: function (response) {
            showSpinner(false);

            // Remove css classes and clear content of previous tab
            $('.tab-pane')
                .removeClass('show active')
                .empty();

            $('#' + tab + '-issues-tab-content').html(response)
                .addClass('show active');
        },
        error: function (xhr, status, error) {
            console.error('AJAX Error:', status, error);
        }
    });
}

$(document).ready(function () {
    getTabContent('all');

    $('#issues-tabs button').click(function () {
        var tab = $(this).attr('data-tab');
        getTabContent(tab.toLowerCase());
    });
});