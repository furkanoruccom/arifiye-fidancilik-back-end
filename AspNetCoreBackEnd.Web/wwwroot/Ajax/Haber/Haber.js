$(document).ready(function () {

        var tr_Aciklama = $('#tr_Aciklama').summernote(),
            en_Aciklama = $('#en_Aciklama').summernote();
    
    
        // Summernote ayarları
        tr_Aciklama.summernote({
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'italic', 'underline', 'clear']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']],
            ],
            styleTags: [
                'p',
                { title: 'Default', tag: 'p', className: '', value: 'p' }
            ],
            callbacks: {
                // Yapıştırma olayı
                onPaste: function (e) {
                    var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
                    e.preventDefault();
                    document.execCommand('insertText', false, bufferText); // Yapıştırılan metni temiz bir şekilde ekle
                    $(this).summernote('fontName', '"DM Sans", sans-serif'); // Fontu ayarla
                }
            }
        });
    
        // CSS ile zorunlu font ayarı
        $('<style>')
            .prop('type', 'text/css')
            .html('.note-editable { font-family: "DM Sans", sans-serif !important; }')
            .appendTo('head');
    
    
    
        // Summernote ayarları
        en_Aciklama.summernote({
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'italic', 'underline', 'clear']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']],
            ],
            styleTags: [
                'p',
                { title: 'Default', tag: 'p', className: '', value: 'p' }
            ],
            callbacks: {
                // Yapıştırma olayı
                onPaste: function (e) {
                    var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
                    e.preventDefault();
                    document.execCommand('insertText', false, bufferText); // Yapıştırılan metni temiz bir şekilde ekle
                    $(this).summernote('fontName', '"DM Sans", sans-serif'); // Fontu ayarla
                }
            }
        });
    
        // CSS ile zorunlu font ayarı
        $('<style>')
            .prop('type', 'text/css')
            .html('.note-editable { font-family: "DM Sans", sans-serif !important; }')
            .appendTo('head');
    
    });
    
    
    
    
    