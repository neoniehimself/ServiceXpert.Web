function getTabContent(tab) {
    var spinner = $('#issue-tabs-content-spinner');

    showSpinner(true, spinner);

    $.ajax({
        type: 'GET',
        url: 'Issues/GetTabContent',
        data: { tab: tab },
        success: function (response) {
            showSpinner(false, spinner);

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

    $('#issue-tabs button').click(function () {
        var tab = $(this).attr('data-tab');
        getTabContent(tab.toLowerCase());
    });
});