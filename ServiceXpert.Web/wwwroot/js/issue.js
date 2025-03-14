$(document).ready(function () {
    $('#btn-create-issue').click(function (e) {
        $.ajax({
            type: 'GET',
            url: 'Issues/InitializeCreateIssue',
            success: function (response) {
                $('.modal-container').html(response);
                $('#create-issue-modal').modal('show');
            }
        });
    });
});

$(document).on('submit', '#create-issue-modal-form', function (e) {
    e.preventDefault();

    var formData = new FormData($(this)[0]);

    $.ajax({
        type: 'POST',
        url: 'Issues/CreateIssue',
        cache: false,
        processData: false,
        contentType: false,
        dataType: 'JSON',
        data: formData,
        success: function (response) {
            $('#create-issue-modal').modal('hide');
            initializeAlert('success', 'The issue was created successfully!', false, true, true);
        }
    });
});