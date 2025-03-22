function getTabContent(tab, pageNumber = 1, pageSize = 10) {
    var spinner = $('#issue-tabs-content-spinner');

    showSpinner(true, spinner);

    $.ajax({
        type: 'GET',
        url: 'Issues/GetTabContent',
        data: { tab: tab, pageNumber: pageNumber, pageSize: pageSize },
        success: function (response) {
            showSpinner(false, spinner);

            // Remove css classes and clear content of previous tab
            $('.tab-pane')
                .removeClass('show active')
                .empty();

            $('#' + tab + '-issues-tab-content')
                .html(response)
                .addClass('show active');
        },
        error: function (xhr, status, error) {
            console.error('AJAX Error:', status, error);
        }
    });
}

$(document).on('click', '.pagination .page-link', function (e) {
    e.preventDefault();
    var tab = $('.tab-pane.show.active').attr('id').replace('-issues-tab-content', '');
    var pageNumber = $(this).data('page');

    getTabContent(tab, pageNumber);
});

$(document).ready(function () {
    getTabContent('all');

    $('#issue-tabs button').click(function () {
        var tab = $(this).attr('data-tab');
        getTabContent(tab.toLowerCase());
    });
});