$(document).ready(function () {
    $.get(`/Issues/${$('#issue-key-label').text()}/Comments`, function (response) {
        if (response && response.hasComments) {
            $('#comments').html(`
                <h5 class="text-muted fw-semibold">Comments</h5>
                <div id="comments-list" class="py-1"></div>
                <textarea class="form-control" placeholder="Add a comment..." id="add-comment-content-field" style="width: 40rem; height: 5.1rem;"></textarea>
                <button type="button" class="btn btn-primary btn-sm mt-3">Add Comment</button>
            `);

            $('#comments-list').html(response.commentsHtml);
        } else {
            $('#comments-spinner').addClass('d-none');
        }
    });
});