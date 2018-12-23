(function() {
    $(document).ready(function() {
        $("#search-btn").click(function () {
            const searchedCategoryVal = $("#main-categories").val();
            const searchedWord = $("#searched-product-name").val();

            if (searchedWord) {
                const queryData = {
                    category: searchedCategoryVal,
                    searchedWord: searchedWord
                };

                const url = searchUrl + encodeQueryData(queryData);

                window.location.href = url;
            } else {
                $.fancybox("Need help? " +
                    "Go to Customer Service...");
            }
        });
    });
})(searchUrl);