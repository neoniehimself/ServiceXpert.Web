function loadIssueComments() {
    $.get(`/Issues/${$('#issue-key-label').text()}/IssueComments`, function (response) {
        if (response && response.hasComments) {
            // Counterpart of _CommentsSection.cshtml
            $('#issue-comments').html(`
                <h5 class="text-muted fw-semibold">Comments</h5>
                <div id="issue-comments-list" class="py-1"></div>
                <div class="row">
                    <div class="col-md-6">
                        <form method="post" id="add-issue-comment-form" role="form">
                            <textarea class="form-control" placeholder="Add a comment..." id="add-issue-comment-content-field" name="@nameof(CreateIssueComment.Content)" style="height: 5.1rem;"></textarea>
                            <button type="submit" class="btn btn-primary mt-3">Add Comment</button>
                        </form>
                    </div>
                </div>
            `);

            $('#issue-comments-list').html(response.commentsHtml);
        } else {
            $('#issue-comments-spinner').addClass('d-none');
        }
    });
}

$(document).ready(function () {
    loadIssueComments();
});

$(document).on('submit', '#add-issue-comment-form', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'POST',
        url: `/Issues/${$('#issue-key-label').text()}/IssueComments`,
        data: new FormData($(this)[0]),
        processData: false,
        contentType: false,
        dataType: 'JSON',
        success: function () {
            loadIssueComments();
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