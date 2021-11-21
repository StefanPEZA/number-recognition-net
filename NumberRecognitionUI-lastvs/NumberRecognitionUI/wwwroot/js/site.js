// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


window.addEventListener("load", () => {

    const canvas = document.querySelector("#canvas");
    const context = canvas.getContext("2d");
    const start_background_color = "white";
    var imageLoader = document.getElementById('imageLoader');
    imageLoader.addEventListener('change', handleImage, true);

    canvas.height = window.innerHeight;
    canvas.width = window.innerWidth;

    let painting = false;

    function startPosition(e) {

        painting = true;
        draw(e);
    }

    function finishedPosition() {

        painting = false;
        context.beginPath();
    }

    function draw(e) {

        if (!painting) return;
        context.lineWidth = 10;
        context.lineCap = 'round';

        context.lineTo(e.clientX - canvas.offsetLeft,
            e.clientY - canvas.offsetTop);
        context.stroke();
        context.beginPath();
        context.moveTo(e.clientX - canvas.offsetLeft,
            e.clientY - canvas.offsetTop);
    }
    canvas.addEventListener('mousedown', startPosition);
    canvas.addEventListener('mouseup', finishedPosition);
    canvas.addEventListener('mousemove', draw);

    $("#erase").click(function () {
        context.fillStyle = start_background_color;
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.fillRect(0, 0, canvas.width, canvas.height);
    });

    $("#upload-link").click(function () {
        $('#imageLoader').click();
    });

    imageLoader.addEventListener('change', handleImage, false);
    function handleImage(e) {
        var reader = new FileReader();
        reader.onload = function (event) {
            var img = new Image();
            img.onload = function () {
                canvas.width = img.width;
                canvas.height = img.height;
                ctx.drawImage(img, 0, 0);
            }
            img.src = event.target.result;
        }
        reader.readAsDataURL(e.target.files[0]);
    }

    /*$('#predict-button').click(function () {

        var id = 5;
        $.ajax({

            method: "POST",
            url: '/Index/send',
            data: { id: id },
            success: function (a) {
                // Replace the div's content with the page method's return.
                alert("success");

            },

        });
    });*/
});