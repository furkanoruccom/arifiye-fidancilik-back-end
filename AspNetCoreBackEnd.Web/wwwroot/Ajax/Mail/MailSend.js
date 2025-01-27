$(document).ready(function () {
    $("#formContact").submit(function (e) {
        e.preventDefault();
        var name = $("#txt_name").val();
        var mail = $("#txt_mail").val();
        var phone = $("#txt_phone").val();
        var message = $("#txt_message").val();
        

        if (name == "" || mail == "" || phone == "" || message == "") {
            alert("Lütfen tüm alanları doldurunuz.");
            return;
        }

        $.ajax({
            type: "POST",
            url: "/Home/SendMessage",
            data: {
                name: name,
                mail: mail,
                phone: phone,
                message: message
            },
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    $("#txt_name").val("");
                    $("#txt_mail").val("");
                    $("#txt_phone").val("");
                    $("#txt_message").val("");
                } else {
                    alert("hata");
                }
            }
        });
    });
});