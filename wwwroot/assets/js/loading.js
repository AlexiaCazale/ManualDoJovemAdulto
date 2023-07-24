function displayBusyIndicator() {
    $(".loading").show();
};

function hideBusyIndicator(){
    $(".loading").hide();
}

document.addEventListener("DOMContentLoaded", function () {
    var el = document.getElementsByTagName('form')[0];
    if (el != undefined){
        el.addEventListener('submit', function() {
            if ($(el).valid() === true) {
                displayBusyIndicator();
            }
        });
    }
    if (document.readyState == 'loading') {
        displayBusyIndicator();
    }
});

document.addEventListener('load', function () {
    hideBusyIndicator();
});