


$("#UpdateForm").submit(function (e) {
    e.preventDefault();
   
        var model = {
            Id: $("#Id").val(),
            name: $("#name").val(),
            tr_Aciklama: tr_Aciklama.value,
            en_Aciklama: en_Aciklama.value,

        }


       

        $.ajax({
            type: "POST",
            url: "/Text/UpdateJson",
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
                    window.location.href = "/Text/Index";
                }
       

            }
        });

});


