function loadComments() {
    $.get(`/Issues/${$('#issue-key-label').text()}/Comments`, function (response) {
        if (response && response.hasComments) {
            // Counterpart of _CommentsSection.cshtml
            $('#comments').html(`
                <h5 class="text-muted fw-semibold">Comments</h5>
                <div id="comments-list" class="py-1"></div>
                <div class="row">
                    <div class="col-md-6">
                        <form method="post" id="add-comment-form" role="form">
                            <textarea class="form-control mt-3" placeholder="Add a comment..." id="add-comment-content-field" name="@nameof(CommentForCreate.Content)" style="height: 5.1rem;"></textarea>
                            <button type="submit" class="btn btn-primary mt-3">Add Comment</button>
                        </form>
                    </div>
                </div>
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
        url: `/Issues/${$('#issue-key-label').text()}/Comments`,
        data: new FormData($(this)[0]),
        processData: false,
        contentType: false,
        dataType: 'JSON',
        success: function () {
            loadComments();
        }, error: function (xhr) {
            if (HasBadRequestErrors(xhr)) {
                try {
                    let response = JSON.parse(xhr.responseText);
                    if (Array.isArray(response)) {
                        showPageAlert('danger', response.join('<br>'));
                    } 
                } catch {
                    console.error(response.responseText);
                }
            }
        }
    });
});