import { initUserPickerSearchbox } from '../shared/user-picker-searchbox.js'

$(document).ready(function () {
    initUserPickerSearchbox();
});

$('#edit-issue-form').submit(function (e) {
    e.preventDefault();

    if (window.confirm('Do you want to save your changes?')) {
        var issueKey = $('#issue-key-label').text();
        $.ajax({
            type: 'PUT',
            url: `/Issues/Edit/${issueKey}`,
            data: new FormData($(this)[0]),
            cache: false,
            processData: false,
            contentType: false,
            dataType: 'JSON',
            success: function (response) {
                // If status code is in 200, then operation was successful
                if (response.statusCode >= 200 && response.statusCode <= 299) {
                    window.location.href = `/Issues/View/${issueKey}`;
                }
            }
        });
    } else {
        return false;
    }
});