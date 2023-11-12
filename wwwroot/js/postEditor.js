var postBody = document.querySelector("#postBody");

var toolbarOptions = [
    [{ 'font': [] }],
    [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
    [{ 'color': [] }, { 'background': [] }],
    [{ 'align': [] }],
    ['bold', 'italic', 'underline', 'strike'],
    ['link', 'blockquote', 'code-block'],

    [{ 'header': 1 }, { 'header': 2 }],
    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
    [{ 'script': 'sub' }, { 'script': 'super' }],
    [{ 'indent': '-1' }, { 'indent': '+1' }],
    [{ 'direction': 'rtl' }],

    ['clean']
];

var quill = new Quill('#editor-container', {
    modules: {
        toolbar: toolbarOptions
    },
    placeholder: 'Introdueix el contingut del post...',
    theme: 'snow'  // or 'bubble'
});

// Load body to quill editor
quill.root.innerHTML = postBody.value;

quill.on('text-change', function () {
    postBody.value = quill.root.innerHTML;

});