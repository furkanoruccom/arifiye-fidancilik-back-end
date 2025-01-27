


$("#CreateForm").submit(function (e) {
    e.preventDefault();


    var firmaId = $("#firmaId").val();

    let base64 = $("#croppedImage").attr("src");

    $.ajax({
        type: "POST",
        url: "/Media/ImageUploadBase64",
        data: {
            base64: base64,
            _imageFolderPath: "wwwroot/uploads/"
        },
        success: function (response) {
            if (!response.success) {
                iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
            }

            if (response.success) {
                CreateFormData(response.data);// ekleme işlemini başlat
            }
        }
    });

    function CreateFormData(MediaName) {
        var model = {
            firmaId: firmaId,
            
            tr_Baslik: $("#tr_Baslik").val(),
            en_Baslik: $("#en_Baslik").val(),
            tr_Aciklama: tr_Aciklama.value,
            en_Aciklama: en_Aciklama.value,
            orderNo: $("#orderNo").val(),
            MediaName: MediaName,
        }


        $.ajax({
            type: "POST",
            url: "/FirmaUrun/CreateJson",
            data: {
                model: model,
            },
            success: function (response) {
                console.log(response);

                if (!response.success) {
                    ImageDelete(response.data);
                    iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
                }


                if (response.success) {
                    iziToast.success({ timeout: 1500, title: 'Successfuly!', message: response.message });
                    window.location.href = "/FirmaUrun/Index/" + firmaId;
                }

                responseError(response.errors);

            }
        });
    }



});



