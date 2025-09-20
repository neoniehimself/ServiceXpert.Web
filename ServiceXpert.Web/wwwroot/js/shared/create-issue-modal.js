import { initUserPickerSearchbox } from './user-picker-searchbox.js'

$(document).ready(function () {
    $('#btn-create-issue').click(() => {
        $.get('/Issues/InitializeCreateIssue', (response) => {
            $('#modal-container').html(response);
            $('#create-issue-modal').modal('show');
        });
    });

    initUserPickerSearchbox();
});

$(document).on('submit', '#create-issue-modal-form', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'POST',
        url: '/Issues',
        data: new FormData($(this)[0]),
        processData: false,
        contentType: false,
        dataType: 'JSON',
        success: function (response) {
            $('#create-issue-modal').modal('hide');
            showPageAlert('success', `Issue key: ${response.issueKey} was created successfully!`, false, true, true);
        },
        error: function (xhr) {
            if (HasBadRequestErrors(xhr)) {
                $('.modal-body').prepend(modalAlertString);
                showModalAlert('danger', xhr.responseJSON.join('<br>'));
                return;
            }
        }
    });
});