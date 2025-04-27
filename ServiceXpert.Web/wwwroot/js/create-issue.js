$(document).ready(function () {
    $('#btn-create-issue').click(() => {
        $.get('Issues/InitializeCreateIssue', (response) => {
            $('.modal-container').html(response);
            $('#create-issue-modal').modal('show');
        });
    });
});

$(document).on('submit', '#create-issue-modal-form', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'POST',
        url: 'Issues',
        data: new FormData($(this)[0]),
        cache: false,
        processData: false,
        contentType: false,
        dataType: 'JSON',
        success: function (response) {
            $('#create-issue-modal').modal('hide');
            initializeAlert('success', `Issue key: ${response.issueKey} was created successfully!`, false, true, true);
        }
    });
});