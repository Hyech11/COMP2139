$(document).ready(function () {
    $('#searchBox').on('keyup', function () {
        let query = $(this).val();
        $('#spinner').show();
        $.ajax({
            url: '/Product/Search',
            data: { query: query },
            success: function (result) {
                $('#productList').html(result);
                $('#spinner').hide();
            }
        });
    });
});
