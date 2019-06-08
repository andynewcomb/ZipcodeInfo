// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


$('#submitzip').click(function() {
    var zipcode = $('#zipcode').val();
    var apiEndpoint = "https://zipcodeinfo.azurewebsites.net/api/zipcodeinfo?code=";
    var url = apiEndpoint + zipcode;

    var request = new XMLHttpRequest();

    request.open('get', url, true);
    request.onload = function () {
        var obj = JSON.parse(request.responseText);
        $('#message').text(JSON.stringify(obj, undefined, 2));
    };

    request.send();
});



    
