const button = document.querySelector('.btn-menu');
const menu = document.querySelector('.menu');
const spanTopo = document.querySelectorAll('.btn-menu span');
const IconSacola = document.querySelector('.svg-sacola');
const sacola = document.querySelector('.sacola')
const main = document.querySelector('main')
const setaSacola = document.querySelector('.seta-sacola');
const removeSacola = document.querySelector('.remove-sacola')

const header = document.querySelector('header');

button.addEventListener('click', () => {
    menu.classList.toggle('active');

    spanTopo[0].classList.toggle('span-topo-active');

    spanTopo[1].classList.toggle('span-bottom-active');

    IconSacola.classList.toggle('sacola-opacity');

    header.classList.toggle('header-active');

    sacola.classList.remove('sacola-active');
    setaSacola.classList.remove('seta-active');
})


$("#close-left").click(function (e) {
    e.preventDefault();
    $(".header-bottom").css({ "width": "0", "padding-left": "0", "padding-right": "0" }); // Belirlenen yüksekliği uygula
    $(".blur-overlay").css({ "opacity": "0", "z-index": "0","display": "none"  });

});


$("#close-full").click(function (e) {
    e.preventDefault();
    $(".header-bottom").css({ "width": "0", "padding-left": "0", "padding-right": "0" }); // Belirlenen yüksekliği uygula
    $(".blur-overlay").css({ "opacity": "0", "z-index": "0","display": "none"  });
    menu.classList.toggle('active');
    spanTopo[0].classList.toggle('span-topo-active');

    spanTopo[1].classList.toggle('span-bottom-active');

    IconSacola.classList.toggle('sacola-opacity');

    header.classList.toggle('header-active');

    sacola.classList.remove('sacola-active');
    setaSacola.classList.remove('seta-active');
});



$(window).resize(function () {
    if ($(window).width() > 990) {
        headerBottom();
    }
});

$(".hover-me").click(function (e) {
    e.preventDefault();
    if ($(window).width() < 990) {
        $(".header-bottom").css({ "width": "100%", "padding-left": "30px", "padding-right": "30px" }); // Belirlenen yüksekliği uygula
    }

});
function headerBottom() {
    $(".hover-me, .header-bottom").on("mouseenter", function () {
        // Mouse girince
        var height = $("#accordionExample").height();
        $(".header-bottom").css("height", height + "px"); // Belirlenen yüksekliği uygula
        $(".blur-overlay").css({ "opacity": "1", "z-index": "9","display": "block" });
        
        $("header").css("background-color", "#000");


    }).on("mouseleave", function () {
        // Mouse çıkınca
        if (!$(".header-bottom").is(":hover")) {
            $(".header-bottom").css("height", "0px");

            $(".blur-overlay").css({ "opacity": "0", "z-index": "0","display": "none" });
            $('.accordion-collapse').removeClass('show');
            $('.accordion-collapse').first().addClass('show');
            if (window.scrollY === 0) {  // scrollY 0 ise, sayfanın en üstündeyiz.
                topOfPageFunction();  // Fonksiyonu çalıştır
            }
        }
    });
}

$(document).ready(function () {
    headerBottom();
    if (window.scrollY === 0) {
        topOfPageFunction();
    }
});


if ($(window).width() > 990) {
    // Scroll olayını dinle
    window.addEventListener('scroll', function () {
        // Kullanıcının scroll pozisyonunu kontrol et
        if (window.scrollY === 0) {  // scrollY 0 ise, sayfanın en üstündeyiz.
            topOfPageFunction();  // Fonksiyonu çalıştır
        }
        else {
            $("header").css("background-color", "#000");
        }


    });

    // Sayfanın en üstünde tetiklenecek fonksiyon
    function topOfPageFunction() {
        $("header").css("background-color", "transparent")

    }
}
