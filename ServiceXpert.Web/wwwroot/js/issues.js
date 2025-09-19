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

$(document).on('click', '.pagination .page-link', function (e) {
    e.preventDefault();
    loadIssueTableRows($('#issue-table-search-form-status-category-field').val(), $(this).data('page'));
});