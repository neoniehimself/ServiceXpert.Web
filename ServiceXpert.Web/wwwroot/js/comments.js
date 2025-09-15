function loadComments() {
    $.get(`/Issues/${$('#issue-key-label').text()}/Comments/`, function (response) {
        if (response && response.hasComments) {
            // Counterpart of _CommentsSection.cshtml
            $('#comments').html(`
                <h5 class="text-muted fw-semibold">Comments</h5>
                <div id="comments-list" class="py-1"></div>
                <form method="post" id="add-comment-form" role="form">
                    <textarea class="form-control" placeholder="Add a comment..." id="add-comment-content-field" name="Content" style="width: 40rem; height: 5.1rem;"></textarea>
                    <button type="submit" class="btn btn-primary mt-3">Add Comment</button>
                </form>
            `);

            $('#comments-list').html(response.commentsHtml);
        } else {
            $('#comments-spinner').addClass('d-none');
        }
    });
}

$(document).ready(function () {
    loadComments();
});

$(document).on('submit', '#add-comment-form', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'POST',
        url: `/Issues/${$('#issue-key-label').text()}/Comments/`,
        data: new FormData($(this)[0]),
        processData: false,
        contentType: false,
        dataType: 'JSON',
        success: function () {
            loadComments();
        }
    });
});