$(document).ready(function () {
    function calculateTotalPrice() {
        let totalPrice = 0;
        $('#cartTable tbody tr').each(function () {
            const quantity = parseInt($(this).find('.quantity').text(), 10);
            const price = parseFloat($(this).find('.price').text().replace('$', '').replace(',', ''));
            const itemTotal = quantity * price;
            $(this).find('.item-total').text(`$${itemTotal.toFixed(2)}`);
            totalPrice += itemTotal;
        });
        $('#totalPrice').text(`$${totalPrice.toFixed(2)}`);
    }
    if ($('#cartTable tbody tr').length === 0) {
        $('#cartTable').hide();
        $('#heading').hide();
        $('#total').hide();
        $('#emptyCartMessage').show();
    }

    calculateTotalPrice();

    $(document).on('click', 'button[data-action]', function () {
        const productId = $(this).data('product-id');
        const action = $(this).data('action');
        let change = 0;

        if (action === 'increase') {
            change = 1;
        } else if (action === 'decrease') {
            change = -1;
        } else if (action === 'delete') {
            removeItem(productId);

            return;
        }

        updateQuantity(productId, change);
    });

    function updateQuantity(productId, change) {
        $.post(`/Customer/Cart/UpdateQuantity`, { productId: productId, change: change })
            .done(function () {
                const quantityElement = $(`button[data-product-id="${productId}"]`).siblings('.quantity');
                let quantity = parseInt(quantityElement.text(), 10);

                if (change === 1) {
                    quantity += 1;
                } else if (change === -1 && quantity > 1) {
                    quantity -= 1;
                }

                quantityElement.text(quantity);
                calculateTotalPrice();
            })
            .fail(function () {
                alert('Failed to update quantity');
            });
    }

    function removeItem(productId) {
        $.post(`/Customer/Cart/RemoveItem`, { productId: productId })
            .done(function (response) {
                if (response.success) {
                    $(`#row-${productId}`).remove();
                    calculateTotalPrice();
                    $('#cart-count').text(`(${response.cartCount})`);
                    if ($('#cartTable tbody tr').length === 0) {
                        $('#cartTable').hide();
                        $('#completePurchase').hide();
                        $('#totalPrice').hide();
                        $('#totalheading').hide();
                    }

                } else {
                    alert(response.message || 'Failed to remove item');
                }
            })
            .fail(function () {
                alert('Failed to remove item');
            });
    }
    $('#completePurchase').on('click', function () {
        window.location.href = '/Customer/Order/ReviewOrder';
    });
});