$(document).ready(function () {
    $('#btn-create-issue').click(() => {
        $.get('/Issues/InitializeCreateIssue', (response) => {
            $('#modal-container').html(response);
            $('#create-issue-modal').modal('show');
        });
    });
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

$(document).on('keyup', '#create-issue-modal-assignee-field', function () {
    let searchQuery = $(this).val();

    if (searchQuery.length > 1) {
        $('#assignee-suggestions-spinner').removeClass('d-none');
    }

    $.ajax({
        type: 'GET',
        url: '/Users/SearchUserByName',
        data: { searchQuery: searchQuery},
        success: function (response) {
            let results = response.d;
            let options = '';
            $.each(results, function (i, item) {
                options += `<option value='${item}'>`;
            });
            $('#assignee-suggestions').html(options);
        },
        complete: function () {
            $('#assignee-suggestions-spinner').addClass('d-none');
        }
    });
});