function showDeleteWarning(categoryId) {
    $("#itemForDeleteValue").val(categoryId);
    $.fancybox("#deleteWarning");
}

(function () {
    $("#deleteBtn").click(() => {
        const $itemForDeleteValue = $("#itemForDeleteValue").val();
        alert($itemForDeleteValue);
    });
})();