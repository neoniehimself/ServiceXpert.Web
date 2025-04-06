$(document).ready(function () {
    loadIssueTableRow('all');

    $('#issue-tabs button').click(function () {
        $('#issue-tabs button').removeClass('active');
        $(this).addClass('active');

        var tab = $(this).data('tab');
        loadIssueTableRow(tab.toLowerCase());
    });
});

function loadIssueTableRow(tab, pageNumber = 1, pageSize = 10) {
    var spinner = $('#table-issue-body-spinner');

    $.ajax({
        type: 'GET',
        url: 'Issues/GetPagedIssuesAsync',
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
        url: 'Issues/' + issueKey,
        success: function (response) {
            $('.modal-container').html(response);
            $('#view-issue-modal').modal('show');
        }
    });
});

$(document).on('hidden.bs.modal', '#view-issue-modal', function () {
    $('.modal-container').html('');
});

$(document).on('click', '.pagination .page-link', function (e) {
    e.preventDefault();

    var tab = $('#issue-tabs button.active').data('tab');
    var pageNumber = $(this).data('page');

    loadIssueTableRow(tab, pageNumber);
});