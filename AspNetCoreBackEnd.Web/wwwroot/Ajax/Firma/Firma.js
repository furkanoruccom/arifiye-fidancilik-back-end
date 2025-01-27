$(document).ready(function () {
    var tr_Aciklama = $('#tr_Aciklama'),
        en_Aciklama = $('#en_Aciklama');

    // Summernote temiz içerik fonksiyonu
    function cleanHTML(content) {
        return content
            .replace(/<font[^>]*>/g, '')   // <font> etiketlerini kaldır
            .replace(/<\/font>/g, '')     // </font> etiketlerini kaldır
            .replace(/ style="[^"]*"/g, '') // inline style'ları kaldır
            .replace(/<span[^>]*>/g, '')   // <span> etiketlerini kaldır
            .replace(/<\/span>/g, '');     // </span> etiketlerini kaldır
    }

    // Summernote başlangıç fonksiyonu
    function initSummernote(element) {
        element.summernote({
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'italic', 'underline', 'clear']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']],
            ],
            callbacks: {
                // Yapıştırma sırasında HTML temizleme
                onPaste: function (e) {
                    var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
                    e.preventDefault();
                    document.execCommand('insertText', false, bufferText); // Sadece metni ekle
                },
                // Değişiklik sırasında HTML temizleme
                onChange: function (contents) {
                    var cleanedContents = cleanHTML(contents); // HTML'i temizle
                    element.summernote('code', cleanedContents); // Temizlenmiş içeriği tekrar yükle
                }
            },
            styleWithCSS: false, // CSS stillerini devre dışı bırak
        });
    }

    // Summernote başlat
    initSummernote(tr_Aciklama);
    initSummernote(en_Aciklama);
});
