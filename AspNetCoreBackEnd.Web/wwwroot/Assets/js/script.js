window.onload = function () {
  var loader = document.getElementById("loader");
  loader.style.opacity = 0;
  setTimeout(function () {
    loader.style.display = "none";
  }, 1000); // 1000ms = 1 saniye
};


$(function () {
  AOS.init();
});

// Sidebar toggle functionality
function toggleSidebar() {
  document.getElementById('sidebar').classList.toggle('active');
}

// $('.btn-side-menu').on('click', function () {
//   $('.menu-box').toggleClass('active');
//   $(this).find('i').toggleClass('fa-bars fa-times');
//   $(this).find('i').css({ "z-index": "9999" });
// });



$(document).ready(function () {
    $('.owl-slide').owlCarousel({
        items: 1,
        nav: true,
        navText: ['<img loading="lazy" src="assets/images/icon/owl-left.webp" width="20" />', '<img loading="lazy" src="assets/images/icon/owl-right.webp" width="20"/>'],
        dots: false,
        loop: true,
        slideSpeed: 900,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplayHoverPause: true,
        animateOut: 'fadeOut',
    });
    // Ensure that the navigation buttons are working
  // $('.owl-product').owlCarousel({
  //   navText: ['<img loading="lazy" src="assets/images/icon/owl-left-2.webp" />', '<img loading="lazy" src="assets/images/icon/owl-right-2.webp" />'],
  //   responsive: {
  //     0: {
  //       items: 1
  //     },
  //     768: {
  //       items: 2
  //     },
  //     1400: {
  //       items: 3
  //     }
  //   },
  //   margin: 40,
  //   nav: true,
  //   loop: true,
  //   dots: false,
  //   autoplay: true,
  //   autoplayTimeout: 3000,
  //   autoplayHoverPause: true,
  // });

});







// Ürün linki ve açılan menü üzerine gelindiğinde menüyü göster
$(".product-link, .dropdown-product").mouseenter(function () {
  $(".dropdown-product").show();
  $(".product-link i").show();
});

// Fare ürün linki ve açılan menüden çıkarsa menüyü gizle
// Burada, fare her iki öğenin dışına çıktığında menüyü gizlemek için bir kontrol yapılıyor
$(".product-link, .dropdown-product").mouseleave(function () {
  // setTimeout kullanarak kullanıcının .dropdown-product menüsü ile etkileşime devam etme şansı verilir
  setTimeout(function () {
    if (!$(".product-link:hover, .dropdown-product:hover").length) {
      $(".dropdown-product").hide();
      $(".product-link i").hide();

    }
  }, 100); // 100ms bekleme süresi, bu süre boyunca fare öğelerden biri üzerindeyse menü açık kalır
});




$(".dropdown-product").hide();
$(".mobile-product-link").click(function (e) {
  e.preventDefault();

  $(".dropdown-product").show();

});

$(".dropdown-product-close").click(function (e) {
  e.preventDefault();

  $(".dropdown-product").hide();

});


document.querySelector('.lang-toggle').addEventListener('click', function () {
  document.querySelector('.lang-menu').classList.toggle('show');
});

function toggleSidebar() {
  document.getElementById('sidebar').classList.toggle('active');
}