


$("#UpdateForm").submit(function (e) {
    e.preventDefault();
    let base64 = $("#croppedImage").attr("src");


    let oldMediaNamePath = $("#oldMediaNamePath").val();
    let oldMediaName = $("#oldMediaName").val();

    if(oldMediaNamePath == base64){
        return CreateFormData(oldMediaName);
    }
    else{
        ImageDelete(oldMediaName, "wwwroot/uploads/");
    }

    $.ajax({
        type: "POST",
        url: "/Media/ImageUploadBase64",
        data: {
            base64:base64,
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
            Id: $("#Id").val(),
            tr_Baslik: $("#tr_Baslik").val(),
            en_Baslik: $("#en_Baslik").val(),
            tr_KisaAciklama: $("#tr_KisaAciklama").val(),
            en_KisaAciklama: $("#en_KisaAciklama").val(),
            tr_Aciklama: tr_Aciklama.value,
            en_Aciklama: en_Aciklama.value,
            orderNo: $("#orderNo").val(),
            MediaName: MediaName,
        }


       

        $.ajax({
            type: "POST",
            url: "/Haber/UpdateJson",
            data: {
                model: model,

            },
            success: function (response) {
                responseError(response.errors);

                if (!response.success) {
                    ImageDelete(response.data);
                    iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
                }


                if (response.success) {
                    iziToast.success({ timeout: 1500, title: 'Successfuly!', message: response.message });
                    window.location.href = "/Haber/Index";
                }
       

            }
        });
    }

});


