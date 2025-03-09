$(document).on('click', '#btn-create-issue', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: 'Issue/InitializeCreateIssue',
        success: function (response) {
            $('.modal-container').html(response);
            $('#create-issue-modal').modal('show');
        }
    });
});

$(document).on('submit', '#create-issue-modal-form', function (e) {
    e.preventDefault();

    var formData = new FormData($(this)[0]);

    $.ajax({
        type: 'POST',
        url: 'Issue/CreateIssue',
        cache: false,
        processData: false,
        contentType: false,
        dataType: 'JSON',
        data: formData,
        success: function (response) {
            $('#create-issue-modal').modal('hide');
            window.location.reload();
        }
    });
});