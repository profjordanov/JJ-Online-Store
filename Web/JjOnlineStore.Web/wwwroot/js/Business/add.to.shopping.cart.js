(function() {
    $(document).ready(function() {
        $("#add-to-cart").click(function () {
            const shoppingCartId = localStorage.getItem("shoppingCartId");

            if (shoppingCartId == null) {

                const request = {
                    url: createShoppingCartUrl,
                    method: "POST"
                };

                $.ajax(request)
                    .then(setShoppingCartIdToLocalStorage)
                    .then(createCartItemByAjax);

            } else {            
                createCartItemByAjax(shoppingCartId);
            }

        });
    });

    function createCartItemByAjax(shoppingCartId) {
        const $productId = $("#product-id").val();
        const $productQuantity = $("#choose-quantity").val();

        const cartItemBindingModel = {
            cartId: shoppingCartId,
            productId: $productId,
            quantity: $productQuantity
        };

        $.ajax({
            type: "POST",
            url: createCartItemUrl,
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(cartItemBindingModel),
            success: function() {
                alert("success");
            },
            error: function(errorMsg) {
                alert(errorMsg);
            }
        });
    }

    function setShoppingCartIdToLocalStorage(shoppingCartId) {
        localStorage.setItem("shoppingCartId", shoppingCartId);
    };

})(createShoppingCartUrl, createCartItemUrl);