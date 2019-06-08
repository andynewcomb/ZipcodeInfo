// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


$('#zipcode').blur(function() {
    var zipcode = $('#zipcode').val();
    httpGet()
});

function httpGet(url, callback) {
    var request = new XMLHttpRequest();

    request.open('get', url, true);
    request.onload = function() {
        callback(request);
    };

    request.send();
}

httGet('details.txt',
    function(request) {
        console.log(request.responseText);
    });
