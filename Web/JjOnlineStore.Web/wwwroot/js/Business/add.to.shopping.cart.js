(function () {
    $(document).ready(function () {
        $("#add-to-cart").click(function () {
            const $productId = $("#product-id").val();
            const $productQuantity = $("#choose-quantity").val();

            const cartItemBindingModel = {
                productId: $productId,
                quantity: $productQuantity
            };

            createCartItemByBindingModel(cartItemBindingModel);
        });

        function createCartItemByBindingModel(cartItemBindingModel) {
            const request = {
                method: "POST",
                url: createCartItemUrl,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(cartItemBindingModel)
            };

            $.ajax(request)
                .then(displaySuccessMessage)
                .catch(displayErrorMessage);
        };

    });

})(createCartItemUrl);

function displaySuccessMessage() {
    $.fancybox("Product successfully added to your cart.");
}

function displayErrorMessage() {
    $.fancybox("This product has already been added to the cart.");
}