$(document).ready(function () {
    getTabContent('all');

    $('#issue-tabs button').click(function () {
        $('#issue-tabs button').removeClass('active');
        $(this).addClass('active');

        var tab = $(this).data('tab');
        getTabContent(tab.toLowerCase());
    });
});

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
        }
    });
}

$(document).on('click', '#table-issue-body .view-issue', function () {
    var issueKey = $(this).data('key');
    $.ajax({
        type: 'GET',
        url: 'Issues/ViewDetails',
        data: { issueKey: issueKey },
        success: function (response) {
            $('.modal-container').html(response);
            $('#view-issue-modal').modal('show');
        }
    });
});

$(document).on('click', '.pagination .page-link', function (e) {
    e.preventDefault();

    var tab = $('#issue-tabs button.active').data('tab');
    var pageNumber = $(this).data('page');

    getTabContent(tab, pageNumber);
});

