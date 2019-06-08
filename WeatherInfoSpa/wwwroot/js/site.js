// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


$('#zipcode').blur(function() {
    var zipcode = $('#zipcode').val();
    var apiEndpoint = "https://zipcodeinfo.azurewebsites.net/api/zipcodeinfo?code=";
    var url = apiEndpoint + zipcode;

    var request = new XMLHttpRequest();

    request.open('get', url, true);
    request.onload = function () {
        $('#message').text(request.responseText);
    };

    request.send();
});



    
