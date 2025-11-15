$(document).ready(function () {
    loadIssuesTableRows();
    $('#issues-table-search-form').submit(function (e) {
        e.preventDefault();
        var searchFormViewModel = serializeSearchFormInputsWithValueToString(this);
        loadIssuesTableRows(searchFormViewModel);
    });
});

function loadIssuesTableRows(searchFormViewModel = null, pageNumber = 1, pageSize = 10) {
    $('#issues-table tbody').html(''); // Empty the table
    $('#issues-table-pagination').remove(); // Remove pagination (dynamically rendered)
    $('#no-data').remove(); // Remove no show text (dynamically rendered)

    var data = `pageNumber=${pageNumber}&pageSize=${pageSize}`;

    if (searchFormViewModel !== null) {
        data += `&${searchFormViewModel}`;
    }

    $.ajax({
        type: 'GET',
        url: '/Issues/GetPagedIssues',
        data: data,
        cache: false,
        processData: false,
        contentType: false,
        dataType: 'JSON',
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
    var searchFormViewModel = serializeSearchFormInputsWithValueToString('#issues-table-search-form');
    loadIssuesTableRows(searchFormViewModel, $(this).data('page'));
});
