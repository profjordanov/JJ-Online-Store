function directAddProductToCart(productId) {
    const productQuantity = 1;

    const cartItemBindingModel = {
        productId: productId,
        quantity: productQuantity
    };

    const request = {
        method: "POST",
        url: window.createCartItemUrl,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(cartItemBindingModel)
    };

    $.ajax(request)
        .then(displaySuccessMessage)
        .catch(displayErrorMessage);
}

function displaySuccessMessage() {
    $.fancybox("Product successfully added to your cart.");
}

function displayErrorMessage() {
    $.fancybox("This product has already been added to the cart.");
}