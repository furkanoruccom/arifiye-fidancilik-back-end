

var errorText = "Hata";

$(".phone-mask").inputmask({
    mask: [
        '+99 (999) 999-9999',
        '+999 (999) 999-9999',
        '+9999 (999) 999-9999'
    ],
    placeholder: "",
    showMaskOnHover: false,
    showMaskOnFocus: false,
    definitions: {
        '9': {
            validator: "[0-9]",
            cardinality: 1
        }
    }
});


$(".email-mask").inputmask({
    mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
    greedy: false,
    onBeforePaste: function (pastedValue, opts) {
        pastedValue = pastedValue.toLowerCase();
        return pastedValue.replace("mailto:", "");
    },
    definitions: {
        '*': {
            validator: "[0-9A-Za-z!#$%&'*+/=?^_`{|}~-]",
            cardinality: 1,
            casing: "lower"
        }
    },
    placeholder: "",
    showMaskOnHover: false,
    showMaskOnFocus: false
});


$(document).ready(function () {
    $('input, select, textarea').on('focus', function () {
        $(this).attr('autocomplete', 'off');
    });
});





// Global AJAX setup for headers
$.ajaxSetup({
    headers: {
        'X-XSRF-Token': $('input[name="__RequestVerificationToken"]').val()
    },
    error: function (jqXHR, textStatus, errorThrown) {
        console.log(jqXHR);
        console.log(textStatus);
        console.log(errorThrown);
        iziToast.error({ timeout: 15000, icon: 'fas fa-times', title: errorText, message: errorThrown });
        iziToast.error({ timeout: 15000, icon: 'fas fa-times', title: errorText, message: textStatus });
        iziToast.error({ timeout: 15000, icon: 'fas fa-times', title: errorText, message: jqXHR.responseText });
        $(".loading-overlay").css({ "visibility": "hidden", "opacity": "0" });
    }
});
// AJAX istekleri başladığında çalışacak fonksiyon
$(document).ajaxStart(function () {
    $(".loading-overlay").css({ "visibility": "visible", "opacity": "0.5" });
});

// Herhangi bir AJAX isteği tamamlandığında çalışacak fonksiyon
$(document).ajaxComplete(function () {
    $(".loading-overlay").css({ "visibility": "hidden", "opacity": "0" });
});


function responseError(errors) {
    console.log(errors);
    for (var key in errors) {
        if (errors.hasOwnProperty(key)) {
            var fieldErrors = errors[key].errors;
            for (var i = 0; i < fieldErrors.length; i++) {
                // alert(fieldErrors[i].errorMessage);
                iziToast.warning({ timeout: 15000, icon: 'fas fa-times', title: errorText, message: fieldErrors[i].errorMessage });
                return;
            }
        }
        $(".loading-overlay").css({ "visibility": "hidden", "opacity": "0" });
    }
}