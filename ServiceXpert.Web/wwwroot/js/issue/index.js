$(document).ready(function () {
    loadIssuesTableRows('all');
    $('#issues-table-search-form').submit(function (e) {
        e.preventDefault();
        loadIssuesTableRows($('#status-category-field').val());
    });
});

function loadIssuesTableRows(statusCategory, pageNumber = 1, pageSize = 10) {
    $('#issues-table tbody').html(''); // Empty the table
    $('#issues-table-pagination').remove(); // Remove pagination (dynamically rendered)
    $('#no-data').remove(); // Remove no show text (dynamically rendered)
    $.ajax({
        type: 'GET',
        url: '/Issues/GetPagedIssuesByStatus',
        data: {
            statusCategory: statusCategory,
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        success: function (response) {
            $('#issues-table-spinner').addClass('d-none');
            $('#issues-table tbody').html(response.issuesTableRowsHtml);
            if ($('#issues-table tbody tr').length > 0) {
                if ($('#issues-table-pagination').length === 0) {
                    $('.table-responsive').append('<nav class="d-flex justify-content-center justify-content-lg-end mt-3" id="issues-table-pagination"></nav>');
                }
                $('#issues-table-pagination').html(response.paginationHtml);
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
    loadIssuesTableRows($('#status-category-field').val(), $(this).data('page'));
});
