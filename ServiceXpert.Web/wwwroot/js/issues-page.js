function getTabContent(tab) {
    var url = 'Issues/';

    switch (tab) {
        case 'all':
            url = url + 'AllIssues';
            break;
        case 'open':
            url = url + 'OpenIssues';
            break;
        default:
            url = url + 'AllIssues';
    }

    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
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