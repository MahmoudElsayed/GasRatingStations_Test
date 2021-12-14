jQuery(document).ready(function() {

    $('.summernote').summernote({
        toolbar: [
            ['style', ['bold', 'italic', 'underline']],
            ['misc', ['undo', 'redo', 'fullscreen']],
        ],
        disableDragAndDrop: true,
        disableResizeEditor: true,
        cleaner: {
            notTime: 2400,
            action: 'paste',
            newline: '<br>',
            notStyle: 'position:absolute;top:0;left:0;right:0',
            keepHtml: true,
            keepOnlyTags: ['<p>', '<br>', '<ul>', '<li>', '<b>', '<strong>', '<i>', '<a>', 'table', 'style', 'script', 'applet', 'embed', 'noframes', 'noscript', 'html'],
            keepClasses: false,
            badTags: [],
            badAttributes: ['style', 'start']
        }
    });


}), window.edit = function() {
    $(".click2edit").summernote()
}, window.save = function() {
    $(".click2edit").summernote("destroy")
};
