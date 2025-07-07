$(document).ready(function () {
    $.get(`/Issues/${$('#issue-key-label').text()}/Comments`, function (response) {
        console.log('Test');
    });
});