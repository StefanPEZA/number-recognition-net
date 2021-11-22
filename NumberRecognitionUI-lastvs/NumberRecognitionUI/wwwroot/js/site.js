// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


window.addEventListener("load", () => {

    const canvas = document.querySelector("#canvas");
    const predicted_canvas = document.querySelector("#predicted-canvas");
    const context = canvas.getContext("2d");
    const start_background_color = "white";
    var imageLoader = document.getElementById('imageLoader');
    imageLoader.addEventListener('change', handleImage, true);

    var default_size = 420
    canvas.height = default_size;
    canvas.width = default_size;
    predicted_canvas.height = 280;
    predicted_canvas.width = 280;

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

        context.lineTo(e.clientX - canvas.getBoundingClientRect().left,
            e.clientY - canvas.getBoundingClientRect().top);
        context.stroke();
        context.beginPath();
        context.moveTo(e.clientX - canvas.getBoundingClientRect().left,
            e.clientY - canvas.getBoundingClientRect().top);
    }
    canvas.addEventListener('mousedown', startPosition);
    canvas.addEventListener('mouseup', finishedPosition);
    canvas.addEventListener('mousemove', draw);

    $("#erase").click(function () {
        //context.fillStyle = start_background_color;
        context.clearRect(0, 0, canvas.width, canvas.height);
        //context.fillRect(0, 0, canvas.width, canvas.height);
    });

    $("#upload-link").click(function () {
        $('#imageLoader').click();
    });

    function calculateAspectRatioFit(srcWidth, srcHeight) {
        var ratio = Math.min(default_size / srcWidth, default_size / srcHeight);
        return { width: srcWidth * ratio, height: srcHeight * ratio };
    }

    imageLoader.addEventListener('change', handleImage, false);
    function handleImage(e) {
        var reader = new FileReader();
        reader.onload = function (event) {
            var img = new Image();
            img.onload = function () {
                let size = calculateAspectRatioFit(img.width, img.height)
                context.drawImage(img, 0, 0);
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