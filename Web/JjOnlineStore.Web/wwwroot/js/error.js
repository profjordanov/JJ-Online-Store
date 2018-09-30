$(document).ready(() => {
    const $messageBodyVal = $("#MessageBody").val();
    var message;
    if ($messageBodyVal) {
        message = $messageBodyVal;
    } else {
        message = "";
    }

    if (message === "") {
        return;
    }

    $("#errorDialog").text(message);
    $.fancybox("#error");
});