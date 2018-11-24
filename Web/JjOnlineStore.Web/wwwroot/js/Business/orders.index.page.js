//Order Index Partial Screens
const $paymentMethodsPartial = $("#payment-methods-partial");
const $deliveryMethodsPartial = $("#delivery-methods-partial");

$("#show-delivery-methods").click(function () {
    $paymentMethodsPartial.fadeOut("slow", function () {
        $deliveryMethodsPartial.fadeIn("slow");
        $deliveryMethodsPartial.removeClass("hidden");
    });
});

$("#show-payment-methods").click(function () {
    $deliveryMethodsPartial.fadeOut("slow", function () {
        $paymentMethodsPartial.fadeIn("slow");
    });
});

$("#submit-order-form").click(function () {
    if (checkOrderFormInputs()) {
        $("#create-order-form").submit();
    } else {
        $.fancybox("Some of the required fields are not filled." +
            "Please, fill in all fields.");
    }
});



function checkOrderFormInputs() {
    var isValid = true;
    $("input").filter("[required]").each(function () {
        if ($(this).val() === "") {
            $("#submit-order-form").prop("disabled", true);
            isValid = false;
            return false;
        }
    });
    if (isValid) {
        $("#submit-order-form").prop("disabled", false);
    }
    return isValid;
}