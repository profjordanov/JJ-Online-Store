//plugin bootstrap minus and plus
//http://jsfiddle.net/laelitenetwork/puJ6G/
$(document).ready(function () {
    $(".btn-number").click(function (e) {
        e.preventDefault();

        var fieldName = $(this).attr('data-field');
        var type = $(this).attr('data-type');
        var input = $("input[name='" + fieldName + "']");
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            if (type == 'minus') {
                var minValue = parseInt(input.attr('min'));
                if (!minValue) minValue = 1;
                if (currentVal > minValue) {
                    input.val(currentVal - 1).change();
                }
                if (parseInt(input.val()) == minValue) {
                    $(this).attr('disabled', true);
                }

            } else if (type == 'plus') {
                var maxValue = parseInt(input.attr('max'));
                if (!maxValue) maxValue = 9999999999999;
                if (currentVal < maxValue) {
                    input.val(currentVal + 1).change();
                }
                if (parseInt(input.val()) == maxValue) {
                    $(this).attr('disabled', true);
                }

            }
        } else {
            input.val(0);
        }
    });
    $('.input-number').focusin(function () {
        $(this).data('oldValue', $(this).val());
    });
    $('.input-number').change(function () {

        var minValue = parseInt($(this).attr('min'));
        var maxValue = parseInt($(this).attr('max'));
        if (!minValue) minValue = 0;
        if (!maxValue) maxValue = 20;
        var valueCurrent = parseInt($(this).val());

        var name = $(this).attr('name');
        if (valueCurrent >= minValue) {
            $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            $(this).val($(this).data('oldValue'));
        }
        if (valueCurrent <= maxValue) {
            $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
        } else {
            $(this).val($(this).data('oldValue'));
        }


    });
    $(".input-number").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
            // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $('tbody input').change(updateTotalPrice);

    function updateTotalPrice() {
        if ($(this).val() <= 20) {
            let quantity = $(this).val();
            let price = $(this).closest('td').prev('td').text().slice(1);
            let totalPrice = quantity * price;
            $(this).closest('td').next('td').text('$' + totalPrice.toFixed(2));
        } else {
            $(this).closest('td').next('td').text('$0.00');
        }

        updateGrandTotal();
    }

    function updateGrandTotal() {
        let grandTotal = 0;
        let pricesArray = $('tbody td:nth-of-type(4)');
        pricesArray.each(function () {
            grandTotal += Number($(this).text().slice(1));
        });

        $('tfoot td:last-of-type').text("Grand total: $" + grandTotal.toFixed(2));
    }

    $('.remove-item').click(removeRow);

    function removeRow() {
        $(this).parent().parent().fadeOut(500, function () {
            $(this).remove();
            updateGrandTotal();
        });
    }
});