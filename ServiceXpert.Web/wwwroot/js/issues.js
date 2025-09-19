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
    $.ajax({
        type: 'GET',
        url: '/Issues/GetPagedIssuesByStatusAsync',
        data: {
            statusCategory: statusCategory,
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        success: function (response) {
            $('#issue-table-spinner').addClass('d-none');
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

$(document).on('click', '#btn-edit-issue', function () {
    $.get(`/Issues/EditIssueAsync/${$('#view-issue-modal-label').text()}`, function (response) {
        $('#view-issue-modal').modal('hide');
        $('#modal-container').html(response);
        $('#edit-issue-modal').modal('show');
    });
});

$(document).on('click', '#btn-back-to-view', function () {
    $.get(`/Issues/ViewIssueAsync/${$('#edit-issue-modal-label').text()}`, function (response) {
        $('#edit-issue-modal').modal('hide');
        $('#modal-container').html(response);
        $('#view-issue-modal').modal('show');
    });
});

$(document).on('submit', '#edit-issue-modal-form', function (e) {
    e.preventDefault();

    var issueKey = $('#edit-issue-modal-label').text();

    $.ajax({
        type: 'PUT',
        url: `/Issues/UpdateIssueAsync/${issueKey}`,
        data: new FormData($(this)[0]),
        cache: false,
        processData: false,
        contentType: false,
        success: function (response) {
            // If StatusCode Is In 200, Then Operation Was Successful
            if (response.statusCode >= 200 && response.statusCode <= 299) {
                $('#edit-issue-modal').modal('hide');
                showPageAlert('success', `Issue key: ${issueKey} was updated successfully!`, false, true, true);
            }
        }
    });
});

$(document).on('click', '.pagination .page-link', function (e) {
    e.preventDefault();
    loadIssueTableRows($('#issue-table-search-form-status-category-field').val(), $(this).data('page'));
});