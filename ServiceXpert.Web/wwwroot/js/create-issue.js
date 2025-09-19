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
    var searchQuery = $(this).val().trim();

    if (!searchQuery) {
        $('#assignee-suggestions').empty();
        return;
    }

    $('#assignee-suggestions-spinner').removeClass('d-none');

    $.ajax({
        type: 'GET',
        url: '/Users/SearchUserByName',
        data: { searchQuery: searchQuery},
        success: function (response) {
            var userProfiles = response.userProfiles;
            var options = '';
            $.each(userProfiles, function (i, userProfile) {
                options += `<option value='${userProfile.firstNameLastName}' data-id=${userProfile.id}>`;
            });
            $('#assignee-suggestions').html(options);
        },
        complete: function () {
            $('#assignee-suggestions-spinner').addClass('d-none');
        }
    });
});

$(document).on('input', '#create-issue-modal-assignee-field', function () {
    var val = $(this).val().trim();

    if (!val) {
        $('#assignee-id').val('');
        return;
    }

    var option = $('#assignee-suggestions option').filter(function () {
        return this.value === val;
    }).first();

    $('#assignee-id').val(option.length ? option.data('id') : '');
});