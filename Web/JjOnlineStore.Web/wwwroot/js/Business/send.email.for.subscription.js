(function() {
    $(document).ready(function() {
        $("#subscribeBtn").click(function() {
            const $emailVal = $("#subscribe-email").val();

            const emailBindingModel = {
                email: $emailVal
            };

            const request = {
                method: "POST",
                url: subscribeByEmailUrl,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(emailBindingModel)
            };

            $.ajax(request)
                .then(displaySubscriptionSuccessMessage);

        });
    });
})(subscribeByEmailUrl);

function displaySubscriptionSuccessMessage() {
    $.fancybox("Mail successfully sent.");
}

function validateEmail(email) {
    const  regExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return regExp.test(String(email).toLowerCase());
}