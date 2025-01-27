$(document).ready(function () {
    $("#isBasvuruFormu").submit(function (e) {
        e.preventDefault();


        var FormDataWWW = new FormData();
        FormDataWWW.append('uploadedFile', $('#txtCv')[0].files[0]);
        FormDataWWW.append("_pdfFolderPath", "wwwroot/uploads/");

        console.log(FormDataWWW);
        
        $.ajax({
            type: "POST",
            url: "/Media/PdfUploadJson",
            data: FormDataWWW,
            processData: false,
            contentType: false,
            success: function (response) {
                if (!response.success) {
                    alert(response.message);
                }

                CreateJson(response.data);
            },
            error: function (xhr, status, error) {
                console.error("Hata oluştu:", error);
            }
        });
        

        function CreateJson(cv) {


            var data = {
                name: $("#txtName").val(),
                mail: $("#txtMail").val(),
                phone: $("#txtPhone").val(),
                job: $("#txtJob").val(),
                city: $("#txtCity").val(),
                cv: cv,
            }


            $.ajax({
                type: "POST",
                async: false,
                url: "/Home/IsBasvuruJson",
                data: data,
                success: function (response) {

                    if (response.success) {
                        alert(response.message);
                        window.location.reload();
                    } else {
                        alert(response.message);
                    }


                }
            });
        }







    });
});