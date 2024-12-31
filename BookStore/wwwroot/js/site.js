// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function updateCartCount() {
    $.ajax({
        url: '/Customer/Cart/GetCartCount',
        method: 'GET',
        success: function (count) {
            const cartCountElement = $("#cart-count");
            if (cartCountElement.length) {
                cartCountElement.text(`(${count})`);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error fetching cart count:', error);
        }
    });
}

$(document).ready(function () {
    updateCartCount();
});
