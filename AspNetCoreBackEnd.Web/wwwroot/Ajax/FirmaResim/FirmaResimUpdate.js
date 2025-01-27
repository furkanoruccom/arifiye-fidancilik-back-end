

$("#UpdateForm").submit(function (e) {
    e.preventDefault();
    var firmaId = $("#firmaId").val();

    let base64 = $("#croppedImage").attr("src");

    let oldMediaNamePath = $("#oldMediaNamePath").val();
    let oldMediaName = $("#oldMediaName").val();

    if (oldMediaNamePath == base64) {
        CreateFormData(oldMediaName);
        return;
    }
    else {
        ImageDelete(oldMediaName, "wwwroot/uploads/");
    }


    $.ajax({
        type: "POST",
        url: "/Media/ImageUploadBase64",
        data: {
            base64: base64,
            _imageFolderPath: "wwwroot/uploads/"
        },
        success: function (response) {
            if (!response.success) {
                ImageDelete(response.data, "wwwroot/uploads/");
                iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
            }

            if (response.success) {
                CreateFormData(response.data);// ekleme işlemini başlat
            }
        }
    });

    function CreateFormData(MediaName) {

        let model = {
            Id: $("#Id").val(),
            Name: $("#Name").val(),
            OrderNo: $("#OrderNo").val(),
            firmaId: firmaId,
            MediaName: MediaName,
        }


        $.ajax({
            type: "POST",
            url: "/FirmaResim/UpdateJson",
            async: false,
            data: {
                model: model,
            },
            success: function (response) {
                responseError(response.errors);

                if (!response.success) {
                    iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
                }

                if (response.success) {
                    iziToast.success({ timeout: 2500, title: 'Successfuly!', message: response.message });
                   window.location.href = "/FirmaResim/Index/" + firmaId;
                }
            }
        });
    }

});