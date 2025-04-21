$(document).ready(function () {
    loadIssueTableRows('all');
    $('#issue-table-search-form').submit(function (e) {
        e.preventDefault();
        loadIssueTableRows($('#issue-table-search-form-status-category-field').val());
    });
});

function loadIssueTableRows(statusCategory, pageNumber = 1, pageSize = 10) {
    $('#issue-table tbody').html(''); // Empty the table
    $('#issue-table-pagination').remove(); // Remove pagination (dynamically rendered)
    $('#no-data').remove(); // Remove no show text (dynamically rendered)
    $('#issue-table-spinner').removeClass('d-none').addClass('d-flex'); // Show spinner
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
            if ($('#issue-table tbody tr').length > 0) {
                if ($('#issue-table-pagination').length === 0) {
                    $('.table-responsive').append('<nav class="d-flex justify-content-center justify-content-lg-end mt-3" id="issue-table-pagination"></nav>');
                }
                $('#issue-table-pagination').html(response.paginationHtml);
            } else {
                if ($('#no-data').length === 0) {
                    $('.table-responsive').append('<p class="mt-3 fw-medium" id="no-data">No data available</p>');
                }
            }
        }
    });
}

$(document).on('click', '#issue-table tbody .view-issue', function () {
    $.ajax({
        type: 'GET',
        url: 'Issues/ViewIssueAsync',
        data: { issueKey: $(this).data('key') },
        success: function (response) {
            $('.modal-container').html(response);
            $('#view-issue-modal').modal('show');
        }
    });
});

$(document).on('hidden.bs.modal', '#view-issue-modal', function () {
    $('.modal-container').html('');
});

$(document).on('click', '#btn-edit-issue', function () {
    $.ajax({
        type: 'GET',
        url: 'Issues/EditIssueAsync',
        data: { issueKey: $('#view-issue-modal-label').text() },
        success: function (response) {
            $('#view-issue-modal').modal('hide');
            $('.modal-container').html(response);
            $('#edit-issue-modal').modal('show');
        }
    });
});

$(document).on('click', '#btn-back-to-view', function () {
    $.ajax({
        type: 'GET',
        url: 'Issues/ViewIssueAsync',
        data: { issueKey: $('#edit-issue-modal-label').text() },
        success: function (response) {
            $('#edit-issue-modal').modal('hide');
            $('.modal-container').html(response);
            $('#view-issue-modal').modal('show');
        }
    });
});

$(document).on('submit', '#edit-issue-modal-form', function (e) {
    e.preventDefault();
    var formData = new FormData($(this)[0]);

    $.ajax({
        type: 'PUT',
        url: 'Issues',
        cache: false,
        processData: false,
        contentType: false,
        dataType: 'JSON',
        data: formData,
        success: function (response) {

        }
    });
});

$(document).on('click', '.pagination .page-link', function (e) {
    e.preventDefault();
    loadIssueTableRows($('#issue-table-search-form-status-category-field').val(), $(this).data('page'));
});