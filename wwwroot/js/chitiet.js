
    $(document).ready(function () {
        $('#addToCartBtn').click(function () {
            var productId = @Model.Id;
            var quantity = $('#inputSoluong').val();
            var size = $('#selectSize').val();

            var item = {
                ProductId: productId,
                Quantity: quantity,
            };

            $.ajax({
                url: '@Url.Action("AddToCart", "Home")',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(item),
                success: function (result) {
                    alert('Sản phẩm đã được thêm vào giỏ hàng.');
                },
                error: function (xhr, status, error) {
                    console.error('Lỗi khi thêm sản phẩm vào giỏ hàng.');
                }
            });
        });
    });

