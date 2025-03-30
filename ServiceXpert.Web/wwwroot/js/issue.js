function getTabContent(tab, pageNumber = 1, pageSize = 10) {
    var spinner = $('#table-issue-body-spinner');

    $.ajax({
        type: 'GET',
        url: 'Issues/GetTabContent',
        data: { tab: tab, pageNumber: pageNumber, pageSize: pageSize },
        success: function (response) {
            spinner.addClass('d-none');
            spinner.detach();
            $('#table-issue-body').html(response.tabContentView);
            $('#table-issue-body').append(spinner);
            $('#table-issue-pagination').html(response.paginationView);
        },
        error: function (xhr, status, error) {
            console.error('AJAX Error:', status, error);
        }
    });
}

$(document).on('click', '.pagination .page-link', function (e) {
    e.preventDefault();

    var tab = $('#issue-tabs button.active').data('tab');
    var pageNumber = $(this).data('page');

    getTabContent(tab, pageNumber);
});

$(document).ready(function () {
    getTabContent('all');

    $('#issue-tabs button').click(function () {
        $('#issue-tabs button').removeClass('active');
        $(this).addClass('active');

        var tab = $(this).data('tab');
        getTabContent(tab.toLowerCase());
    });
});