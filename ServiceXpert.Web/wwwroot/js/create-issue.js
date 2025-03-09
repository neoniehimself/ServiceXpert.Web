$(document).on('click', '#btn-create-issue', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: 'Home/CreateIssue',
        success: function (response) {
            $('.modal-container').html(response);
            $('#create-issue-modal').modal('show');
        }
    });
});