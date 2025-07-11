$(document).ready(function () {
    $.get(`/Issues/${$('#issue-key-label').text()}/Comments`, function (response) {
        if (response && response.hasComments) {
            $('#comments').html(`
                <h5 class="text-muted fw-semibold">Comments</h5>
                <div id="comments-list" class="py-1"></div>
                <button type="button" class="btn btn-primary btn-sm">Add Comment</button>
            `);

            $('#comments-list').html(response.commentsHtml);
        } else {
            $('#comments-spinner').addClass('d-none');
        }
    });
});