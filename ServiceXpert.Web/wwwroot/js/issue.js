$(document).ready(function () {
    loadIssueTableRows('all');
    $('#issue-table-search-form').submit(function (e) {
        e.preventDefault();
        $('#issue-table-spinner').removeClass('d-none').addClass('d-flex');
        loadIssueTableRows($('#issue-table-search-form-status-category-field').val());
    });
});

function loadIssueTableRows(statusCategory, pageNumber = 1, pageSize = 10) {
    $.ajax({
        type: 'GET',
        url: 'Issues/GetPagedIssuesAsync',
        data: {
            statusCategory: statusCategory,
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        success: function (response) {
            $('#issue-table-spinner').removeClass('d-flex').addClass('d-none');
            $('#issue-table tbody').html(response.issueTableRowsHtml);
            $('#issue-table-pagination').html(response.paginationHtml);
        }
    });
}

$(document).on('click', '#issue-table tbody .view-issue', function () {
    $.ajax({
        type: 'GET',
        url: 'Issues/' + $(this).data('key'),
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
    loadIssueTableRows($('#issue-table-search-form-status-category-field').val(), $(this).data('page'));
});