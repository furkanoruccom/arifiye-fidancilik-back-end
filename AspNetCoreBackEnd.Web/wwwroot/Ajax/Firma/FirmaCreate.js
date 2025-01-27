


$("#CreateForm").submit(function (e) {
    e.preventDefault();

    let base64 = $("#croppedImage").attr("src");
    console.log(base64);
    // alert(base64);
    // return;
    if (base64 == "" || base64 == null || base64 == "/Panel/img/warning.jpg") {
        // iziToast.warning({ timeout: 1500, title: 'Warning!', message: "Please select image" });
        CreateFormData(null);
        return;
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
                iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
            }

            if (response.success) {
                CreateFormData(response.data); // ekleme işlemini başlat
            }
        }
    });



    function CreateFormData(MediaName) {


        var model = {
            tr_Baslik: $("#tr_Baslik").val(),
            en_Baslik: $("#en_Baslik").val(),

            tr_KisaAciklama: $("#tr_KisaAciklama").val(),
            en_KisaAciklama: $("#en_KisaAciklama").val(),

            
            tr_Buttons: $("#tr_Buttons").val(),
            en_Buttons: $("#en_Buttons").val(),
 

            tr_Aciklama: tr_Aciklama.value,
            en_Aciklama: en_Aciklama.value,


            orderNo: $("#orderNo").val(),
            MediaName: MediaName,

        }


        $.ajax({
            type: "POST",
            url: "/Firma/CreateJson",
            data: {
                model: model,
            },
            async: false,
            success: function (response) {

                if (!response.success) {
                    console.log(response);
                    ImageDelete(response.data);
                    iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
                }


                if (response.success) {
                    iziToast.success({ timeout: 1500, title: 'Successfuly!', message: response.message });
                    window.location.href = "/Firma/Index";
                }
                responseError(response.errors);

            }
        });
    }



});



