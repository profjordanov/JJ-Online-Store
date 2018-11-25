//Order Index Partial Screens
const $paymentMethodsPartial = $("#payment-methods-partial");
const $deliveryMethodsPartial = $("#delivery-methods-partial");

//Messages
const fieldsValidationErrMsg = "Some of the required fields are not filled." +
    "Please, fill in all fields.";

$("#show-delivery-methods").click(function () {
    if (validatePaymentInputs()) {
        $paymentMethodsPartial.fadeOut("slow", function () {
            $deliveryMethodsPartial.fadeIn("slow");
            $deliveryMethodsPartial.removeClass("hidden");
        });
    } else {
        $.fancybox(fieldsValidationErrMsg);
    }
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
        $.fancybox(fieldsValidationErrMsg);
    }
});

function validatePaymentInputs() {
    const cardholderNameVal = $("#CardholderName").val();
    const expireDateMonthVal = $("#expire-date-month").val();
    const expireDateYearVal = $("#expire-date-year").val();
    const cardNumberVal = $("#CardNumber").val();
    const cvvVal = $("#Cvv").val();

    if (cardholderNameVal == "" ||
        cardNumberVal == "" ||
        cvvVal == "" ||
        expireDateMonthVal == 0 ||
        expireDateYearVal == 0) {
        return false;

    }
    return true;
}

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