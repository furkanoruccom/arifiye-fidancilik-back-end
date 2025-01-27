

$("#CreateForm").submit(function (e) {
    e.preventDefault();
    var firmaId = $("#firmaId").val(),
        image_files = $("#MediaFile")[0].files,
        durum = true;

    for (var i = 0; i < image_files.length; i++) {
        if (image_files[i].size > 1048576) {
            iziToast.warning({ timeout: 8000, title: 'Error!', message: image_files[i].name + ": Dosya boyutu çok büyük! Maksimum dosya boyutu 1 MB olmalıdır." });
            durum = false;
            return;
        }
    }


    if (!durum) {
        return;
    }

    var order = 0;

    for (var i = 0; i < image_files.length; i++) {

        var image_file = image_files[i],
            formdata = new FormData();

        formdata.append("image", image_file);
        formdata.append("_imageFolderPath", "wwwroot/uploads/");


        $.ajax({
            type: "POST",
            url: "/Media/ImageUpload",
            data: formdata,
            async: false,
            contentType: false,
            processData: false,
            success: function (response) {

                if (response.success) {
                    order += 10;
                    CreateFormData(response.data, order, firmaId);
                }
                if (!response.success) {
                    iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
                }
            }
        });
    }






    function CreateFormData(MediaName, order, firmaId) {

        let model = {
            OrderNo: order,
            firmaId: firmaId,
            MediaName: MediaName,
        }


        $.ajax({
            type: "POST",
            url: "/FirmaResim/CreateJson",
            async: false,
            data: {
                model: model,
            },
            success: function (response) {


                if (!response.success) {
                    iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
                }

                if (response.success) {
                    iziToast.success({ timeout: 2500, title: 'Successfuly!', message: response.message });
                    window.location.href = "/FirmaResim/Index/" + firmaId;
                }
                responseError(response.errors);
            }
        });
    }
});