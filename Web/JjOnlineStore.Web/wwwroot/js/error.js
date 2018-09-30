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

    const messages = message.split(";");
    const messageDiv = document.getElementById("messageDiv");
    messageDiv.innerHTML = "";

    for (let i = 0; i < messages.length; i++) {
        const paragraph = document.createElement("P");
        const currentMessage = document.createTextNode(messages[i]);
        paragraph.appendChild(currentMessage);
        messageDiv.appendChild(paragraph);
    }

    $.fancybox("#error");
});